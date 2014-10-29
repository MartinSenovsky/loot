using System.Collections.Generic;
using Assets.M.Scripts.Utils;
using Holoville.HOTween;
using SULogger.Primitives;
using UnityEngine;
using System.Collections;

public class UnitsLoader : MonoBehaviour
{
	private List<Unit> _units = new List<Unit>();

	//public delegate void ListDelegate(List<Unit> list);

	[HideInInspector]
	//public event ListDelegate _signalUnitsSpawned;
	public Signal<List<Unit>> _signalUnitsSpawned = new Signal<List<Unit>>();

	[HideInInspector]
	public Signal<List<Unit>> _signalEnemiesSpawned = new Signal<List<Unit>>();

	private float _spawnTimeScale = 0.5f;

	void Start()
	{
		
	}

	void Update()
	{

	}


	public void _loadPlayersUnits()
	{
		bool firstTimePlaying = MSaveLoader._getBool(MSaveLoader.NAME_FIRST_TIME_PLAYING, true);

		if (firstTimePlaying)
		{
			// create first hero unit
			int numUnits = 2;
			for (int i = 0; i < numUnits; i++)
			{
				Invoke("_spawnNewPlayer", i / Time.timeScale * _spawnTimeScale);
			}

			Invoke("_onSpawnedAll", numUnits / Time.timeScale * _spawnTimeScale);
		}
		else
		{
			int numUnits = MSaveLoader._getInt(MSaveLoader.NUM_HEROES, 0);

			if (numUnits == 0)
			{
				Debug.LogError("No units in save");
			}

			for (int i = 0; i < numUnits; i++)
			{
				Invoke("_spawnPlayer", i / Time.timeScale * _spawnTimeScale);
			}

			Invoke("_onSpawnedAll", numUnits / Time.timeScale * _spawnTimeScale);
		}
	}


	private void _spawnNewPlayer()
	{
		Transform parentTransform = GameMain._instance._playerUnitsPositionManager._getUnitParentTransform(_units.Count);

		// Unit scripts holder
		GameObject unitGameObject = Instantiate(Resources.Load("Unit", typeof(GameObject)), parentTransform.position, new Quaternion()) as GameObject;
		unitGameObject.transform.parent = parentTransform;

		Unit unit = unitGameObject.GetComponent<Unit>();
		UnitStats unitStats = unitGameObject.GetComponent<UnitStats>();
		unitStats._uid = MUtils._uid();

		UnitHud unitHud = unitGameObject.GetComponent<UnitHud>();
		unitHud._showNoHud();

		// create test hero
		_makeTestHero(unitStats);

		// save unit stats
		UnitStatsSaveLoad._saveUnitStats(unitStats);


		// Unit model holder
		GameObject unitModelGameObject = Instantiate(Resources.Load(unitStats._prefabName, typeof(GameObject)), parentTransform.position, new Quaternion()) as GameObject;
		unitModelGameObject.transform.parent = parentTransform;


		// show spawn effect
		Vector3 pos = unitModelGameObject.transform.position;
		GameMain._instance._effectManager._makeEffect(EffectManager._HERO_SPAWN, pos, new Quaternion());

		// rotate to camera
		unit._lookAt(Camera.main.transform.position);

		// Add unit to units list
		_units.Add(unit);
	}


	private void _spawnPlayer()
	{
		Transform parentTransform = GameMain._instance._playerUnitsPositionManager._getUnitParentTransform(_units.Count);

		// Unit scripts holder
		GameObject unitGameObject = Instantiate(Resources.Load("Unit", typeof(GameObject)), parentTransform.position, new Quaternion()) as GameObject;
		unitGameObject.transform.parent = parentTransform;

		Unit unit = unitGameObject.GetComponent<Unit>();
		UnitStats unitStats = unitGameObject.GetComponent<UnitStats>();
		unitStats._uid = MSaveLoader._getString(MSaveLoader.HERO_UID(_units.Count), "");

		if (unitStats._uid == "")
		{
			Debug.LogError("Unit uid is empty string after load");
		}

		UnitHud unitHud = unitGameObject.GetComponent<UnitHud>();
		unitHud._showNoHud();

		// load unit stats
		UnitStatsSaveLoad._loadUnitStats(unitStats);

		// todo load unit items

		// todo load unit skills

		// Unit model holder
		GameObject unitModelGameObject = Instantiate(Resources.Load(unitStats._prefabName, typeof(GameObject)), parentTransform.position, new Quaternion()) as GameObject;
		unitModelGameObject.transform.parent = parentTransform;


		// show spawn effect
		Vector3 pos = unitModelGameObject.transform.position;
		GameMain._instance._effectManager._makeEffect(EffectManager._HERO_SPAWN, pos, new Quaternion());

		// rotate to camera
		unit._lookAt(Camera.main.transform.position);

		// Add unit to units list
		_units.Add(unit);
	}



	private void _spawnEnemy()
	{
		Transform parentTransform = GameMain._instance._enemyUnitsPositionManager._getUnitParentTransform(_units.Count);

		// Unit scripts holder
		GameObject unitGameObject = Instantiate(Resources.Load("Unit", typeof(GameObject)), parentTransform.position, new Quaternion()) as GameObject;
		unitGameObject.transform.parent = parentTransform;

		Unit unit = unitGameObject.GetComponent<Unit>();
		UnitStats unitStats = unitGameObject.GetComponent<UnitStats>();
		UnitHud unitHud = unitGameObject.GetComponent<UnitHud>();
		unitHud._showNoHud();

		// create test hero
		_makeTestEnemy(unitStats);


		// Unit model holder
		GameObject unitModelGameObject = Instantiate(Resources.Load(unitStats._prefabName, typeof(GameObject)), parentTransform.position, new Quaternion()) as GameObject;
		unitModelGameObject.transform.parent = parentTransform;


		// show spawn effect
		Vector3 pos = unitModelGameObject.transform.position;
		GameMain._instance._effectManager._makeEffect(EffectManager._HERO_SPAWN, pos, new Quaternion());

		// Add unit to units list
		_units.Add(unit);
	}


	private void _onSpawnedAll()
	{
		_signalUnitsSpawned.Dispatch(_units);
		_units.Clear();
	}


	private void _onSpawnedAllEnemies()
	{
		_signalEnemiesSpawned.Dispatch(_units);
		_units.Clear();
	}


	private void _makeTestHero(UnitStats u)
	{
		u.Action = 0;
		u.ActionMax = Random.Range(250, 400);

		u.HpMax = 500;
		u.Hp = 500;

		u.Level = 1;
		u.Name = "Mega Hero";

		u._attackDamage = 100;

		u._prefabName = "Hero1";
	}

	private void _makeTestEnemy(UnitStats u)
	{
		u.Action = 0;
		u.ActionMax = Random.Range(450, 500);

		u.HpMax = 500;
		u.Hp = 500;

		u.Level = 10;
		u.Name = "Enemy";

		u._prefabName = "wolf";
		//		u._prefabName = "Hero2";
		//		u._prefabName = "BigHeadsHero";
	}

	public void _spawnEnemies(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Invoke("_spawnEnemy", i / Time.timeScale * _spawnTimeScale);
		}

		Invoke("_onSpawnedAllEnemies", count / Time.timeScale * _spawnTimeScale);
	}
}
