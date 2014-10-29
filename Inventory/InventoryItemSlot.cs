using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{

	public Inventory _parentInventory;
	public Image _itemImage;
	public Image _bgImage;

	private InventoryItem _inventoryItem;

	void Start()
	{
		_bgImage = GetComponent<Image>();
		_parentInventory = GetComponentInParent<Inventory>();
		_itemImage = transform.FindChild("ItemImage").GetComponent<Image>();
	}

	void Update()
	{

	}


	public InventoryItem _getItem()
	{
		if (_inventoryItem)
		{
			return _inventoryItem;
		}
		return null;
	}


	public bool _hasItem()
	{
		if (_inventoryItem)
		{
			return true;
		}
		return false;
	}


	public void _addItem(InventoryItem inventoryItem)
	{
		// reference
		_inventoryItem = inventoryItem;

		// child
		_inventoryItem = inventoryItem;
		_inventoryItem.transform.parent = transform;
		_inventoryItem.transform.localPosition = Vector3.zero;

		// image
		_itemImage.sprite = _inventoryItem._sprite;
		_itemImage.enabled = true;

		// report to inventory
		_parentInventory._onItemAddedOrRemoved(inventoryItem, true);
	}


	public InventoryItem _removeItem()
	{
		InventoryItem tempInventoryItem = _inventoryItem;

		_inventoryItem.transform.parent = null;
		_inventoryItem = null;

		_itemImage.enabled = false;

		// report to inventory
		_parentInventory._onItemAddedOrRemoved(tempInventoryItem, false);

		return tempInventoryItem;
	}
}
