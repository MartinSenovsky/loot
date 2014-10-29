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

	public float Action;
	public float ActionMax;

	public bool _doActionNow;

	[HideInInspector]
	public string _prefabName;
	[HideInInspector]
	public string _uid;


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

	public int _totalAttackDamage()
	{
		int v = _attackDamage;

		// todo items
		// todo buffs

		return v;
	}

	public int _totalArmor()
	{
		int v = _armor;

		// todo items
		// todo buffs

		return v;
	}

	public float _totalArmorPenetration()
	{
		float v = _armorPenetration;

		// todo items
		// todo buffs

		return v;
	}

	public int _totalAttackSpeed()
	{
		int v = _attackSpeed;

		// todo items
		// todo buffs

		return v;
	}

	public int _totalAbilityPower()
	{
		int v = _abilityDamage;

		// todo items
		// todo buffs

		return v;
	}

	public int _totalHpRegen()
	{
		int v = _hpRegen;

		// todo items
		// todo buffs

		return v;
	}

	public int _totalMagicResist()
	{
		int v = _magicResistance;

		// todo items
		// todo buffs

		return v;
	}

	public float _totalMagicPenetration()
	{
		float v = _magicPenetration;

		// todo items
		// todo buffs

		return v;
	}

	public float _totalCriticalDamage()
	{
		float v = _criticalDamage;

		// todo items
		// todo buffs

		return v;
	}

	public float _totalCriticalChance()
	{
		float v = _criticalChance;

		// todo items
		// todo buffs

		return v;
	}

	public float _totalCooldownReduction()
	{
		float v = _coolDownReduction;

		// todo items
		// todo buffs

		return v;
	}

	public float _totalSpellVamp()
	{
		float v = _spellVamp;

		// todo items
		// todo buffs

		return v;
	}

	public float _totalLifeSteal()
	{
		float v = _lifeSteal;

		// todo items
		// todo buffs

		return v;
	}

	public int _totalHpMax()
	{
		int v = HpMax;

		// todo items
		// todo buffs

		return v;
	}
}
