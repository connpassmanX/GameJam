using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour {
	private Dictionary<string, Camera> cameras;
	private Camera currentCamera;

	private string mainCameraName = "Main Camera";
	private string[] effectiveCameraName = new string[] { "Camera01", "Camera02", "Camera03", "Camera04" };

	private float backMainCameraCounter;

	public void Start()
	{
		cameras = new Dictionary<string, Camera> ();

		addCamera (GameObject.Find (mainCameraName).GetComponent<Camera> ());

		backMainCameraCounter = 0.0f;
	}

	public void Update()
	{
		if (0.0f < backMainCameraCounter) {
			backMainCameraCounter -= Time.deltaTime;
			if (backMainCameraCounter <= 0.0f) {
				switchMainCamera ();
			}
		}
	}

	public void addCamera(Camera camera) {
		cameras.Add (camera.name, camera);
	}

	public void switchCamera(string cameraName) {
		if (currentCamera != null) {
			currentCamera.enabled = false;
		}
		currentCamera = cameras [cameraName];
		currentCamera.enabled = true;
	}

	public void switchMainCamera()
	{
		switchCamera (mainCameraName);
	}

	public void randomEffectiveCamera()
	{
		switchCamera (effectiveCameraName[Random.Range(0, effectiveCameraName.Length)]);
		backMainCameraCounter = 0.5f;
	}
}
