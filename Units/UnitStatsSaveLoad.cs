using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.M.Scripts.Utils;

public class UnitStatsSaveLoad
{

	static public void _saveUnitStats(UnitStats stats)
	{
		string uid = stats._uid;

		_saveString("Name", uid, stats.Name);
		_saveFloat("ActionMax", uid, stats.ActionMax);
		_saveInt("HpMax", uid, stats.HpMax);
		_saveInt("Hp", uid, stats.Hp);
		_saveString("PrefabName", uid, stats._prefabName);
		_saveInt("Level", uid, stats.Level);
		_saveInt("Xp", uid, stats.Exp);


		_saveInt("_attackDamage", uid, stats._attackDamage);
		_saveFloat("_armorPenetration", uid, stats._armorPenetration);
		_saveInt("_attackSpeed", uid, stats._attackSpeed);
		_saveFloat("_criticalChance", uid, stats._criticalChance);
		_saveFloat("_criticalDamage", uid, stats._criticalDamage);
		_saveFloat("_lifeSteal", uid, stats._lifeSteal);
		
		_saveInt("_armor", uid, stats._armor);
		_saveInt("_hpRegen", uid, stats._hpRegen);
		_saveInt("_magicResistance", uid, stats._magicResistance);

		_saveInt("_abilityDamage", uid, stats._abilityDamage);
		_saveFloat("_coolDownReduction", uid, stats._coolDownReduction);
		_saveFloat("_magicPenetration", uid, stats._magicPenetration);
		_saveFloat("_spellVamp", uid, stats._spellVamp);

	}


	static public void _loadUnitStats(UnitStats stats)
	{
		string uid = stats._uid;

		stats.Name = _loadString("Name", uid);
		stats.ActionMax = _loadFloat("ActionMax", uid);
		stats.HpMax = _loadInt("HpMax", uid);
		stats.Hp = _loadInt("Hp", uid);
		stats._prefabName = _loadString("PrefabName", uid);
		stats.Level = _loadInt("Level", uid);
		stats.Exp = _loadInt("Xp", uid);


		stats._attackDamage = _loadInt("_attackDamage", uid);
		stats._armorPenetration = _loadInt("_armorPenetration", uid);
		stats._attackSpeed = _loadInt("_attackSpeed", uid);
		stats._criticalChance = _loadFloat("_criticalChance", uid);
		stats._criticalDamage = _loadFloat("_criticalDamage", uid);
		stats._lifeSteal = _loadFloat("_lifeSteal", uid);

		stats._armor = _loadInt("_armor", uid);
		stats._hpRegen = _loadInt("_hpRegen", uid);
		stats._magicResistance = _loadInt("_magicResistance", uid);

		stats._abilityDamage = _loadInt("_abilityDamage", uid);
		stats._coolDownReduction = _loadFloat("_coolDownReduction", uid);
		stats._magicPenetration = _loadInt("_magicPenetration", uid);
		stats._spellVamp = _loadFloat("_spellVamp", uid);
	}


	static private void _saveInt(string statName, string uid, int v)
	{
		MSaveLoader._saveInt("UNIT_" + statName + "_" + uid, v);
	}


	static private int _loadInt(string statName, string uid)
	{
		return MSaveLoader._getInt("UNIT_" + statName + "_" + uid);
	}


	static private void _saveFloat(string statName, string uid, float v)
	{
		MSaveLoader._saveFloat("UNIT_" + statName + "_" + uid, v);
	}


	static private float _loadFloat(string statName, string uid)
	{
		return MSaveLoader._getFloat("UNIT_" + statName + "_" + uid);
	}


	static private void _saveString(string statName, string uid, string v)
	{
		MSaveLoader._saveString("UNIT_" + statName + "_" + uid, v);
	}


	static private string _loadString(string statName, string uid)
	{
		return MSaveLoader._getString("UNIT_" + statName + "_" + uid);
	}
}

