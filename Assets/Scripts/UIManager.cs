using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {

	public Image progressCursol;
	public Text pantyNumLabel;

	private ScoreManager scoreManager;
	private GameManager gameManager;

	void Start () {
		scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		SetProgressCursol(0.0f);
		pantyNumLabel.text = "X 0";
	}

	void Update(){
		SetPantyNum(scoreManager.getPantyNum());
		SetProgressCursol(gameManager.getPlayerRunProgressRate());
	}

	public void SetPantyNum(int pantyNum)
	{
		pantyNumLabel.text = "X " + pantyNum.ToString();
	}

	public void SetProgressCursol(float progressRate)
	{
		//Debug.Log (progressRate);
		//progressRate = 50.0f;
		Vector2 pos = progressCursol.GetComponent<RectTransform>().anchoredPosition;
		pos.x = (912 * (progressRate / 100.0f)) - 456;
		progressCursol.GetComponent<RectTransform>().anchoredPosition = pos;
	}
}
