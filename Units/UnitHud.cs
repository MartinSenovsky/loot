using Assets.M.Scripts.Utils;
using SULogger.Primitives;
using UnityEngine;
using System.Collections;
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

	void Start()
	{
		_unit = GetComponent<Unit>();

		_unitStats = GetComponent<UnitStats>();
		_unitStats._hud = this;

		_inventory = GetComponentInChildren<Inventory>();
		_inventory._hud = this;


		NameText.text = _unitStats.Name;
		LevelText.text = _unitStats.Level.ToString();

		Tweens._hideInstant(_inventoryPanelGameObject.transform);
	}

	void Update()
	{
		HpSlider.value = (float)_unitStats.Hp / (float)_unitStats.HpMax * 100f;
		ActionSlider.value = (float)_unitStats.Action / (float)_unitStats.ActionMax * 100f;
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
}
