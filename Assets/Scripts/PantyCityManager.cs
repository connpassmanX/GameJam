using UnityEngine;
using System.Collections;

public class PantyCityManager : MonoBehaviour {
	private float genDistance = 20.0f;

	// 地面生成
	private GameObject groundPrefab;
	private float groundInterval = 40.0f;
	private float lastGroundPosX;

	// 建物生成
	private GameObject[] housePrefabs;
	private float[] houseIntervals;
	private bool[] houseIsOneStoried;
	private float lastBuildPosX;

	// パンティ生成
	private GameObject pantyPrefab;
	private float[] pantyGenPosY;

	// 市民生成
	private GameObject citizenPrefab;


	private ScoreManager ScoreManager_;

	// プレイヤー情報
	private GameObject player;

	void Start () {
		// プレハブの読み込み
		groundPrefab = (GameObject)Resources.Load ("Prefabs/GroundPrefab");

		housePrefabs = new GameObject[4];
		housePrefabs[0] = (GameObject)Resources.Load ("Prefabs/House01Prefab");
		housePrefabs[1] = (GameObject)Resources.Load ("Prefabs/House02Prefab");
		housePrefabs[2] = (GameObject)Resources.Load ("Prefabs/House03Prefab");
		housePrefabs[3] = (GameObject)Resources.Load ("Prefabs/HousePantyShrinePrefab");
		houseIntervals = new float[]{ 7.0f, 7.65f, 2.75f, 3.65f };
		houseIsOneStoried = new bool[]{ true, false, false, true };

		pantyPrefab = (GameObject)Resources.Load ("Prefabs/PantyPrefab");
		pantyGenPosY = new float[] { 0.9f, 3.1f };

		citizenPrefab = (GameObject)Resources.Load ("Prefabs/CitizenPrefab");

		// プレイヤー情報の取得
		player = GameObject.Find ("ToKoMo_CC_Guy_01");

		// 初期値
		lastGroundPosX = -groundInterval;
		lastBuildPosX = -groundInterval;

		// 初期生成　
		for (int i = 0; i < 2; ++i) {
			generateGround ();
		}

		for (int i = 0; i < 10; ++i) {
			generateHouses ();
		}
	}

	void Update () {
		// 地面の生成
		if ((lastGroundPosX - genDistance) <= player.transform.position.x) {
			generateGround ();
		}

		// 建物の生成
		if ((lastBuildPosX - genDistance) <= player.transform.position.x) {
			generateHouses ();
		}

	}

	// 地面生成
	private void generateGround() {
		// instiate
		Vector3 position = new Vector3 ( lastGroundPosX, 0.0f, 0.0f);
		GameObject newGround = Instantiate (groundPrefab, position, Quaternion.identity) as GameObject;
		newGround.transform.parent = this.transform;

		// add last
		lastGroundPosX += groundInterval;
	}

	// 建物の生成
	private void generateHouses() {
		// build house
		int genIdx = Random.Range (0, housePrefabs.Length - 1);
		if (Random.Range (0, 200) < 1) {
			genIdx = housePrefabs.Length - 1; // 神社　
		}

		Vector3 position = new Vector3 ( lastBuildPosX, 0.0f, 0.0f);
		GameObject newHouse = Instantiate (housePrefabs[genIdx], position, Quaternion.identity) as GameObject;
		newHouse.transform.parent = this.transform;

		// generate Panty
		generatePanty(lastBuildPosX, houseIsOneStoried[genIdx]);

		// add last
		lastBuildPosX += houseIntervals [genIdx];
	}

	// パンティ・市民生成
	private void generatePanty(float buildPosX, bool isOneStoried) {
		if (buildPosX < 20.0f) {
			return;
		}
		if (Random.Range (0, 100) < (20 + getScoreManager().getCitizenNum () * 8.0f)) {
			return;
		}

		int genIdx = Random.Range (0, pantyGenPosY.Length);
		if (isOneStoried) {
			genIdx = 0;
		}

		bool isCitizen = (Random.Range (0, 100) < 30); // 30%で市民つき
		if (isCitizen) {
			generateCitizen (buildPosX);
		}

		// instiate
		Vector3 position = new Vector3 ( buildPosX + 1.6f, pantyGenPosY[genIdx], 0.0f);
		GameObject newPanty = Instantiate (pantyPrefab, position, Quaternion.identity) as GameObject;
		newPanty.transform.parent = this.transform;
		newPanty.GetComponent<Panty> ().setIsCitizen (isCitizen);
	}

	// 市民
	private void generateCitizen(float buildPosX) {
		// instiate
		Vector3 position = new Vector3 ( buildPosX + 2.0f, 0.0f, 0.0f);
		GameObject newCitizen = Instantiate (citizenPrefab, position, Quaternion.identity) as GameObject;
		newCitizen.transform.parent = this.transform;
	}


	private ScoreManager getScoreManager()
	{
		if (ScoreManager_ == null) {
			ScoreManager_ = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		}
		return ScoreManager_;
	}
}
