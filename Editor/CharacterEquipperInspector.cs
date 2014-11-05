using Assets.M.Scripts.Units;
using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(CharacterEquipper))]
public class CharacterEquipperInspector : Editor
{
	private CharacterEquipper character;
	private GameObject hair;
	private GameObject bag;
	private GameObject leftWeapon;
	private GameObject rightWeapon;
	private GameObject leftShoulder;
	private GameObject rightShoulder;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		character = (CharacterEquipper)target;

		_drawMountFor(hair, CharacterMounts.Hair);
		_drawMountFor(bag, CharacterMounts.Bag);
		_drawMountFor(leftWeapon, CharacterMounts.LeftHand);
		_drawMountFor(rightWeapon, CharacterMounts.RightHand);
		_drawMountFor(leftShoulder, CharacterMounts.LeftShoulder);
		_drawMountFor(rightShoulder, CharacterMounts.RightShoulder);
		
	}


	private void _drawMountFor(GameObject go, CharacterMounts mount)
	{
		go = EditorGUILayout.ObjectField(mount.ToString(), go, typeof(GameObject), true) as GameObject;
		if (go)
		{
			if (character._getGameObjectFromMount(mount) == null)
			{
				character._attachGameObjectToMount(go, mount);
			}
		}
	}
}
