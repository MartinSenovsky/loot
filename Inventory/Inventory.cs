using Assets.M.Scripts.Utils;
using Holoville.HOTween;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	public static Inventory _globalPlayersInventory;

	// generic
	public bool _isGlobalPlayersInventory;
	private List<InventoryItemSlot> _slots;

	// not global
	public Unit _owner;
	public int _minimalEqipmentItemLevel = 0;
	public Text _equipmentLevelText;

	// global only


	

	void Start()
	{
		// is global?
		if (_isGlobalPlayersInventory)
		{
			_globalPlayersInventory = this;
		}
		else
		{
			_setItemAbsorbEnabled(false, true);
		}

		// slots
		_slots = new List<InventoryItemSlot>();
		InventoryItemSlot[] slotsArray = GetComponentsInChildren<InventoryItemSlot>();
		foreach (InventoryItemSlot inventoryItemSlot in slotsArray)
		{
			_slots.Add(inventoryItemSlot);
		}
	}

	void Update()
	{

	}


	public void _addItemAuto(InventoryItem item)
	{
		InventoryItemSlot slot = _getFirstFreeSlot();

		if (slot)
		{
			_addItemToSlot(item, slot);
		}
	}

	public void _addItemToSlot(InventoryItem item, InventoryItemSlot itemSlot)
	{
		if (itemSlot._hasItem())
		{
			Debug.LogWarning("Overriding item in itemSlot!!");
		}

		itemSlot._addItem(item);
	}

	public InventoryItem _removeItemFromSlot(InventoryItemSlot itemSlot)
	{
		if (itemSlot._hasItem() == false)
		{
			Debug.LogWarning("There's no item!!");
		}

		return itemSlot._removeItem();
	}


	public InventoryItemSlot _getFirstFreeSlot()
	{
		foreach (InventoryItemSlot slot in _slots)
		{
			if (slot._hasItem() == false)
			{
				return slot;
			}
		}

		return null;
	}


	public void _onItemAddedOrRemoved()
	{
		if (_isGlobalPlayersInventory)
		{
			
		}
		else
		{
			foreach (InventoryItemSlot slot in _slots)
			{
				if (slot._hasItem() == false)
				{
					_setItemAbsorbEnabled(false);
					return;
				}
			}

			// all slots are full - enable absorb
			_setItemAbsorbEnabled(true);
		}
	}


	private void _setItemAbsorbEnabled(bool enabled, bool instant = false)
	{
		GameObject absorbItemsButtonGameObject = transform.FindChild("AbsorbItemsButton").gameObject;

		if (instant)
		{
			if (enabled)
			{
				Tweens._showInstant(absorbItemsButtonGameObject.transform);
			}
			else
			{
				Tweens._hideInstant(absorbItemsButtonGameObject.transform);
			}
			return;
		}
		

		bool showEnableAnim = false;
		bool showDisableAnim = false;

		if (enabled && absorbItemsButtonGameObject.activeSelf == false)
		{
			showEnableAnim = true;
		}
		else if (enabled == false && absorbItemsButtonGameObject.activeSelf)
		{
			showDisableAnim = true;
		}

		if (showEnableAnim)
		{
			Tweens._showUpWithBump(absorbItemsButtonGameObject.transform);
		}
		else if (showDisableAnim)
		{
			Tweens._hideWithBump(absorbItemsButtonGameObject.transform);
		}
	}


	public void _absorbInventoryItems()
	{
		foreach (InventoryItemSlot slot in _slots)
		{
			if (slot._hasItem() == false)
			{
				return;
			}
		}


		// absorb items
		foreach (InventoryItemSlot slot in _slots)
		{
			InventoryItem item = slot._removeItem();
			// ToDo item into unit's stats
		}

		// increase equipment minimal item level
		_minimalEqipmentItemLevel += 1;
		_equipmentLevelText.text = _minimalEqipmentItemLevel.ToString();
	}
}
