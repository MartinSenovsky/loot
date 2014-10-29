using System;
using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour
{
	public UnitStatPanel _statPanel;

	public string Name;
	public int Level;
	public int Exp;

	public int Hp;
	public int HpMax;

	public int Mp;
	public int MpMax;

	public float Action;
	public float ActionMax;

	public bool _doActionNow;

	[HideInInspector]
	public string _prefabName;
	[HideInInspector]
	public string _uid;
	[HideInInspector]
	public UnitHud _hud;


	// Game visible stats in stats panel - base values

	// offensive
	public int _attackDamage = 0;
	public int _attackSpeed = 0;
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


	void Start()
	{

	}

	void Update()
	{

	}


	public void _updateActionTime(float ms)
	{
		if (Hp <= 0)
		{
			return;
		}

		// if frozen, stunned etc return

		Action += ms;

		if (Action >= ActionMax)
		{
			_doActionNow = true;
			Action = 0;
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


	public int _totalAttackDamage()
	{
		float v = _attackDamage;

		_addValueFromItems(InventoryItemStat._attackDamage, v);

		// todo buffs

		return (int)v;
	}

	public int _totalArmor()
	{
		int v = _armor;

		_addValueFromItems(InventoryItemStat._armor, v);
		// todo buffs

		return v;
	}

	public float _totalArmorPenetration()
	{
		float v = _armorPenetration;

		_addValueFromItems(InventoryItemStat._armorPenetration, v);
		// todo buffs

		return v;
	}

	public int _totalAttackSpeed()
	{
		int v = _attackSpeed;

		_addValueFromItems(InventoryItemStat._attackSpeed, v);
		// todo buffs

		return v;
	}

	public int _totalAbilityPower()
	{
		int v = _abilityDamage;

		_addValueFromItems(InventoryItemStat._abilityDamage, v);
		// todo buffs

		return v;
	}

	public int _totalHpRegen()
	{
		int v = _hpRegen;

		_addValueFromItems(InventoryItemStat._hpRegen, v);
		// todo buffs

		return v;
	}

	public int _totalMagicResist()
	{
		int v = _magicResistance;

		_addValueFromItems(InventoryItemStat._magicResistance, v);
		// todo buffs

		return v;
	}

	public float _totalMagicPenetration()
	{
		float v = _magicPenetration;

		_addValueFromItems(InventoryItemStat._magicPenetration, v);
		// todo buffs

		return v;
	}

	public float _totalCriticalDamage()
	{
		float v = _criticalDamage;

		_addValueFromItems(InventoryItemStat._criticalDamage, v);
		// todo buffs

		return v;
	}

	public float _totalCriticalChance()
	{
		float v = _criticalChance;

		_addValueFromItems(InventoryItemStat._criticalChance, v);
		// todo buffs

		return v;
	}

	public float _totalCooldownReduction()
	{
		float v = _coolDownReduction;

		_addValueFromItems(InventoryItemStat._coolDownReduction, v);
		// todo buffs

		return v;
	}

	public float _totalSpellVamp()
	{
		float v = _spellVamp;

		_addValueFromItems(InventoryItemStat._spellVamp, v);
		// todo buffs

		return v;
	}

	public float _totalLifeSteal()
	{
		float v = _lifeSteal;

		_addValueFromItems(InventoryItemStat._lifeSteal, v);
		// todo buffs

		return v;
	}

	public int _totalHpMax()
	{
		int v = HpMax;

		_addValueFromItems(InventoryItemStat._hp, v);
		// todo buffs

		return v;
	}
}
