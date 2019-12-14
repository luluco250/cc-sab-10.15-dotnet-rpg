using System;

[Flags]
public enum Cell {
	None = 0,
	PlayerRed = 1,
	PlayerPurple = 2,
	PlayerBlue = 4,
	PlayerYellow = 8,
	AnyPlayer = 1 | 2 | 4 | 8,
	Bug = 16,
	Mine = 32
}