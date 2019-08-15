using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	private float playerRunProgressRate; // 進行度 0.0fから100.0f

	void Awake()
	{
		// 必要なシーンの追加
		List<string> currentSceneNames = new List<string> ();
		for (int idx = 0; idx < SceneManager.sceneCount; ++idx) {
			Scene s = SceneManager.GetSceneAt (idx);
			currentSceneNames.Add (s.name);
		}

		// 必要シーンの読み込み
		string[] sceneNames = new string[]{"PlayerScene", "GameUIScene", "PantyCityScene"};
		foreach (string sceneName in sceneNames) {
			//Debug.Log (sceneName);
			// 必要なシーンがなければ追加する
			if (!currentSceneNames.Contains(sceneName)) {
				SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);
			}	
		}

	}

	void Start () {
		Initialize ();

		// BGMの再生を開始
		SoundController.Instance.playBgm (SoundController.SOUND.BGM_MAIN);
		// ゲーム開始時の主人公ボイス再生
		SoundController.Instance.play (SoundController.SOUND.VOICE_MAIN_START);
	}

	public void Initialize()
	{
		// 進行度の初期化
		playerRunProgressRate = 0.0f;
	}

	public void setPlayerRunProgressRate(float rate) 
	{
		playerRunProgressRate = rate;
        //Debug.Log(rate);
	}

	public float getPlayerRunProgressRate()
	{
		return playerRunProgressRate;
	}





}
