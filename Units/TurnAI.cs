using System;
using System.Collections.Generic;
using Holoville.HOTween;
using Holoville.HOTween.Plugins.Core;
using SULogger.Primitives;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class TurnAI : MonoBehaviour
{

	public Signal _signalTurnCompleted = new Signal();
	private List<Unit> _enemies;
	private List<Unit> _friends;

	private Unit _unit;
	private Unit _target;

	private Vector3 _moveBackPosition;

	public String _attackPriority;
	public String _protectFrom;

	void Start()
	{
		_unit = GetComponent<Unit>();

		_attackPriority = "random";
		_protectFrom = "none";
	}

	void Update()
	{

	}

	public void _makeTurn()
	{
		_getTarget();


		if (_target)
		{
			// check if any enemy blocks my attack type
			Unit blocker = null;

			foreach (Unit enemy in _enemies)
			{
				if (enemy._turnAI._isProtecting(_target, _unit))
				{
					blocker = enemy;
					break;
				}
			}

			if (blocker)
			{
				// show block anim
				blocker._playPrepareBlockAnim(_unit);

				// switch target to blocker
				_target = blocker;
			}

			_moveToAttackRange();

		}
		else
		{
			// skip turn to wait for enemies to become targetable
			_unit._unitStats._Action = _unit._unitStats._ActionMax;
			_signalTurnCompleted.Dispatch();
		}
	}

	private bool _isProtecting(Unit target, Unit from)
	{
		if (_unit._unitStatus._canProtect())
		{
			return true;
		}

		return false;
	}

	private void _moveToAttackRange()
	{
		// save back position
		_moveBackPosition = _unit._unitRoot.transform.position;

		float attackRange = 1.0f;

		// calculate move to position
		Vector3 attackPosition = _target._unitRoot.transform.position;
		attackPosition = Vector3.MoveTowards(attackPosition, _moveBackPosition, attackRange);

		// look at each other
		_unit._lookAt(_target, 0.1f);
		_target._lookAt(_unit, 0.1f);

		// tween to that position
		HOTween.To(_unit._unitRoot.transform, 0.1f, new TweenParms().Prop("position", attackPosition).OnComplete(_onAttackRangeReached).Delay(0.1f));
	}

	private void _onAttackRangeReached()
	{
		_attack();
	}

	private void _attack()
	{
		_target._beforeAttackerAttack(_unit);
		_unit._signalAttackAnimDamagePoint.Add(_onUnitAttackAnimDamagePoint);
		_unit._signalAttackAnimFinished.AddOnce(_onUnitAttackAnimFinished);
		_unit._playAttackAnim();
	}

	private void _onUnitAttackAnimFinished()
	{
		_unit._signalAttackAnimDamagePoint.Remove(_onUnitAttackAnimDamagePoint);
		_moveBack();
	}

	private void _onUnitAttackAnimDamagePoint()
	{
		_unit._onAttackHitEnemy(_target);
	}

	private void _moveBack()
	{
		HOTween.To(_unit._unitRoot.transform, 0.1f, new TweenParms().Prop("position", _moveBackPosition).OnComplete(_onMoveBackDone));
	}

	private void _onMoveBackDone()
	{
		_signalTurnCompleted.Dispatch();
	}

	private void _getTarget()
	{
		if (_attackPriority == "random")
		{
			_target = _getRandomEnemy();
		}
		else
		{
			//			ErrorManager._error("Unknown _attackPriority: " + _attackPriority);
			Debug.LogError("Unknown _attackPriority: \"" + _attackPriority + "\"");
		}
	}

	public void _prepUnitLists()
	{
		_enemies = GameMain._instance._enemies;
		_friends = GameMain._instance._units;

		// i'm players enemy - switch
		if (_enemies.Contains(_unit))
		{
			_enemies = GameMain._instance._units;
			_friends = GameMain._instance._enemies;
		}
	}


	private Unit _getFrontEnemy()
	{
		return null;
	}


	private Unit _getBackEnemy()
	{
		return null;
	}


	public Unit _getRandomEnemy(int triesLeft = 30)
	{
		if (triesLeft == 0)
		{
			// there's really no 1 to target
			return null;
		}

		if (_enemies == null || _enemies.Count == 0)
		{
			_prepUnitLists();
		}

		if (_enemies.Count == 0)
		{
			// no enemies
			return null;
		}

		// get random enemy
		Unit target = _enemies[Random.Range(0, _enemies.Count)];

		if (_canAttack(target))
		{
			return target;
		}

		return _getRandomEnemy(triesLeft - 1);
	}


	private bool _canAttack(Unit target)
	{
		if (target._unitStatus._canBeAttacked() == false) return false;
		if (_unit._unitStatus._canAttack() == false) return false;

		return true;
	}
}
