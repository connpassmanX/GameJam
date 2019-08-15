using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {
    public ParticleSystem effect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    void Inst_effect()
    {
        Instantiate(effect);
    }
}
