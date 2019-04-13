using System.Collections;
using UnityEngine;

public class RenderBoard : MonoBehaviour {
    public GameObject testBitcoin;

    Grid grid;
    int x = -3;
    
    void Awake() {
        grid = GetComponent<Grid>();
    }

    void Start() {
        StartCoroutine(KeepMoving());
    }

    IEnumerator KeepMoving() {
        while (true) {
            testBitcoin.transform.position =
                grid.GetCellCenterLocal(new Vector3Int(x++, 0, 0));

            yield return new WaitForSeconds(1f);
        }
    }
}