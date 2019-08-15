using UnityEngine;
using System.Collections;

public class Panty : MonoBehaviour {
	private ScoreManager ScoreManager_;
	private bool isHaveCitizen;
	private GameObject effectParticlePrefab;

	void Start () {
		effectParticlePrefab = (GameObject)Resources.Load ("Prefabs/PantyGetPrefab");
	}


    //衝突が始まったときに１度だけ呼ばれる関数（接触したオブジェのタグを表示する） 
    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("col_enter:" + col.gameObject.tag);
		if (isHaveCitizen) {
			getScoreManager ().addCitizen ();
			GameObject.Find ("GameManager").GetComponent<CameraManager> ().randomEffectiveCamera ();

			//地域住民の暖かい声援
			SoundController.Instance.play (SoundController.SOUND.VOICE_CITIZEN_ON_ENCOUNT);
		}
		getScoreManager ().addPanty ();

		Instantiate (effectParticlePrefab, this.transform.position, Quaternion.identity);

		Destroy(gameObject);

		SoundController.Instance.play (SoundController.SOUND.MAINCHARACTOR_GET_PANTY);
		SoundController.Instance.play (SoundController.SOUND.VOICE_MAIN_GET_PANTY);


    }

	public void setIsCitizen(bool isCitizen) {
		isHaveCitizen = isCitizen;
	}

	public bool getIsCitizen()
	{
		return isHaveCitizen;
	}

	private ScoreManager getScoreManager()
	{
		if (ScoreManager_ == null) {
			ScoreManager_ = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		}
		return ScoreManager_;
	}

}
