using UnityEngine;
using System.Collections;

public class PantyManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// プレハブを取得
		GameObject prefab = (GameObject)Resources.Load ("Prefabs/Panty");

		for (int i = 0; i < 20; i++) {
			float x = (float)(i * 10 + 4.0f);
			Vector3 position = new Vector3 ( x, 0f, 3.47f);
			// プレハブからインスタンスを生成
			Instantiate (prefab, position, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
