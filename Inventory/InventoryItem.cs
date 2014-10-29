using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{

	public Damage _damage;
	public Sprite _sprite;

	public List<InventoryItemStat> _stats; 

	void Start ()
	{
		
	}
	
	
	void Update () {
	
	}


	public void _addStat(InventoryItemStat stat)
	{
		stat.transform.parent = transform;
		_stats.Add(stat);
	}


	public InventoryItemStat _getStat(String statName)
	{
		foreach (InventoryItemStat stat in _stats)
		{
			if (stat._statName == statName)
			{
				return stat;
			}
		}

		return null;
	}


	static public InventoryItem _createNew()
	{
		GameObject itemGameObject = Instantiate(Resources.Load("InventoryItem", typeof(GameObject))) as GameObject;
		InventoryItem item = itemGameObject.GetComponent<InventoryItem>();

		item._stats = new List<InventoryItemStat>();
		item._damage = new Damage();

		return item;
	}
}
