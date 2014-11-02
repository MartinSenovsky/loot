﻿using Holoville.HOTween;
using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	public Inventory _globalInventory;


	void Start()
	{

	}

	void Update()
	{

	}


	public void _onBattleStartClicked()
	{
		GameMain._instance._startGame();
	}


	public void _showGlobalInventory()
	{
		RectTransform rectTransform = _globalInventory.transform.parent.GetComponent<RectTransform>();
		HOTween.To(rectTransform, 0.9f, new TweenParms().Prop("anchoredPosition", new Vector2(0, -280)));
	}


	public void _hideGlobalInventory()
	{
		RectTransform rectTransform = _globalInventory.transform.parent.GetComponent<RectTransform>();
		HOTween.To(rectTransform, 0.9f, new TweenParms().Prop("anchoredPosition", new Vector2(0, -500)));
	}

	public void _onUnitInventoryToggled()
	{
		if (GameMain._instance._guiManager._isAnyUnitInventoryVisible())
		{
			_showGlobalInventory();
		}
		else
		{
			_hideGlobalInventory();
		}
	}
}
