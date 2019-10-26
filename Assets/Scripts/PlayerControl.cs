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
	public GameObject executeButton;

	public Color[] sheetColors = {
		Color.blue, Color.red, Color.magenta, Color.yellow
	};

	void Awake() =>
		UpdateSheetColor();

	void Start() {
		foreach (var p in players) {
			p.data.heldBitcoins = 0;
			p.data.baseBitcoins = 0;
		}
	}

	public void Execute() =>
		StartCoroutine(SlowExecute());

	IEnumerator SlowExecute() {
		executeButton.SetActive(false);

		var player = players[currentPlayer];

		yield return PlayerAction(player, direction1);
		yield return PlayerAction(player, direction2);
		yield return PlayerAction(player, direction3);
		
		NextPlayer();
		executeButton.SetActive(true);
	}

	IEnumerator PlayerAction(Player player, DirectionButton direction) {
		CallDirection(player, direction.GetDirection());

		if (
			player.data.position == Vector2Int.zero &&
			player.data.heldBitcoins < 3
		) {
			++player.data.heldBitcoins;
		}

		if (player.data.position == player.data.startPosition) {
			player.data.baseBitcoins += player.data.heldBitcoins;
			player.data.heldBitcoins = 0;
		}

		yield return new WaitForSeconds(moveDelay);
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