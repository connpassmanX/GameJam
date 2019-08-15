using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SoundController.Instance.playBgm (SoundController.SOUND.BGM_TITLE);
	}
	
	// Update is called once per frame
	void Update () {
        //マウスボタン(左)が押されたとき
        if (Input.GetMouseButtonDown(0))
        {
			GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ().Initialize ();
			SceneManager.LoadScene("OTUGE");
        }
	}
}
