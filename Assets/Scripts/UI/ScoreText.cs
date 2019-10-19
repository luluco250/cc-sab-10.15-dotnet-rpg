using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
	public PlayerData playerData;
	public Text heldScore, baseScore;

	void FixedUpdate() {
		heldScore.text = playerData.heldBitcoins.ToString();
		baseScore.text = playerData.baseBitcoins.ToString();
	}
}