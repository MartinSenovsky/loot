using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UnitsPositionManager : MonoBehaviour
{

	private List<Transform> _transforms; 

	void Start () {
		_transforms = new List<Transform>();

		foreach (Transform child in transform)
		{
			_transforms.Add(child);
		}
	}
	
	void Update () {
	
	}


	public Transform _getUnitParentTransform(int id)
	{
		if (id >= _transforms.Count)
		{
			Debug.LogError("too big position id, there are only:" + _transforms.Count + " slots!");
			return _transforms[0];
		}

		return _transforms[id];
	}
}
