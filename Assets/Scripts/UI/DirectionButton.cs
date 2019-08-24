using System;
using UnityEngine;
using UnityEngine.UI;

public class DirectionButton : MonoBehaviour {
	public enum Direction {
		Up = 0,
		Right = 1,
		Down = 2,
		Left = 3,
		None = 4
	}

	static readonly float[] DirectionAngles = new float[]{0, 270, 180, 90};

	Direction direction = Direction.Up;
	Graphic graphic;

	void Awake() {
		graphic = GetComponent<Graphic>();
	}

	void Start() {
		ApplyDirection();
	}

    public void OnClick() {
		int dir = (int)direction;
		dir = (dir + 1) % 5;
		direction = (Direction)dir;
		ApplyDirection();
	}

	void ApplyDirection() {
		if (direction == Direction.None) {
			graphic.color = Color.clear;
		} else {
			graphic.color = Color.white;
			var rot = transform.localRotation.eulerAngles;
			rot.z = DirectionAngles[(int)direction];
			transform.localRotation = Quaternion.Euler(rot);
		}
	}
	
	public Direction GetDirection() => direction;
	public void SetDirection(Direction dir) {
		direction = dir;
		ApplyDirection();
	}
}
