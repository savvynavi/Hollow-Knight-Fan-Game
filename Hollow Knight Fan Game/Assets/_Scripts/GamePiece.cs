using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour {

	public enum TYPE {
		BLUE = 1,
		GREEN,
		PURPLE
	}
	public GamePieceManager manager = null;

	[SerializeField]
	TYPE pieceType;
	[SerializeField]
	Vector2 coord = Vector2.zero;

	Vector2 firstTouchPos = Vector2.zero;
	Vector2 finalTouchPos = Vector2.zero;
	float angle = 0.0f;
	Camera cam;
	GamePiece otherPiece = null;
	Vector2 tmpPosition = Vector2.zero;
	Vector2 tmpPositionOther = Vector2.zero;
	public float targetX;
	public float targetY;

	public float initialPosX;
	public float initialPosY;
	public bool isTargetSet = false;


	public Vector2 Coord {
		get { return coord; }
		set { coord = value; }
	}

	public GamePiece OtherPiece {
		get { return otherPiece; }
		set { otherPiece = value; }
	}

	public TYPE PieceType {
		get { return pieceType; }
		set { }
	}

	private void Start() {
		cam = Camera.main;
		initialPosX = transform.position.x;
		initialPosY = transform.position.y;
	}

	//private void Update() {

		//setTargetPos();

		////if you've selected 2 peices to swap, lerps it towards it's final position and swaps them
		//if(otherPiece != null) {


		//	if(Mathf.Abs(targetX - transform.position.x) > 0.1) {
		//		tmpPosition = new Vector2((targetX), transform.position.y);
		//		transform.position = Vector2.Lerp(transform.position, tmpPosition, 0.4f);
		//	} else {

		//		transform.position = tmpPosition;

		//		OtherPiece = null;
		//	}
		//}
	//}

	public void setTargetPos() {
		if(OtherPiece != null) {
			targetX = OtherPiece.initialPosX;
			targetY = OtherPiece.initialPosY;
		} else {
			targetX = this.transform.position.x;
			targetY = this.transform.position.y;
			initialPosX = targetX;
			initialPosY = targetY;
		}
	}


	//detects where the player first clicks the piece
	private void OnMouseDown() {
		firstTouchPos = cam.ScreenToWorldPoint(Input.mousePosition);
		Debug.Log(firstTouchPos);
	}

	//detects where the player lets go of the piece and then calculates an angle
	private void OnMouseUp() {
		finalTouchPos = cam.ScreenToWorldPoint(Input.mousePosition);
		Debug.Log(finalTouchPos);
		manager.CalculateAngle(angle, firstTouchPos, finalTouchPos, coord, this, otherPiece);
	}
}
