using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{

	public float OffsetY;
	public Camera _camera;

	void Update () {

		if (_camera == null)
		{
			_camera = GameObject.Find("GameCamera").GetComponent<Camera>();
		}
		Vector3 pos = _camera.transform.position;
		transform.LookAt(pos);
		transform.Rotate(0, 270 + OffsetY, 0);
	}
}
