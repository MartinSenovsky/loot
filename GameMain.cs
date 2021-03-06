﻿using System;
using System.Collections.Generic;
using System.Timers;
using Assets.M.Scripts.Utils;
using Holoville.HOTween;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class GameMain : MonoBehaviour
{
	public static GameMain _instance;

	public FightTurnManager _fightTurnManager;
	public UnitsLoader _unitsLoader;
	private UnitsPositionManager[] _unitsPositionManagers;
	public UnitsPositionManager _playerUnitsPositionManager;
	public UnitsPositionManager _enemyUnitsPositionManager;
	public GuiManager _guiManager;
	public EffectManager _effectManager;
	public UnitsManager _unitsManager;

	public List<Unit> _units;
	public List<Unit> _enemies;
	

	void Start()
	{
		_instance = this;

		MSaveLoader._clearSave();

		Time.timeScale = 1;

		// get managers
		_unitsLoader = GetComponent<UnitsLoader>();
		_fightTurnManager = GetComponent<FightTurnManager>();
		_unitsPositionManagers = GetComponentsInChildren<UnitsPositionManager>();
		_guiManager = GetComponent<GuiManager>();
		_unitsManager = GetComponent<UnitsManager>();

		_effectManager = GameObject.Find("EffectManager").GetComponent<EffectManager>();

		_playerUnitsPositionManager = _unitsPositionManagers[0];
		_enemyUnitsPositionManager = _unitsPositionManagers[1];

		DelayedCall.To(this, _lateStart, 0.1f);
	}


	void _lateStart()
	{
		// load player characters
		_loadAndInstantiatePlayerUnits();
	}

	public void _startMenu()
	{
		_endGame();

		_guiManager._startMenu();
	}


	private void _endMenu()
	{
		_guiManager._hideMenu();
	}


	public void _startBattle(int level)
	{
		_endMenu();
		_unitsLoader._signalEnemiesSpawned.AddOnce(_onEnemiesSpawned);
		_unitsLoader._signalEnemySpawned.Add(_onEnemySpawned);
		_unitsLoader._spawnEnemies(3);
	}

	private void _onEnemySpawned(Unit enemy)
	{
		// rotate enemy on random friend
		Unit randomFriend = _units[Random.Range(0, _units.Count)];

		if (randomFriend)
		{
			enemy._lookAt(randomFriend);
		}

		// rotate friends on him
		foreach (Unit unit in _units)
		{
			unit._lookAt(enemy, Random.Range(0.1f, 0.4f));
		}
	}


	private void _startGameAfterEnemiesSpawned()
	{
		_guiManager._startGame();

		_fightTurnManager._startFight();
	}


	private void _endGame()
	{
		foreach (Unit enemy in _enemies)
		{
			enemy._destroy();
		}

		foreach (Unit unit in _units)
		{
			unit._unitStats._hp = unit._unitStats._HpMax;
		}


		_enemies.Clear();
	}


	private void _loadAndInstantiatePlayerUnits()
	{
		_unitsLoader._signalUnitsSpawned.AddOnce(_onUnitsSpawned);
		_unitsLoader._loadPlayersUnits();
	}

	private void _onUnitsSpawned(List<Unit> list)
	{
		_units = new List<Unit>(list);


		// start menu
		DelayedCall.To(this, _startMenu, 0.1f);
	}


	private void _onEnemiesSpawned(List<Unit> list)
	{
		_enemies = new List<Unit>(list);
		
		// start game continue
		DelayedCall.To(this, _startGameAfterEnemiesSpawned, 0.1f);
	}


	void Update()
	{

	}
}
