﻿using Assets.M.Scripts.Utils;
using Holoville.HOTween;
using SULogger.Primitives;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitHud : MonoBehaviour
{
	public Signal _signalInventoryToggled = new Signal();


	// menu hud
	public GameObject MenuPanelGameObject;

	// game hud
	public GameObject GamePanelGameObject;
	public Text NameText;
	public Text LevelText;
	public Slider HpSlider;
	public Slider ActionSlider;

	[HideInInspector]
	public bool _isInventoryVisible = false;
	[HideInInspector]
	public bool _isStatsVisible = false;

	// inventory
	public GameObject _inventoryPanelGameObject;

	private Unit _unit;
	[HideInInspector] 
	public UnitStats _unitStats;

	[HideInInspector] 
	public Inventory _inventory;

	[HideInInspector]
	public float _nothing;

	void Start()
	{
		_unit = GetComponent<Unit>();

		_unitStats = GetComponent<UnitStats>();
		_unitStats._hud = this;

		_inventory = GetComponentInChildren<Inventory>();
		_inventory._hud = this;


		NameText.text = _unitStats._Name;
		LevelText.text = _unitStats._Level.ToString();

		Tweens._hideInstant(_inventoryPanelGameObject.transform);

		HOTween.To(this, 0.1f, new TweenParms().Prop("_nothing", 0).OnComplete(_updateHeightOfHud));
	}


	void _updateHeightOfHud()
	{
		transform.localPosition = new Vector3(0, _unit._height + 0.5f, 0);
	}


	void Update()
	{
		HpSlider.value = (float)_unitStats._hp / (float)_unitStats._HpMax * 100f;
		ActionSlider.value = (float)_unitStats._Action / (float)_unitStats._ActionMax * 100f;
	}


	public void _showGameHud()
	{
		_showNoHud();
		GamePanelGameObject.SetActive(true);
	}


	public void _showMenuHud()
	{
		MenuPanelGameObject.SetActive(true);
		GamePanelGameObject.SetActive(false);
	}


	public void _showNoHud()
	{
		MenuPanelGameObject.SetActive(false);
		GamePanelGameObject.SetActive(false);

		if (_isInventoryVisible)
		{
			_toggleInventory();
		}
		if (_isStatsVisible)
		{
			_toggleStats();
		}
	}


	public void _toggleInventory()
	{
		if (_isInventoryVisible)
		{
			Tweens._hideUnitInventory(_inventoryPanelGameObject.transform);
		}
		else
		{
			Tweens._showUnitInventory(_inventoryPanelGameObject.transform);
		}

		_isInventoryVisible = !_isInventoryVisible;

		_signalInventoryToggled.Dispatch();
	}


	public void _toggleStats()
	{
		//if(EventSystemManager.currentSystem.firstSelectedObject != EventSystemManager.currentSystem.currentSelectedObject) return;

		if (_isStatsVisible)
		{
			Tweens._hideUnitStats(_unitStats._statPanel.transform);
		}
		else
		{
			_unitStats._statPanel._updateStatLines(_unitStats);
			Tweens._showUnitStats(_unitStats._statPanel.transform);
		}

		_isStatsVisible = !_isStatsVisible;
	}


	public void _showAiConfiguration()
	{
		//if (EventSystemManager.currentSystem.firstSelectedObject != EventSystemManager.currentSystem.currentSelectedObject) return;

		GameMain._instance._guiManager._showAiConfiguration(_unit);
	}
}
