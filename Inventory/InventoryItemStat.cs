using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections;

public class InventoryItemStat : MonoBehaviour
{
	public const string _attackDamage = "_attackDamage";
	public const string _attackSpeed = "_attackSpeed";
	public const string _armorPenetration = "_armorPenetration";
	public const string _criticalChance = "_criticalChance";
	public const string _criticalDamage = "_criticalDamage";
	public const string _lifeSteal = "_lifeSteal";

	public const string _armor = "_armor";
	public const string _hp = "_hp";
	public const string _hpRegen = "_hpRegen";
	public const string _magicResistance = "_magicResistance";

	public const string _abilityDamage = "_abilityDamage";
	public const string _coolDownReduction = "_coolDownReduction";
	public const string _magicPenetration = "_magicPenetration";
	public const string _spellVamp = "_spellVamp";


	public string _statName;
	public float _statValue;
	public bool _isAbsoluteValue = true;

	public string _description()
	{
		string s = "";


		if (_statValue > 0)
		{
			s += "+" + _statValue;
		}
		else
		{
			s += _statValue;
		}

		if (_isAbsoluteValue == false)
		{
			s += "%";
		}

		s += " ";

		s += _nameToDisplayName(_statName);


		return s;
	}


	private string _nameToDisplayName(string name)
	{
		name = name.Substring(1);
		string first = name[0].ToString().ToUpper();
		name = name.Substring(1);
		name = first + name;
		name = Regex.Replace(name, "([a-z])([A-Z])", "$1 $2");
		return name;
	}


	static public InventoryItemStat _createNew()
	{
		GameObject statGameObject = Instantiate(Resources.Load("InventoryItemStat", typeof(GameObject))) as GameObject;
		InventoryItemStat stat = statGameObject.GetComponent<InventoryItemStat>();



		return stat;
	}
}
