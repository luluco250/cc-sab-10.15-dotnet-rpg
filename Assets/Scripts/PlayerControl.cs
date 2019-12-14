using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
	public DirectionButton direction1, direction2, direction3;
	public List<Player> players;
	public static int currentPlayer = 0;
	public Graphic sheetGraphic;
	public float moveDelay = 0.5f;
	public GameObject executeButton;
	public AudioClip gotBitcoinSound;
	public Board board;

	AudioSource audioSource;

	public Color[] sheetColors = {
		Color.blue, Color.red, Color.magenta, Color.yellow
	};

	void Awake() {
		audioSource = this.GetComponent<AudioSource>();
	}

	void Start() {
		foreach (var p in players) {
			p.data.heldBitcoins = 0;
			p.data.baseBitcoins = 0;
		}

		UpdateSheetColor();
		UpdateSheetDirections();
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
		var lastPos = player.data.position;
		
		CallDirection(player, direction.GetDirection());
		
		var cell = board.GetCell(player.data.position);
		Debug.Log(cell);

		if (player.data.position == Vector2Int.zero) {
			Debug.Log($"{player.data.playerName} is mining!");

			if (player.data.heldBitcoins < 3) {
				++player.data.heldBitcoins;
				audioSource.PlayOneShot(gotBitcoinSound);
			}
			
			player.data.position = lastPos;
		} else if (player.data.position == player.data.startPosition) {
			Debug.Log($"{player.data.playerName} is storing!");

			player.data.baseBitcoins += player.data.heldBitcoins;
			player.data.heldBitcoins = 0;
		} else if (GetPlayer(player.data.position, player, out Player other)) {
			Debug.Log(
				$"{player.data.playerName} is stealing from " +
				$"{other.data.playerName}!"
			);

			Steal(player, other);

			player.data.position = lastPos;
		}
		
		yield return new WaitForSeconds(moveDelay);
	}

	void NextPlayer() {
		UpdatePlayerData();
		currentPlayer = (currentPlayer + 1) % 4;
		UpdateSheetColor();
		UpdateSheetDirections();
	}

	void CallDirection(Player player, Direction dir) {
		switch (dir) {
			case Direction.Up:
				player.Up();
				break;
			case Direction.Down:
				player.Down();
				break;
			case Direction.Left:
				player.Left();
				break;
			case Direction.Right:
				player.Right();
				break;
		}
	}

	void UpdatePlayerData() {
		var dirs = players[currentPlayer].data.directions;
		dirs[0] = direction1.GetDirection();
		dirs[1] = direction2.GetDirection();
		dirs[2] = direction3.GetDirection();
	}

	void UpdateSheetColor()
		=> sheetGraphic.color = sheetColors[currentPlayer];
	
	void UpdateSheetDirections() {
		var dirs = players[currentPlayer].data.directions;
		direction1.SetDirection(dirs[0]);
		direction2.SetDirection(dirs[1]);
		direction3.SetDirection(dirs[2]);
	}
	
	Player GetPlayerAtCell(Cell cell) {
		switch (cell) {
			case Cell.PlayerRed:
				return players[0];
			case Cell.PlayerPurple:
				return players[1];
			case Cell.PlayerBlue:
				return players[2];
			case Cell.PlayerYellow:
				return players[3];
			default:
				throw new Exception(
					"A célula do inimigo não tem um inimigo?"
				);
		}
	}

	bool GetPlayer(Vector2Int pos, Player except, out Player found) {
		foreach (var player in players) {
			if (player == except)
				continue;

			if (player.data.position == pos) {
				found = player;
				return true;
			}
		}
		
		found = null;
		return false;
	}
	
	void Steal(Player player, Player other) {
		if (player.data.heldBitcoins < 3 && other.data.heldBitcoins > 0) {
			--other.data.heldBitcoins;
			++player.data.heldBitcoins;
		}
	}
}