using System;
using UnityEngine;

public class Player : MonoBehaviour {
    public PlayerData data;
    public Grid grid;
    new Renderer renderer;

    void Awake() {
        renderer = GetComponent<MeshRenderer>();
    }

    void Start() {
        renderer.material.color = GetColorFromType(data.cellType);
    }

    void FixedUpdate() {
        var newPos = grid.GetCellCenterLocal((Vector3Int)data.position);
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            newPos,
            0.1f
        );
            
    }

    public static Color GetColorFromType(Board.Cell cellType) {
        switch (cellType) {
            case Board.Cell.PlayerBlue:
                return Color.blue;
            case Board.Cell.PlayerRed:
                return Color.red;
            case Board.Cell.PlayerPurple:
                return Color.magenta;
            case Board.Cell.PlayerYellow:
                return Color.yellow;
            default:
                throw new Exception("No color for " + cellType);
        }
    }

    public void Up()
        => data.position.y = 1;

    public void Down()
        => data.position.y -= 1;

    public void Left()
        => data.position.x -= 1;

    public void Right()
        => data.position.x += 1;
}