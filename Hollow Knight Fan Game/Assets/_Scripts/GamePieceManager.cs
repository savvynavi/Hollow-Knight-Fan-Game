using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePieceManager : MonoBehaviour {
	[SerializeField]
	List<ObjectPool> pools;

	List<GamePiece> pieces;
	private void Start() {
		pieces = new List<GamePiece>();
	}

	public List<GamePiece> Pieces {
		get { return pieces; }
		private set { }
	}

	public float snapSpeed = 0.4f;

	GamePiece p1 = null, p2 = null;
	Vector2 tmpPosP1 = Vector2.zero, tmpPosP2 = Vector2.zero;
	bool moving = false;

	private void Update() {
		//if 2 pieces to swap, calls swap code then sets to null
		if(moving == true) {
			p1.setTargetPos();
			p2.setTargetPos();
			swapPieces(p1, p2);
		}
	}

	//resets all pieces and then refills the board
	public void Reset() {
		foreach(ObjectPool pool in pools) {
			pool.ResetAll();
		}
	}

	//takes in a transform and game board coordinate and activates a random piece colour + adds it to the piece list
	public void PlacePiece(Transform pTrans, Vector2 coord) {

		Debug.Log("and in the adding piece code");
		int pieceSelected = Random.Range(0, pools.Count);
		
		GameObject newPiece = pools[pieceSelected].GetPooledObject();
		GamePiece tmpPiece = newPiece.GetComponent<GamePiece>();
		newPiece.transform.position = pTrans.position;
		newPiece.transform.rotation = pTrans.rotation;
		tmpPiece.Coord = coord;
		tmpPiece.targetX = (int)coord.x;
		tmpPiece.targetY = (int)coord.y;
		newPiece.SetActive(true);
		tmpPiece.manager = this;
		pieces.Add(tmpPiece);
	}

	//depending opn the angle will determine what direction the piece goes in
	public void CalculateAngle(float angle, Vector2 firstTouchPos, Vector2 finalTouchPos, Vector2 coord, GamePiece pieceOne, GamePiece pieceTwo) {
		angle = Mathf.Atan2(finalTouchPos.y - firstTouchPos.y, finalTouchPos.x - firstTouchPos.x) * 180 / Mathf.PI;
		Debug.Log(angle);
		DetectAdjacentPieces(angle, coord, pieceOne, pieceTwo);
	}

	//depending on the angle, 
	public void DetectAdjacentPieces(float angle, Vector2 coord, GamePiece pieceOne, GamePiece pieceTwo) {

		if(angle > -45 && angle <= 45) {
			//right swipe
			setPieces(1, 0, coord, pieceOne, pieceTwo);
			return;

		} else if(angle > 45 && angle <= 135) {
			//up swipe
			setPieces(0, 1, coord, pieceOne, pieceTwo);
			return;

		} else if(angle > 135 || angle <= -135) {
			//left swipe
			setPieces(-1, 0, coord, pieceOne, pieceTwo);
			return;

		} else if(angle < -45 && angle >= -135) {
			//down swipe
			setPieces(0, -1, coord, pieceOne, pieceTwo);
			return;
		}
	}

	//finds the second piece, swaps their coordinates, and stes moving to true
	void setPieces(int bufferX, int bufferY, Vector2 coord, GamePiece pieceOne, GamePiece pieceTwo) {
		foreach(GamePiece piece in Pieces) {
			if(piece.Coord == new Vector2(coord.x + bufferX, coord.y + bufferY)) {
				pieceTwo = piece;
				pieceTwo.OtherPiece = pieceOne;
				pieceOne.OtherPiece = pieceTwo;
				pieceTwo.Coord = new Vector2(pieceTwo.Coord.x - bufferX, pieceTwo.Coord.y - bufferY);
				pieceOne.Coord = new Vector2(coord.x + bufferX, coord.y + bufferY);
				Debug.Log("swiped down, swapping " + pieceOne.PieceType + " with " + pieceTwo.PieceType);

				p1 = pieceOne;
				p2 = pieceTwo;
				moving = true;

				return;
			}
		}
	}

	
	//takes in 2 game pieces and swaps their position 
	void swapPieces(GamePiece pieceOne, GamePiece pieceTwo) {
		Debug.Log("swapped pieces!");
		if(moving == true) {
			//lerps the pieces towards eachother and when close enough, snaps them into position
			if(Mathf.Abs(pieceOne.targetX - pieceOne.transform.position.x) > 0.1 || Mathf.Abs(pieceOne.targetY - pieceOne.transform.position.y) > 0.1) {
				tmpPosP1 = new Vector2(pieceOne.targetX, pieceOne.targetY);
				tmpPosP2 = new Vector2(pieceTwo.targetX, pieceTwo.targetY);

				pieceOne.transform.position = Vector2.Lerp(pieceOne.transform.position, tmpPosP1, snapSpeed);
				pieceTwo.transform.position = Vector2.Lerp(pieceTwo.transform.position, tmpPosP2, snapSpeed);

			} else {
				pieceOne.transform.position = tmpPosP1;
				pieceTwo.transform.position = tmpPosP2;

				pieceOne.OtherPiece = null;
				pieceTwo.OtherPiece = null;

				//Call setTarget to make sure it resets everything
				pieceOne.setTargetPos();
				pieceTwo.setTargetPos();
				p1 = null;
				moving = false;
			}
		}
		
	}
}
