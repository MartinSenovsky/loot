using Assets.M.Scripts.Utils;
using UnityEngine;
using System.Collections;

public class InventoryInputEventReceiver : MonoBehaviour
{

	private InventoryDragDropManager _dragDropManager;

	void Start()
	{
		DelayedCall.To(this, _lateStart, 0.1f);
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
		_dragDropManager._onPointerDown(itemSlot);
	}


	public void _onPointerUp(InventoryItemSlot itemSlot)
	{
		_dragDropManager._onPointerUp(itemSlot);
	}
}
