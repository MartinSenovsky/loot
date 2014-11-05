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

		_saveString("_Name", uid, stats._Name);
		_saveFloat("_ActionMax", uid, stats._ActionMax);
		_saveInt("_HpMax", uid, stats._HpMax);
		_saveInt("_Hp", uid, stats._Hp);
		_saveString("PrefabName", uid, stats._prefabName);
		_saveInt("_Level", uid, stats._Level);
		_saveInt("Xp", uid, stats._Exp);


		_saveInt(InventoryItemStat._attackDamage, uid, stats._attackDamage);
		_saveFloat(InventoryItemStat._armorPenetration, uid, stats._armorPenetration);
		_saveInt(InventoryItemStat._attackSpeed, uid, stats._attackSpeed);
		_saveFloat(InventoryItemStat._criticalChance, uid, stats._criticalChance);
		_saveFloat(InventoryItemStat._criticalDamage, uid, stats._criticalDamage);
		_saveFloat(InventoryItemStat._lifeSteal, uid, stats._lifeSteal);
		
		_saveInt(InventoryItemStat._armor, uid, stats._armor);
		_saveInt(InventoryItemStat._hp, uid, stats._hp);
		_saveInt(InventoryItemStat._hpRegen, uid, stats._hpRegen);
		_saveInt(InventoryItemStat._magicResistance, uid, stats._magicResistance);

		_saveInt(InventoryItemStat._abilityDamage, uid, stats._abilityDamage);
		_saveFloat(InventoryItemStat._coolDownReduction, uid, stats._coolDownReduction);
		_saveFloat(InventoryItemStat._magicPenetration, uid, stats._magicPenetration);
		_saveFloat(InventoryItemStat._spellVamp, uid, stats._spellVamp);

	}


	static public void _loadUnitStats(UnitStats stats)
	{
		string uid = stats._uid;

		stats._Name = _loadString("_Name", uid);
		stats._ActionMax = _loadFloat("_ActionMax", uid);
		stats._HpMax = _loadInt("_HpMax", uid);
		stats._Hp = _loadInt("_Hp", uid);
		stats._prefabName = _loadString("PrefabName", uid);
		stats._Level = _loadInt("_Level", uid);
		stats._Exp = _loadInt("Xp", uid);


		stats._attackDamage = _loadInt(InventoryItemStat._attackDamage, uid);
		stats._armorPenetration = _loadInt(InventoryItemStat._armorPenetration, uid);
		stats._attackSpeed = _loadInt(InventoryItemStat._attackSpeed, uid);
		stats._criticalChance = _loadFloat(InventoryItemStat._criticalChance, uid);
		stats._criticalDamage = _loadFloat(InventoryItemStat._criticalDamage, uid);
		stats._lifeSteal = _loadFloat(InventoryItemStat._lifeSteal, uid);

		stats._armor = _loadInt(InventoryItemStat._armor, uid);
		stats._hpRegen = _loadInt(InventoryItemStat._hpRegen, uid);
		stats._hp = _loadInt(InventoryItemStat._hp, uid);
		stats._magicResistance = _loadInt(InventoryItemStat._magicResistance, uid);

		stats._abilityDamage = _loadInt(InventoryItemStat._abilityDamage, uid);
		stats._coolDownReduction = _loadFloat(InventoryItemStat._coolDownReduction, uid);
		stats._magicPenetration = _loadInt(InventoryItemStat._magicPenetration, uid);
		stats._spellVamp = _loadFloat(InventoryItemStat._spellVamp, uid);
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

