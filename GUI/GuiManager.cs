using Holoville.HOTween;
using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour
{

	private Unit _mouseDownUnit;

	private bool _unitDeselectionEnabled;

	public MenuManager _menuManager;
	public EndFightPanel _endFightPanel;

	void Start()
	{
		_menuManager = GameObject.Find("MenuOverlay").GetComponent<MenuManager>();
		_endFightPanel = GameObject.Find("EndFightPanel").GetComponent<EndFightPanel>();

		Invoke("_hideMenu", 0.1f);

		Input.simulateMouseWithTouches = true;
	}


	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Unit unit = _getUnitUnderMouse();

			_unitDeselectionEnabled = true;
			Invoke("_disableUnitDeselecting", 0.2f);

			if (unit)
			{
				_mouseDownUnit = unit;
			}
		}
		else if (Input.GetMouseButtonUp(0) && _unitDeselectionEnabled)
		{
			Unit unit = _getUnitUnderMouse();
			if (unit && unit == _mouseDownUnit)
			{
				_selectUnit(unit);
			}
			else
			{
				_selectUnit(null);
			}
			_mouseDownUnit = null;
		}
	}


	public void _disableUnitDeselecting()
	{
		_unitDeselectionEnabled = false;
	}


	public void _showFightOverMessage(string text)
	{
		_endFightPanel._labelText.text = text;
		_endFightPanel._labelText.gameObject.SetActive(true);
		_endFightPanel._labelAnimator.Play("VictoryText");
	}


	private Unit _getUnitUnderMouse()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, 100))
		{
			if (hit.transform.tag == Unit._TAG)
			{
				Unit unit = hit.transform.parent.GetComponentInChildren<Unit>();
				return unit;
			}
		}

		return null;
	}


	public void _startMenu()
	{
		foreach (Unit unit in GameMain._instance._units)
		{
			unit._unitHud._showMenuHud();
			unit._unitHud._signalInventoryToggled.Add(_menuManager._onUnitInventoryToggled);
		}

		_menuManager.gameObject.SetActive(true);

		// test item
		_addTestItem();
		_addTestItem();
		_addTestItem();
		_addTestItem();
		_addTestItem();
		_addTestItem();
		_addTestItem();
		_addTestItem();
		_addTestItem();
		_addTestItem();
		_addTestItem();
		_addTestItem();
		_addTestItem();
	}


	public void _hideMenu()
	{
		_menuManager.gameObject.SetActive(false);

		foreach (Unit unit in GameMain._instance._units)
		{
			unit._unitHud._showNoHud();
		}

		foreach (Unit unit in GameMain._instance._enemies)
		{
			unit._unitHud._showNoHud();
		}
	}


	public void _startGame()
	{
		foreach (Unit unit in GameMain._instance._units)
		{
			unit._unitHud._showGameHud();
		}

		foreach (Unit unit in GameMain._instance._enemies)
		{
			unit._unitHud._showGameHud();
		}
	}


	private void _addTestItem()
	{
		InventoryItem testItem = InventoryItem._createNew();
		testItem._sprite = InventoryItemSprites._instance._getRandomSword();
		testItem._damage._min = Random.Range(100, 200);
		testItem._damage._max = Random.Range(testItem._damage._min + 10, 300);

		InventoryItemStat s = InventoryItemStat._createNew();
		s._statName = InventoryItemStat._attackDamage;
		s._statValue = Random.Range(100, 500);
		s._isAbsoluteValue = true;
		testItem._addStat(s);

		Inventory._globalPlayersInventory._addItemAuto(testItem);
	}


	public void _selectUnit(Unit unit)
	{
		return;
		if (unit == null)
		{
			CameraDragOrbit._instance._tweenTo(CameraDragOrbit._instance._defaultTargetPosition, CameraDragOrbit._instance.distance, 0.5f);
		}
		else
		{
			CameraDragOrbit._instance._tweenTo(unit.transform.position, CameraDragOrbit._instance.distanceMin + 1.0f, 0.5f);

			Vector3 cameraFakePos = CameraDragOrbit._instance.transform.position;
			cameraFakePos.y = unit._unitRoot.transform.position.y;

			Vector3 cameraUnitPosition = unit._unitRoot.transform.position;
			cameraUnitPosition = Vector3.MoveTowards(cameraUnitPosition, cameraFakePos, 2);

			HOTween.To(CameraDragOrbit._instance.transform, 1.5f, new TweenParms().Prop("position", cameraUnitPosition));
		}
	}


	public bool _isAnyUnitInventoryVisible()
	{
		foreach (Unit unit in GameMain._instance._units)
		{
			if (unit._unitHud._isInventoryVisible)
			{
				return true;
			}
		}

		return false;
	}
}
