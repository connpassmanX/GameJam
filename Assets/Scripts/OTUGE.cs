using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OTUGE : MonoBehaviour {
    Animator animator_;
    Vector3 pos;
    private float time_;
    private int OTUGE_state_;
    public ParticleSystem kourin;
    public ParticleSystem kourin_burst;
    GameObject panty_;
    GameObject fall_panty_;
    

    Camera mainCamera;
    Camera kourinCamera;
	// Use this for initialization
	void Start () {
        fall_panty_ = GameObject.Find("fallpants");
        animator_ = GetComponent<Animator>();
        time_ = 0.0f;
        OTUGE_state_ = 0;
        panty_ = GameObject.Find("Helmet");
        panty_.SetActive(false);
        fall_panty_.SetActive(false);
        mainCamera=GameObject.Find("Main_Camera").GetComponent<Camera>();
        kourinCamera = GameObject.Find("kourin_Camera").GetComponent<Camera>();
        kourinCamera.enabled = false;
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
            pos.x -= 3 * Time.deltaTime;
            animator_.SetBool("isRunning", true);
            if (time_ > 3.0f)
            {
                Instantiate(kourin, new Vector3(-2.0f, 4.0f, 3.9f),Quaternion.Euler(90,0,0));
                mainCamera.enabled = false;
                kourinCamera.enabled = true;
                fall_panty_.SetActive(true);
                OTUGE_state_ = 1;

            }
        }
        //-----パンツ神降臨
        if (OTUGE_state_ == 1)
        {
            //走るアニメーション停止
            animator_.SetBool("isRunning", false);

			//パンツが男の頭に当たるまで落とす
            if (fall_panty_.transform.position.y > 1.41)
            {
                fall_panty_.transform.position += new Vector3(0.0f, -0.8f, 0.0f) * Time.deltaTime;
                kourinCamera.transform.position += new Vector3(-0.15f, -0.78f, -0.1f) * Time.deltaTime;
            }
            if (fall_panty_.transform.position.y <= 1.41)
            {
				Instantiate(kourin_burst, new Vector3(-2.21f, 1.4f, 3.842f), Quaternion.Euler(90, 0, 0));
				fall_panty_.transform.position = new Vector3 (-2.21f, 1.41f, 3.842f);
			}
            if (time_ > 8.0f)
            {
				SoundController.Instance.play (SoundController.SOUND.MAINCHARACTOR_JOIN_PANTY);

                panty_.SetActive(true);
                Destroy(fall_panty_);
                OTUGE_state_ = 2;
            }
        }
        //-----フェイスアップ
        if (OTUGE_state_ == 2)
        {
            if (time_ < 9.0f)
            {
                kourinCamera.transform.position += new Vector3(0.3f, 0.3f, 0.6f) * Time.deltaTime;
                kourinCamera.transform.LookAt(panty_.transform);
            }
            if (time_ > 10.0f)
            {
                mainCamera.enabled = true;
                kourinCamera.enabled = false;
                transform.Rotate(new Vector3(0f, 180f, 0f));
                OTUGE_state_ = 3;
            }
        }
        //-----反転して走っていく
        if(OTUGE_state_==3)
        {
            pos.x += 4 * Time.deltaTime;
            animator_.SetBool("isRunning", true);
            if (time_ > 13.0f) SceneManager.LoadScene("GameMainScene");
        }
        //--------共通の処理
        transform.position = pos;
        time_ += 1.0f * Time.deltaTime;
        //Debug.Log(time_);
	}

	void LateUpdate()
	{
		//-----パンツ神降臨
		if (OTUGE_state_ == 1) {
			//走るアニメーション停止
			animator_.SetBool ("isRunning", false);
			Vector3 shogoPos = transform.position;
			shogoPos.x = -2.2f;
			shogoPos.z = 3.9f;
			transform.position = shogoPos;
		}
	}
}
