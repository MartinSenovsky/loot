using Holoville.HOTween;
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
		HOTween.To(rectTransform, 0.9f, new TweenParms().Prop("localPosition", new Vector3(0, 0, 0)));
	}


	public void _hideGlobalInventory()
	{
		RectTransform rectTransform = _globalInventory.transform.parent.GetComponent<RectTransform>();
		HOTween.To(rectTransform, 0.9f, new TweenParms().Prop("localPosition", new Vector3(0, -198.52f, 0.14624f)));
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
