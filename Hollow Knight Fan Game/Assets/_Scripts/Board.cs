using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	[SerializeField]
	int width = 10;
	[SerializeField]
	int height = 10;
	[SerializeField]
	ObjectPool bgTilePool;
	[SerializeField]
	GamePieceManager gamePieceManager;

	struct GamePoint {
		public GameObject Tile;
		public Vector2 pos;
	}

	[SerializeField]
	List<GamePoint> gamePoints;

	private void Start() {
		gamePoints = new List<GamePoint>();
		Reset();
	}

	private void Update() {
		
	}


	//resets the board with random pieces
	public void Reset() {

		//turn all game pieces off and reset them
		gamePieceManager.Reset();

		//place all bg tiles and then pieces
		for(int i = 0; i < width; i++) {
			for(int j = 0; j < height; j++) {
				Vector2 tmpPos = new Vector2( transform.position.x + i - (width / 2), transform.position.y + j - (height / 2));
				GameObject newBGTile = bgTilePool.GetPooledObject();
				newBGTile.transform.position = tmpPos;
				newBGTile.transform.rotation = transform.rotation;
				newBGTile.SetActive(true);

				//add to gamePoints list
				GamePoint tmpGP = new GamePoint();
				tmpGP.Tile = newBGTile;
				tmpGP.pos = new Vector2(i, j);
				gamePoints.Add(tmpGP);

				Debug.Log("about to add piece");
				gamePieceManager.PlacePiece(newBGTile.transform, tmpGP.pos);
			}
		}

	}
}
