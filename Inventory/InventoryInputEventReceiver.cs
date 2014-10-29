using UnityEngine;
using System.Collections;

public class InventoryInputEventReceiver : MonoBehaviour
{

	private InventoryDragDropManager _dragDropManager;

	void Start()
	{
		Invoke("_lateStart", 0.1f);
	}


	void _lateStart()
	{
		_dragDropManager = InventoryDragDropManager._instance;
	}



	public void _onPointerEnter(InventoryItemSlot itemSlot)
	{
		_dragDropManager._onPointerEnter(itemSlot);
	}

	public void _onPointerExit(InventoryItemSlot itemSlot)
	{
		_dragDropManager._onPointerExit(itemSlot);
	}

	public void _onPointerDown(InventoryItemSlot itemSlot)
	{
		GameMain._instance._guiManager._disableUnitDeselecting();
		_dragDropManager._onPointerDown(itemSlot);
	}


	public void _onPointerUp(InventoryItemSlot itemSlot)
	{
		GameMain._instance._guiManager._disableUnitDeselecting();
		_dragDropManager._onPointerUp(itemSlot);
	}
}
