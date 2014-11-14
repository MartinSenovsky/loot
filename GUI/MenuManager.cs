using Holoville.HOTween;
using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	public Inventory _globalInventory;
	public BattleStartButton _BattleStartButton;

	void Start()
	{
		_BattleStartButton._signalStartBattlePressed.Add(_onBattleStartClicked);
	}

	void Update()
	{

	}


	public void _onBattleStartClicked(int level)
	{
		GameMain._instance._startBattle(level);
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
