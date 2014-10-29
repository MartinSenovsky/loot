using System.Collections.Generic;
using Assets.M.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatPanel : MonoBehaviour
{
	private List<UnitStatsStatLine> _statLines;


	void Start()
	{
		Tweens._hideInstant(transform);
		_statLines = new List<UnitStatsStatLine>();

		UnitStatsStatLine[] statsArray = GetComponentsInChildren<UnitStatsStatLine>();
		foreach (UnitStatsStatLine line in statsArray)
		{
			_statLines.Add(line);

			line.gameObject.SetActive(false);
		}
	}

	void Update()
	{

	}


	private UnitStatsStatLine _getUnusedStatLine()
	{
		foreach (UnitStatsStatLine line in _statLines)
		{
			if (line.gameObject.activeSelf == false)
			{
				return line;
			}
		}

		Debug.LogWarning("Need more stat lines");
		return null;
	}


	public void _addStatLine(string statName, float value, bool showPercent, Color color)
	{
		UnitStatsStatLine line = _getUnusedStatLine();
		line.gameObject.SetActive(true);
		line._set(statName, value, showPercent, color);
	}


	public void _removeStatLine(string name)
	{
		for( int i = _statLines.Count - 1; i >= 0; i--)
		{
			UnitStatsStatLine line = _statLines[i];

			if (line._name == name)
			{
				line.gameObject.SetActive(false);
			}
		}
	}

	public void _updateStatLines(UnitStats stats)
	{
		_makeOrUpdateStatLine("HP", stats._totalHpMax(), false, ColorManager._instance._green);
		_makeOrUpdateStatLine("HR", stats._totalHpRegen(), false, ColorManager._instance._green);
		_makeOrUpdateStatLine("XP", stats.Exp, false, ColorManager._instance._purple);

		_makeOrUpdateStatLine("AD", stats._totalAttackDamage(), false, ColorManager._instance._red);
		_makeOrUpdateStatLine("AS", stats._totalAttackSpeed(), false, ColorManager._instance._gold);
		_makeOrUpdateStatLine("AP", stats._totalArmorPenetration(), false, ColorManager._instance._blue);
		_makeOrUpdateStatLine("AC", stats._totalArmor(), false, ColorManager._instance._green);
		_makeOrUpdateStatLine("CD", stats._totalCriticalDamage(), false, ColorManager._instance._red);
		_makeOrUpdateStatLine("CC", stats._totalCriticalChance(), false, ColorManager._instance._gold);
		_makeOrUpdateStatLine("LS", stats._totalLifeSteal(), false, ColorManager._instance._red);

		_makeOrUpdateStatLine("AP", stats._totalAbilityPower(), false, ColorManager._instance._blue);
		_makeOrUpdateStatLine("MR", stats._totalMagicResist(), false, ColorManager._instance._green);
		_makeOrUpdateStatLine("MP", stats._totalMagicPenetration(), false, ColorManager._instance._red);
		_makeOrUpdateStatLine("SV", stats._totalSpellVamp(), false, ColorManager._instance._purple);

		// reactivate grid layout to sort stat lines
		GetComponent<GridLayoutGroup>().enabled = false;
		GetComponent<GridLayoutGroup>().enabled = true;
	}


	private void _makeOrUpdateStatLine(string statName, float value, bool showPercent, Color color)
	{
		foreach (UnitStatsStatLine line in _statLines)
		{
			if (line._name == statName)
			{
				line._set(statName, value, showPercent, color);

				if (value <= 0)
				{
					_removeStatLine(statName);
				}

				return;
			}
		}

		if (value > 0)
		{
			_addStatLine(statName, value, showPercent, color);
		}
	}
}
