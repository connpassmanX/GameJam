using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour {
    private bool jump_;
    private bool isJump_;
    private bool isjumpButtonPush_;
    private bool isjumpCool_;//次のジャンプへのクールタイム
    Animator animator_;
    Vector3 pos;
    private float vel_y;
    //---カメラ移動用変数
    public GameObject main_Camera;
    private float camera_x=4.0f;
    private float camera_y=2.5f;
    private float camera_z=-15.0f;
    //---ゴール用変数
    private float Goal_X= 360.0f; 
    //---プログレスバー用
    GameManager game_manager;

	CameraManager cameraManager;

    // Use this for initialization
    void Start()
    {
        animator_ = GetComponent<Animator>();
        vel_y = 0.0f;
        isJump_ = false;
        isjumpButtonPush_ = false;
        main_Camera=GameObject.Find("Main Camera");
        game_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
		cameraManager = GameObject.Find ("GameManager").GetComponent<CameraManager> ();


		string[] effectiveCameraName = new string[] { "Camera01", "Camera02", "Camera03", "Camera04" };
		foreach (string cname in effectiveCameraName) {
			Camera cam = GameObject.Find (cname).GetComponent<Camera> ();
			cam.enabled = false;
			cameraManager.addCamera (cam);
		}
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        AnimatorStateInfo animator_state_ = animator_.GetCurrentAnimatorStateInfo(0);
        vel_y -= 0.4f * Time.deltaTime;//重力加速度
        
        //正面方向へ移動＋走るアニメーション開始
        pos.x += 6 * Time.deltaTime;
       
        animator_.SetBool("isRunning", true);
        pos.y += vel_y;
        if(isJump_==true&&pos.y<=0.0f){
            Invoke("jumpCoolOff",0.35f);
        }
        if (pos.y <= 0.0f)
        {
            pos.y = 0.0f;
            vel_y = 0.0f;
            isJump_ = false;
            animator_.SetBool("isJumping", false);
        }
        
        //走るアニメーション停止
        //animator_.SetBool("isRunning", false);

		//ジャンプアクション
		if (Input.GetMouseButtonDown(0) && isJump_==false&&isjumpButtonPush_==false&&isjumpCool_==false)
        {
            animator_.SetBool("isJumping", true);
            Invoke("Jump", 0.2f);
            isjumpButtonPush_ = true;
            isjumpCool_ = true;
        }
        /* ジャンプアニメーションが終わったら
        if (animator_state_.nameHash == Animator.StringToHash("Base Layer.JUMP00"))
        {        
        }*/

        transform.position = pos;
        //Debug.Log(animator_state_.IsName("Player_Animation.JUMP00"));
        main_Camera_Move(pos);
        progressBar(pos);
        gotoGoalScene();
    }
    private void Jump()
    {
        isJump_ = true;
        vel_y = 0.2f;
        isjumpButtonPush_ = false;
    }
    private void jumpCoolOff()
    {
        isjumpCool_ = false;
    }

    //-----------メインカメラ操作---------------
    
    private void main_Camera_Move(Vector3 pos)
    {
        pos=transform.position;
        pos.x += camera_x;
        pos.y = camera_y;
        pos.z+=camera_z;
        main_Camera.transform.position = pos;
    }
    //-----ゴール到達によるシーン遷移----
    private void gotoGoalScene()
    {
        if(transform.position.x>Goal_X){
            SceneManager.LoadScene("DedicationScene");
        }
    }
    //----プログレスバー関連-----
    private void progressBar(Vector3 pos)
    {
        game_manager.setPlayerRunProgressRate((pos.x/Goal_X)*100.0f);
    }
}
