using System.Collections.Generic;
using Assets.M.Scripts.Utils;
using GoogleFu;
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
	public Signal<Unit> _signalEnemySpawned = new Signal<Unit>();

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
				DelayedCall.To(this, _spawnNewPlayer, i / Time.timeScale * _spawnTimeScale);
			}
			DelayedCall.To(this, _onSpawnedAll, numUnits / Time.timeScale * _spawnTimeScale);
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
				DelayedCall.To(this, _spawnNewPlayer, i / Time.timeScale * _spawnTimeScale);
			}
			DelayedCall.To(this, _onSpawnedAll, numUnits / Time.timeScale * _spawnTimeScale);
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

		// create test stats
		_makeTestStats(unitStats);

		// randomly choose unit
		UnitsDatabase unitsDatabase = UnitsDatabase.Instance;
		UnitsDatabaseRow unitRow = unitsDatabase.Rows[Random.Range(0, unitsDatabase.Rows.Count)];

		_fillStatsFromUnitDatabase(unitStats, unitRow);

		// save unit stats
		UnitStatsSaveLoad._saveUnitStats(unitStats);

		// prefab
		string prefabName = unitRow._prefabname1;

		// Unit model holder
		GameObject unitModelGameObject = Instantiate(Resources.Load(prefabName, typeof(GameObject)), parentTransform.position, new Quaternion()) as GameObject;
		unitModelGameObject.transform.parent = parentTransform;


		// show spawn effect
		Vector3 pos = unitModelGameObject.transform.position;
		GameMain._instance._effectManager._makeEffect(EffectManager._HERO_SPAWN, pos, new Quaternion());

		// rotate to camera
		unit._lookAt(Camera.main.transform.position);

		// Add unit to units list
		_units.Add(unit);
	}


	private void _fillStatsFromUnitDatabase(UnitStats unitStats, UnitsDatabaseRow unitRow)
	{
		unitStats._Name = unitRow._name;
		unitStats._ActionMax = unitRow._action;
		unitStats._HpMax = unitRow._hp;
		unitStats._hpRegen = unitRow._hpregen;
		unitStats._MpMax = unitRow._mp;
		unitStats._abilityDamage = unitRow._abilitydamage;
		unitStats._armor = unitRow._armor;
		unitStats._armorPenetration = unitRow._armorpenetration;
		unitStats._magicPenetration = unitRow._magicpenetration;
		unitStats._magicResistance = unitRow._magicresist;
		unitStats._spellVamp = unitRow._spellvamp;
		unitStats._attackDamage = unitRow._attackdamage;
		unitStats._attackSpeed = unitRow._attackspeed;
		unitStats._attackType = unitRow._attacktype;
		unitStats._baseAttackMelee = unitRow._baseattackmelee;
		unitStats._coolDownReduction = unitRow._cooldownreduction;
		unitStats._criticalChance = unitRow._criticalchance;
		unitStats._criticalDamage = unitRow._criticaldamage;
		unitStats._lifeSteal = unitRow._lifesteal;
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

		// create test enemy
		_makeTestEnemy(unitStats);


		// Unit model holder
		GameObject unitModelGameObject = Instantiate(Resources.Load(unitStats._prefabName, typeof(GameObject)), parentTransform.position, new Quaternion()) as GameObject;
		unitModelGameObject.transform.parent = parentTransform;


		// show spawn effect
		Vector3 pos = unitModelGameObject.transform.position;
		GameMain._instance._effectManager._makeEffect(EffectManager._HERO_SPAWN, pos, new Quaternion());

		_signalEnemySpawned.Dispatch(unit);


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


	private void _makeTestStats(UnitStats u)
	{
		u._Action = 0;
		u._ActionMax = Random.Range(250, 400);

		u._HpMax = 20;
		u._hp = 20;

		u._Level = 1;
		u._Name = "Hero" + _units.Count;

		u._attackDamage = 1;

		u._prefabName = "Hero1";
	}

	private void _makeTestEnemy(UnitStats u)
	{
		u._Action = 0;
		u._ActionMax = Random.Range(450, 500);

		u._HpMax = 10;
		u._hp = 10;

		u._Level = 1;
		u._Name = "Enemy" + _units.Count;

		u._attackDamage = 1;

		u._prefabName = "wolf";
		//		u._prefabName = "Hero2";
		//		u._prefabName = "BigHeadsHero";
	}

	public void _spawnEnemies(int count)
	{
		for (int i = 0; i < count; i++)
		{
			DelayedCall.To(this, _spawnEnemy, i / Time.timeScale * _spawnTimeScale);
		}
		DelayedCall.To(this, _onSpawnedAllEnemies, count / Time.timeScale * _spawnTimeScale);
	}
}
