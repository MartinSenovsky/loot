using UnityEngine;
using System.Collections;

public class MoveWithCamera : MonoBehaviour
{

	private Vector3 offset;
	
	void Start ()
	{
		offset = Camera.main.transform.position - transform.position;
		Debug.Log(offset);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Camera.main.transform.position - offset;
	}
}
