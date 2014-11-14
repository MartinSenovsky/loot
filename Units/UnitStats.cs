using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour
{
	// gui
	public UnitStatPanel _statPanel;


	// general
	public string _Name;
	public int _Level;
	public int _Exp;

	public int _HpMax;

	public int _Mp;
	public int _MpMax;

	public float _Action;
	public float _ActionMax;

	public bool _doActionNow;

	public string _attackType;
	public bool _baseAttackMelee;

	
	// offensive
	public int _attackDamage = 0;
	public float _attackSpeed = 0;
	public float _armorPenetration = 0;
	public float _criticalChance = 0;
	public float _criticalDamage = 0;
	public float _lifeSteal = 0;


	// defensive
	public int _armor = 0;
	public int _hp = 0;
	public int _hpRegen = 0;
	public int _magicResistance = 0;


	// ability
	public int _abilityDamage = 0;
	public float _coolDownReduction = 0;
	public float _magicPenetration = 0;
	public float _spellVamp = 0;


	[HideInInspector]
	public string _prefabName;

	[HideInInspector]
	public string _uid;
	
	[HideInInspector]
	public UnitHud _hud;

	private Unit _unit;
	


	void Start()
	{
		_unit = GetComponent<Unit>();
	}

	void Update()
	{

	}


	public void _updateActionTime(float ms)
	{
		if (_unit._unitStatus._canAttack())
		{
			return;
		}

		// if frozen, stunned etc return

		_Action += ms;

		if (_Action >= _ActionMax)
		{
			_doActionNow = true;
			_Action = 0;
		}
	}


	public float _addValueFromItems(String statName, float value)
	{
		// items absolute values
		foreach (InventoryItemSlot slot in _hud._inventory._slots)
		{
			if (slot._hasItem())
			{
				InventoryItemStat stat = slot._getItem()._getStat(statName);
				
				if (stat && stat._isAbsoluteValue)
				{
					value += stat._statValue;
				}
			}
		}

		// items % bonuses
		foreach (InventoryItemSlot slot in _hud._inventory._slots)
		{
			if (slot._hasItem())
			{
				InventoryItemStat stat = slot._getItem()._getStat(statName);
				if (stat && stat._isAbsoluteValue == false)
				{
					value *= stat._statValue;
				}
			}
		}

		return value;
	}






	public int _totalAttackDamage(bool items = true, bool buffs = true)
	{
		float v = _attackDamage;
		if(items)
		v = _addValueFromItems(InventoryItemStat._attackDamage, v);

		// todo buffs

		return (int)v;
	}

	public int _totalArmor(bool items = true, bool buffs = true)
	{
		float v = _armor;
		if (items)
		v = _addValueFromItems(InventoryItemStat._armor, v);
		// todo buffs

		return (int)v;
	}

	public float _totalArmorPenetration(bool items = true, bool buffs = true)
	{
		float v = _armorPenetration;
		if (items)
		v = _addValueFromItems(InventoryItemStat._armorPenetration, v);
		// todo buffs

		return v;
	}

	public int _totalAttackSpeed(bool items = true, bool buffs = true)
	{
		float v = _attackSpeed;
		if (items)
		v = _addValueFromItems(InventoryItemStat._attackSpeed, v);
		// todo buffs

		return (int)v;
	}

	public int _totalAbilityPower(bool items = true, bool buffs = true)
	{
		float v = _abilityDamage;
		if (items)
		v = _addValueFromItems(InventoryItemStat._abilityDamage, v);
		// todo buffs

		return (int)v;
	}

	public int _totalHpRegen(bool items = true, bool buffs = true)
	{
		float v = _hpRegen;
		if (items)
		v = _addValueFromItems(InventoryItemStat._hpRegen, v);
		// todo buffs

		return (int)v;
	}

	public int _totalMagicResist(bool items = true, bool buffs = true)
	{
		float v = _magicResistance;
		if (items)
		v = _addValueFromItems(InventoryItemStat._magicResistance, v);
		// todo buffs

		return (int)v;
	}

	public float _totalMagicPenetration(bool items = true, bool buffs = true)
	{
		float v = _magicPenetration;
		if (items)
		v = _addValueFromItems(InventoryItemStat._magicPenetration, v);
		// todo buffs

		return v;
	}

	public float _totalCriticalDamage(bool items = true, bool buffs = true)
	{
		float v = _criticalDamage;
		if (items)
		v = _addValueFromItems(InventoryItemStat._criticalDamage, v);
		// todo buffs

		return v;
	}

	public float _totalCriticalChance(bool items = true, bool buffs = true)
	{
		float v = _criticalChance;
		if (items)
		v = _addValueFromItems(InventoryItemStat._criticalChance, v);
		// todo buffs

		return v;
	}

	public float _totalCooldownReduction(bool items = true, bool buffs = true)
	{
		float v = _coolDownReduction;
		if (items)
		v = _addValueFromItems(InventoryItemStat._coolDownReduction, v);
		// todo buffs

		return v;
	}

	public float _totalSpellVamp(bool items = true, bool buffs = true)
	{
		float v = _spellVamp;
		if (items)
		v = _addValueFromItems(InventoryItemStat._spellVamp, v);
		// todo buffs

		return v;
	}

	public float _totalLifeSteal(bool items = true, bool buffs = true)
	{
		float v = _lifeSteal;
		if (items)
		v = _addValueFromItems(InventoryItemStat._lifeSteal, v);
		// todo buffs

		return v;
	}

	public int _totalHpMax(bool items = true, bool buffs = true)
	{
		float v = _HpMax;
		if (items)
		v = _addValueFromItems(InventoryItemStat._hp, v);
		// todo buffs

		return (int)v;
	}










	public void _absorbItems()
	{
		_abilityDamage = _totalAbilityPower(true, false);
		_armor = _totalArmor(true, false);
		_armorPenetration= _totalArmorPenetration(true, false);
		_attackDamage = _totalAttackDamage(true, false);
		_attackSpeed = _totalAttackSpeed(true, false);
		_coolDownReduction = _totalCooldownReduction(true, false);
		_criticalChance = _totalCriticalChance(true, false);
		_criticalDamage = _totalCriticalDamage(true, false);
		_hp = _totalHpMax(true, false);
		_hpRegen = _totalHpRegen(true, false);
		_lifeSteal = _totalLifeSteal(true, false);
		_magicPenetration = _totalMagicPenetration(true, false);
		_magicResistance = _totalMagicResist(true, false);
		_spellVamp = _totalSpellVamp(true, false);
	}
}
