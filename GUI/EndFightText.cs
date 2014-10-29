using UnityEngine;
using System.Collections;

public class EndFightText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void _onAnimationEnd()
	{
		gameObject.SetActive(false);
	}
}
