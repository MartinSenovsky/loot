using UnityEngine;
using System.Collections;

public class UnitStatus : MonoBehaviour
{
	private Unit _unit;
	private UnitStats _unitStats;




	void Start()
	{
		_unit = GetComponent<Unit>();
		_unitStats = GetComponent<UnitStats>();
	}

	void Update()
	{

	}


	public bool _canMove()
	{
		if (_isDead()) return false;


		return true;
	}


	public bool _canAttack()
	{
		if (_isDead()) return false;


		return true;
	}


	public bool _canBeAttacked()
	{
		if (_isDead()) return false;


		return true;
	}


	public bool _canProtect()
	{
		if (_isDead()) return false;


		return true;
	}


	public bool _isDead()
	{
		if (_unitStats._Hp <= 0)
		{
			return true;
		}

		return false;
	}

}
