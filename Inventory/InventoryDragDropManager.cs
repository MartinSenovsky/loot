using Holoville.HOTween;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryDragDropManager : MonoBehaviour
{
	[HideInInspector]
	public static InventoryDragDropManager _instance;

	public Sprite _mouseOverSprite;
	public Sprite _mouseOutSprite;
	public Sprite _mouseOverGreenSprite;
	public Sprite _mouseOverRedSprite;

	public Image _overlayDragDropImage;
	public Image _overlaySwapImage;

	public bool _dragging;
	private InventoryItem _draggedInventoryItem;
	public InventoryItemSlot _overSlot;


	private InventoryItemSlot _cancelTweenItemSlot;
	private InventoryItemSlot _swapTweenItemSlotFrom;
	private InventoryItem _swappedInventoryItem;


	void Start()
	{
		_instance = this;
	}

	void Update()
	{
		if (_dragging)
		{
			_overlayDragDropImage.transform.position = Input.mousePosition;
		}
	}


	public void _showItemInfo(InventoryItemSlot itemSlot)
	{
		if (itemSlot._getItem())
		{
			WeaponInfo._show(itemSlot._getItem());
		}
	}


	public void _hideItemInfo(InventoryItemSlot itemSlot)
	{
		if (itemSlot._getItem())
		{
			WeaponInfo._hide(itemSlot._getItem());
		}
	}


	public void _startDrag(InventoryItemSlot itemSlot)
	{
		_overlayDragDropImage.sprite = itemSlot._itemImage.sprite;
		_dragging = true;
		_draggedInventoryItem = itemSlot._removeItem();
	}


	public void _cancelDrag(InventoryItemSlot itemSlot)
	{
		_dragging = false;
		_tweenImageToPosition(_overlayDragDropImage, itemSlot);
	}


	public void _tweenImageToPosition(Image image, InventoryItemSlot itemSlot)
	{
		_cancelTweenItemSlot = itemSlot;

		Vector3 pos = Camera.main.WorldToScreenPoint(itemSlot.transform.position);
		HOTween.To(image.transform, 0.3f, new TweenParms().Prop("position", pos).OnComplete(_tweenImageComplete));
	}


	void _tweenImageComplete()
	{
		_cancelTweenItemSlot._addItem(_draggedInventoryItem);
		_clearOverlayDragDropImage();
		_cancelTweenItemSlot = null;
		_draggedInventoryItem = null;
	}


	public void _finishDrag(InventoryItemSlot from, InventoryItemSlot to)
	{
		_dragging = false;
		if (to._hasItem())
		{
			if (_canSwap(from, to))
			{
				_swapItems(from, to);
			}
			else
			{
				_cancelDrag(from);
			}
		}
		else
		{
			to._addItem(_draggedInventoryItem);
			_draggedInventoryItem = null;
			_clearOverlayDragDropImage();
		}
	}

	private void _swapItems(InventoryItemSlot from, InventoryItemSlot to)
	{
		_swapTweenItemSlotFrom = from;
		_swappedInventoryItem = to._removeItem();

		to._addItem(_draggedInventoryItem);
		_draggedInventoryItem = null;
		_clearOverlayDragDropImage();

		_overlaySwapImage.sprite = _swappedInventoryItem._sprite;
		_overlaySwapImage.transform.position = Camera.main.WorldToScreenPoint(to.transform.position);

		Vector3 pos = Camera.main.WorldToScreenPoint(from.transform.position);
		HOTween.To(_overlaySwapImage.transform, 0.3f, new TweenParms().Prop("position", pos).OnComplete(_onSwapTweenComplete));
	}


	private void _onSwapTweenComplete()
	{
		_swapTweenItemSlotFrom._addItem(_swappedInventoryItem);

		_swapTweenItemSlotFrom = null;
		_swappedInventoryItem = null;
		_clearOverlaySwapImage();
	}


	private bool _canSwap(InventoryItemSlot from, InventoryItemSlot to)
	{
		return true;
	}


	private void _clearOverlayDragDropImage()
	{
		_overlayDragDropImage.sprite = null;
		_overlayDragDropImage.transform.Translate(-1000, 0, 0);
	}


	private void _clearOverlaySwapImage()
	{
		_overlaySwapImage.sprite = null;
		_overlaySwapImage.transform.Translate(-1000, 0, 0);
	}


	public void _onPointerEnter(InventoryItemSlot itemSlot)
	{
		_overSlot = itemSlot;

		if (_dragging)
		{
			if (Random.Range(0, 2) == 0)
			{
				itemSlot._bgImage.sprite = _mouseOverGreenSprite;
			}
			else
			{
				itemSlot._bgImage.sprite = _mouseOverRedSprite;
			}
		}
		else
		{
			itemSlot._bgImage.sprite = _mouseOverSprite;
			_showItemInfo(itemSlot);
		}
	}

	public void _onPointerExit(InventoryItemSlot itemSlot)
	{
		_overSlot = null;
		itemSlot._bgImage.sprite = _mouseOutSprite;

		if (_dragging)
		{

		}
		else
		{
			_hideItemInfo(itemSlot);
		}
	}

	public void _onPointerDown(InventoryItemSlot itemSlot)
	{
		_hideItemInfo(itemSlot);

		if (_canDrag(itemSlot))
		{
			_startDrag(itemSlot);
		}
	}

	private bool _canDrag(InventoryItemSlot itemSlot)
	{
		if (itemSlot._hasItem() == false)
		{
			return false;
		}

		return true;
	}

	public void _onPointerUp(InventoryItemSlot itemSlot)
	{
		if (_dragging)
		{
			if (_overSlot)
			{
				_finishDrag(itemSlot, _overSlot);
				_showItemInfo(_overSlot);
			}
			else
			{
				_cancelDrag(itemSlot);
			}
		}
	}
}
