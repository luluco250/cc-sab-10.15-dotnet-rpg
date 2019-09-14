using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
	public DirectionButton direction1, direction2, direction3;
	public List<Player> players;
	public static int currentPlayer = 0;
	public Graphic sheetGraphic;
	public float moveDelay = 0.5f;

	public Color[] sheetColors = {
		Color.blue, Color.red, Color.magenta, Color.yellow
	};

	void Awake() =>
		UpdateSheetColor();

	public void Execute() =>
		StartCoroutine(SlowExecute());

	IEnumerator SlowExecute() {
		var player = players[currentPlayer];

		CallDirection(player, direction1.GetDirection());
		yield return new WaitForSeconds(moveDelay);

		CallDirection(player, direction2.GetDirection());
		yield return new WaitForSeconds(moveDelay);

		CallDirection(player, direction3.GetDirection());
		yield return new WaitForSeconds(moveDelay);
		
		NextPlayer();
	}

	void NextPlayer() {
		currentPlayer = (currentPlayer + 1) % 4;
		UpdateSheetColor();
	}

	void CallDirection(Player player, DirectionButton.Direction dir) {
		switch (dir) {
			case DirectionButton.Direction.Up:
				player.Up();
				break;
			case DirectionButton.Direction.Down:
				player.Down();
				break;
			case DirectionButton.Direction.Left:
				player.Left();
				break;
			case DirectionButton.Direction.Right:
				player.Right();
				break;
		}
	}

	void UpdateSheetColor()
		=> sheetGraphic.color = sheetColors[currentPlayer];
}