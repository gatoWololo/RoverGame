using UnityEngine;
using System.Collections;

public class CreateTerrain : MonoBehaviour {
	public DirtTile tile;
	//Two dimensional array of tiles.
	private Tile[,] tileTerrain;

	//================================================================================
	void Start () {
		this.createNewTerrain (100, 100);

		return;
	}
	//================================================================================
	// Create terain of tiles in a 10x10 fashion.
	private Tile[,] createNewTerrain(int height, int width){
		this.tileTerrain = new Tile[height,width];

		/*Initialize all tiles to dirt tiles for now.*/
		for (int i = 0; i < height; i++){
			for (int j = 0; j < width; j++) {
				Vector2 position = new Vector2 (i - height / 2, j - width / 2);
				this.tileTerrain[i,j] = instantiateTile(position);
				this.tileTerrain[i,j].setTileIndex(i,j);
				this.tileTerrain[i,j].renderer.enabled = false;
			}
		}


		return this.tileTerrain;
	}
	//================================================================================
	public DirtTile instantiateTile(Vector2 position){

		//DirtTile tile = (DirtTile) Resources.Load ("Prefabs/dirtTilePrefab");

		Vector3 tilePosition = new Vector3 (position.x, position.y, 0);
		DirtTile dirtTile = Instantiate(this.tile, tilePosition, Quaternion.identity) as DirtTile;
		
		return dirtTile;
	}
	//================================================================================
	//Given a direction returns the index of the tiles adjacent to this one in that
	//direction.
	public int getAdjacentTiles(Direction direction,Tile currentTile){
		
		return -1;
		
	}
	// Update is called once per frame
	void Update () {
	
	}
	//================================================================================
}
