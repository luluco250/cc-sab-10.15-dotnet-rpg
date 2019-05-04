using System.Collections;
using UnityEngine;

public class RenderBoard : MonoBehaviour {
	public GameObject testBitcoin;

	Grid grid;
	int x = -3;
	int y = 2;
	int xdir = 1;
	int ydir = -1;
	float time;
	
	void Awake() {
		grid = GetComponent<Grid>();
	}

	void Start() {
		time = Time.time;
	}

	void Update() {
		if (Time.time - time > 0.5f) {
			x += xdir;
			xdir = (x == 3) ? -1 : (x == -3) ? 1 : xdir;
			y += ydir;
			ydir = (y == 3) ? -1 : (y == -3) ? 1 : ydir;

			int r = Random.Range(0, 5);

			testBitcoin.transform.localPosition =
				grid.GetCellCenterLocal(new Vector3Int(x, y, 0));
			
			time = Time.time;
		}
	}
}