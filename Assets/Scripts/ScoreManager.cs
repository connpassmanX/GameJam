using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	private int pantyNum;
	private int citizenNum;

	void Start () {
		DontDestroyOnLoad (this);
		Initialize ();
	}

	public void Initialize() 
	{
		pantyNum = 0;
		citizenNum = 0;
	}

	public int getPantyNum()
	{
		return pantyNum;
	}

	public void addPanty()
	{
		++pantyNum;
        //Debug.Log(pantyNum);
	}


	public int getCitizenNum()
	{
		return citizenNum;
	}

	public void addCitizen()
	{
		++citizenNum;
	}
}
