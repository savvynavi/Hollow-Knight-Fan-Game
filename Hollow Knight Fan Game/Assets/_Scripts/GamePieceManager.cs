using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePieceManager : MonoBehaviour {
	[SerializeField]
	List<ObjectPool> pieces;


	private void Start() {
		
	}

	//resets all pieces and then refills the board
	public void Reset() {
		foreach(ObjectPool pool in pieces) {
			pool.ResetAll();
		}
	}

	public void PlacePiece(Transform pTrans, Vector2 coord) {

		Debug.Log("and in the adding piece code");
		int pieceSelected = Random.Range(0, pieces.Count);

		GameObject newPiece = pieces[pieceSelected].GetPooledObject();
		newPiece.transform.position = pTrans.position;
		newPiece.transform.rotation = pTrans.rotation;
		newPiece.GetComponent<GamePiece>().Coord = coord;
		newPiece.SetActive(true);
	}
}
