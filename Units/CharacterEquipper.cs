using Assets.M.Scripts.Units;
using UnityEngine;
using System.Collections;

public class CharacterEquipper : MonoBehaviour
{
	public SkinnedMeshRenderer _skinnedMeshRenderer;


	void Start()
	{

	}


	private Transform _getMountPoint(CharacterMounts mount)
	{
		string name = "";

		switch (mount)
		{
			case CharacterMounts.Hair:
				name = "mount0";
				break;
			case CharacterMounts.Bag:
				name = "mount1";
				break;
			case CharacterMounts.LeftHand:
				name = "mount4";
				break;
			case CharacterMounts.RightHand:
				name = "mount3";
				break;
			case CharacterMounts.LeftShoulder:
				name = "mount5";
				break;
			case CharacterMounts.RightShoulder:
				name = "mount6";
				break;

		}

		if (name == "")
		{
			Debug.LogError("unknown mount " + mount);
		}


		Transform t = transform.FindChild(name);

		if (t == null)
		{
			Debug.LogError("no transform for mount: " + mount);
		}

		return t;
	}


	void Update()
	{

	}


	public void _attachGameObjectToMount(GameObject o, CharacterMounts mountPointName)
	{
		Transform mount = _getMountPoint(mountPointName);
		o.transform.parent = mount;
		o.transform.localPosition = new Vector3();
		o.transform.localRotation = new Quaternion();
	}


	public GameObject _getGameObjectFromMount(CharacterMounts mount)
	{
		Transform t = _getMountPoint(mount);

		if (t.childCount == 0)
		{
			return null;
		}

		return t.GetChild(0).gameObject;
	}
}
