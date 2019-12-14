using System;
using UnityEngine;

public class Player : MonoBehaviour {
    public PlayerData data;
    public Grid grid;
    new Renderer renderer;

    void Awake() {
        renderer = GetComponent<MeshRenderer>();
        data.position = data.startPosition;
    }

    void Start() {
        renderer.material.color = GetColorFromType(data.cellType);
        
        for (int i = 0; i < data.directions.Length; ++i)
            data.directions[i] = Direction.Up;
    }

    void FixedUpdate() {
        var newPos = grid.GetCellCenterLocal((Vector3Int)data.position);
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            newPos,
            0.1f
        );
            
    }

    public static Color GetColorFromType(Cell cellType) {
        switch (cellType) {
            case Cell.PlayerBlue:
                return Color.blue;
            case Cell.PlayerRed:
                return Color.red;
            case Cell.PlayerPurple:
                return Color.magenta;
            case Cell.PlayerYellow:
                return Color.yellow;
            default:
                throw new Exception("No color for " + cellType);
        }
    }

    public void Up()
        => data.position.y = Mathf.Clamp(data.position.y + 1, -3, 3);

    public void Down()
        => data.position.y = Mathf.Clamp(data.position.y - 1, -3, 3);

    public void Left()
        => data.position.x = Mathf.Clamp(data.position.x - 1, -3, 3);

    public void Right()
        => data.position.x = Mathf.Clamp(data.position.x + 1, -3, 3);
}