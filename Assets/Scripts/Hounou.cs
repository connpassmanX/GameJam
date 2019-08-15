using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hounou : MonoBehaviour {

	Animator animator_;
	Vector3 pos;
	private float time_;
	private int OTUGE_state_;
	public ParticleSystem kourin;
	public ParticleSystem kourin_burst;
	public GameObject panty_;
	public GameObject pantyPanel;
	public Text pantyNum;

	public ParticleSystem pantyParticleSystem;

	GameObject fall_panty_;

	Camera mainCamera;
	Camera kourinCamera;

	private ScoreManager scoreManager;

	public GameObject houonPanel;

	bool countFlag = true;

	// Use this for initialization
	void Start () {
//		fall_panty_ = GameObject.Find("fallpants");
		animator_ = GetComponent<Animator>();
		time_ = 0.0f;
		OTUGE_state_ = 0;
//		panty_ = GameObject.Find("Helmet");
//		panty_.SetActive(false);
		mainCamera=GameObject.Find("Main Camera").GetComponent<Camera>();
//		kourinCamera = GameObject.Find("kourin_Camera").GetComponent<Camera>();
//		kourinCamera.enabled = false;
		pantyPanel.SetActive(false);

		scoreManager = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();

	}
	
	// Update is called once per frame
	void Update () {
		//-------共通の処理
		pos = transform.position;
		AnimatorStateInfo animator_state_ = animator_.GetCurrentAnimatorStateInfo(0);

		//-----神社へ走ってくる
		//正面方向へ移動＋走るアニメーション開始
		if(OTUGE_state_==0)
		{
			pos.x += 3 * Time.deltaTime;
			animator_.SetBool("isRunning", true);
			if (time_ > 3.0f)
			{
				Instantiate(kourin, new Vector3(-2.0f, 4.0f, 3.9f),Quaternion.Euler(90,0,0));
//				mainCamera.enabled = false;
//				kourinCamera.enabled = true;
				OTUGE_state_ = 1;
				time_ = 0.0f;

				SoundController.Instance.play (SoundController.SOUND.GAME_CLEAR);
			}
		}

		//-----パンツ神降臨
		if (OTUGE_state_ == 1) {
			//走るアニメーション停止
			animator_.SetBool ("isRunning", false);
			if (countFlag) {
				countFlag = false;
				float k = 0.0f;
				for (int i = 0; i < scoreManager.getPantyNum(); i++) {
					Invoke ("make_particle", k);
					k += 0.1f;
				}
			}
			if(time_ > 3.0f) {
				OTUGE_state_ = 2;
				Debug.Log (":OTUGE_state_:");

				SoundController.Instance.play (SoundController.SOUND.VOICE_MAIN_CLEAR);

				houonPanel.SetActive(true);
			}

		}

		if (OTUGE_state_ == 2) {
			Debug.Log("--------------------------------2");
			pantyPanel.SetActive(true);

			pantyNum.text  = scoreManager.getPantyNum().ToString();
		}

		//--------共通の処理
		transform.position = pos;
		time_ += 1.0f * Time.deltaTime;
		Debug.Log(time_);

		//マウスボタン(左)が押されたとき
		if (Input.GetMouseButtonDown(0))
		{
			SceneManager.LoadScene("TitleScene");
		}
	}

	private void make_particle(){
		Instantiate (pantyParticleSystem, new Vector3 (pos.x, (pos.y + 2.0f), pos.z), Quaternion.Euler (0, 45, 0));	
	}
}
