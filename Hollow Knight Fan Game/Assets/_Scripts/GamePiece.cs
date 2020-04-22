using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour {

	enum TYPE {
		BLUE = 1,
		GREEN,
		PURPLE
	}

	[SerializeField]
	TYPE PieceType;
	[SerializeField]
	Vector2 coord = Vector2.zero;

	public Vector2 Coord {
		get { return coord; }
		set { coord = value; }
	}

	private void Start() {
		
	}

	private void Update() {
		
	}

	//raycasts out in all 4 directions to see what colour tiles are near it
	public void DetectAdjacentPieces() {

	}
}
