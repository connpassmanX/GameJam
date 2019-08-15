using UnityEngine;
using System.Collections;

public class TimeDestroy : MonoBehaviour {
	public float destroyTime = 5.0f;

	void Start() {
		Destroy (this.gameObject, destroyTime);
	}
}
