using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour
{
	public static int _HERO_SPAWN = 1;
	public static int _BLOOD_HIT = 2;
	public static int _SPELL_HEAL_RECEIVED = 3;


	public GameObject _prefabHeroSpawn;
	public GameObject _prefabSpellHealReceived;
	public List<GameObject> _prefabsBloodHit;

	private GameObject _effectsParent;

	void Start()
	{
		_effectsParent = GameObject.Find("Game").transform.FindChild("Effects").gameObject;
	}

	void Update()
	{

	}


	public GameObject _makeEffect(int type, Vector3 pos, Quaternion rotation)
	{
		GameObject effectGameObject = null;

		if (type == _HERO_SPAWN)
		{
			effectGameObject = Instantiate(_prefabHeroSpawn, pos, rotation) as GameObject;
			effectGameObject.transform.parent = _effectsParent.transform;
		}
		else if (type == _BLOOD_HIT)
		{
			effectGameObject = Instantiate(_randomFromList(_prefabsBloodHit), pos, rotation) as GameObject;
			effectGameObject.transform.parent = _effectsParent.transform;
		}
		else if (type == _SPELL_HEAL_RECEIVED)
		{
			effectGameObject = Instantiate(_prefabSpellHealReceived, pos, rotation) as GameObject;
			effectGameObject.transform.parent = _effectsParent.transform;
		}
		else
		{
			Debug.LogError("Unknown effect type: " + type);
		}


		return effectGameObject;
	}


	private T _randomFromList<T>(List<T> l)
	{
		return l[Random.Range(0, l.Count)];
	}
}
