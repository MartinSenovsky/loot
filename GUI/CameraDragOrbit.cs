using Holoville.HOTween;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraDragOrbit : MonoBehaviour
{

	public static CameraDragOrbit _instance;

	public Vector3 _defaultTargetPosition;
	private Transform _targetTransform;

	void Start()
	{
		_instance = this;
		Vector3 angles = transform.eulerAngles;
		rotationYAxis = angles.y;
		rotationXAxis = angles.x;

		_targetTransform = GameObject.Find("GameCameraTarget").transform;
		_defaultTargetPosition = _targetTransform.position;
	}


	public void _tweenTo(Vector3 pos, float dist, float timeSec)
	{
		HOTween.To(this, timeSec, new TweenParms().Prop("distance", dist));
		HOTween.To(_targetTransform, timeSec, new TweenParms().Prop("position", pos));
	}


	void Update()
	{

	}


	public void _onPress()
	{
		
	}


	public void _onRelease()
	{
		
	}



	public Transform target;
	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	public float distanceMin = .5f;
	public float distanceMax = 15f;

	public float smoothTime = 2f;

	float rotationYAxis = 0.0f;
	float rotationXAxis = 0.0f;

	float velocityX = 0.0f;
	float velocityY = 0.0f;

	private bool _dragging;

	
	void LateUpdate()
	{
		if (target && Input.GetMouseButtonDown(0) && EventSystemManager.currentSystem.IsPointerOverEventSystemObject() == false)
		{
			_dragging = true;
		}

		if (Input.GetMouseButtonUp(0) && _dragging)
		{
			_dragging = false;
		}

		if (target)
		{
			if (_dragging && InventoryDragDropManager._instance._dragging == false)
			{
				velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
				velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
			}

			rotationYAxis += velocityX;
			rotationXAxis -= velocityY;

			rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);

			Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
			Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
			Quaternion rotation = toRotation;

			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

//			RaycastHit hit;
//			if (Physics.Linecast(target.position, transform.position, out hit))
//			{
//				distance -= hit.distance;
//			}
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;

			transform.rotation = rotation;
			transform.position = position;

			velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
			velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);

			if (velocityX < 0.0001f || velocityX > -0.0001f)
			{
				velocityX = 0;
			}
			if (velocityY < 0.0001f || velocityY > -0.0001f)
			{
				velocityY = 0;
			}
		}

	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}
