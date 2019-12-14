﻿using System;
using UnityEngine;

public class Board : MonoBehaviour {
	Cell[][] board = new Cell[7][];

	void Awake() {
		for (int i = 0; i < 7; ++i)
			board[i] = new Cell[7];
		
		// Canto esquerdo superior.
		board[0][0] = Cell.PlayerRed;
		// Canto direito superior.
		board[6][0] = Cell.PlayerPurple;
		// Canto direito inferior.
		board[6][6] = Cell.PlayerBlue;
		// Canto esquerdo inferior.
		board[0][6] = Cell.PlayerYellow;
		// Meio.
		board[3][3] = Cell.Mine;
	}

	public Cell GetCell(Vector2Int pos)
		=> GetCell(pos.x, pos.y);
	public void SetCell(Vector2Int pos, Cell type)
		=> SetCell(pos.x, pos.y, type);

	public Cell GetCell(int x, int y) {
		return board[x + 3][y + 3];
	}
	public void SetCell(int x, int y, Cell type)
		=> board[x + 3][y + 3] = type;

	public bool MoveCell(int x1, int y1, int x2, int y2) {
		// Verificar se a posição antiga está dentro do tabuleiro.
		if (x1 < 0 || x1 > 6 || y1 < 0 || y1 > 6) {
			throw new Exception(
				$"Board.MoveCell(): Posição inicial inválida! ({x1}, {y1})"
			);
		}

		// Verificar se a nova posição está dentro do tabuleiro.
		if (x2 < 0 || x2 > 6 || y2 < 0 || y2 > 6)
			return false;
		
		// Verificar se a nova posição está vazia.
		if (board[x2][y2] != Cell.None)
			return false;

		board[x2][y2] = board[x1][y1];
		board[x1][y1] = Cell.None;
		return true;
	}

	public bool AddCell(int x, int y, Cell type) {
		// Verificar se a posição da nova célula está dentro do tabuleiro.
		if (x < 0 || x > 6 || y < 0 || y > 6)
			return false;
		
		// Verificar se a posição da nova célula está vazia.
		if (board[x][y] != Cell.None)
			return false;
		
		board[x][y] = type;
		return true;
	}
}