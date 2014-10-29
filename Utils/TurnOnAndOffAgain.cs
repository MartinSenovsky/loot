using UnityEngine;
using System.Collections;

public class TurnOnAndOffAgain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("_turnOff", 1.0f);
	}


	private void _turnOff()
	{
		gameObject.SetActive(false);
		Invoke("_turnOn", 1.0f);
	}


	private void _turnOn()
	{
		gameObject.SetActive(true);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
