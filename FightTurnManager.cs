using System;
using System.Collections.Generic;
using SULogger.Primitives;
using UnityEngine;
using System.Collections;

public class FightTurnManager : MonoBehaviour
{
	public Signal _signalFightOver = new Signal();

	public bool _fightActive;
	public bool _moveTimeForward;

	private List<Unit> _units;
	private int i;

	void Start()
	{
		_fightActive = false;
	}

	void Update()
	{
		if (_fightActive && _moveTimeForward)
		{
			float time = Time.deltaTime * 1000;
			_update(time);
		}
	}


	private void _update(float time)
	{
		Unit _unitOnTurn = _units[i];

		// update gui
		_unitOnTurn._unitStats._updateActionTime(time);

		// do turn action
		if (_unitOnTurn._unitStats._doActionNow)
		{

			// move to next unit
			i++;
			if (i >= _units.Count)
			{
				i = 0;
			}

			// execute turn action
			_moveTimeForward = false;
			_unitOnTurn._unitStats._doActionNow = false;
			_unitOnTurn._signalTurnCompleted.AddOnce(_onUnitTurnCompleted);
			_unitOnTurn._doOnTurnAction();
		}
		else
		{
			// move to next unit
			i++;
			if (i >= _units.Count)
			{
				i = 0;
				return;
			}

			_update(time);
		}
	}

	private void _onUnitTurnCompleted(Unit unit)
	{
		// check if fight over

		bool playerWin = true;
		bool playerLost = true;

		foreach (Unit u in GameMain._instance._units)
		{
			if (u._unitStats.Hp > 0)
			{
				playerLost = false;
				break;
			}
		}

		foreach (Unit u in GameMain._instance._enemies)
		{
			if (u._unitStats.Hp > 0)
			{
				playerWin = false;
				break;
			}
		}

		if (playerWin && playerLost)
		{
			_endFightDraw();
		}
		else if (playerLost)
		{
			_endFightLost();
		}
		else if (playerWin)
		{
			_endFightWin();
		}
		else
		{
			// fight's not over
			_moveTimeForward = true;
		}
	}

	private void _endFightWin()
	{
		Debug.Log("WIN");

		_fightActive = false;
		GameMain._instance._guiManager._showFightOverMessage("Victory!");
		GameMain._instance._startMenu();
	}

	private void _endFightLost()
	{
		Debug.Log("DEFEAT");

		_fightActive = false;
		GameMain._instance._guiManager._showFightOverMessage("Defeat!");
		GameMain._instance._startMenu();
	}

	private void _endFightDraw()
	{
		Debug.Log("DRAW");

		_fightActive = false;
		GameMain._instance._guiManager._showFightOverMessage("Draw!");
		GameMain._instance._startMenu();
	}


	public void _startFight()
	{
		_fightActive = true;
		
		_units = new List<Unit>(GameMain._instance._units);
		_units.AddRange(GameMain._instance._enemies);

		foreach (Unit unit in _units)
		{
			unit._lookAt(unit._turnAI._getRandomEnemy());
		}


		i = 0;
		_moveTimeForward = true;
	}



}
