using System;
using Assets.M.Scripts.GUI;
using Assets.M.Scripts.Utils;
using UnityEngine;
using System.Collections;

public class AiConfigurationPanel : MonoBehaviour, IPopup
{

	private Unit _unit;

	void Start()
	{
		Tweens._hideInstant(gameObject.transform);
		GetComponent<RectTransform>().anchoredPosition = new Vector2();
	}

	void Update()
	{

	}

	public void _showAiConfiguration(Unit unit)
	{
		_unit = unit;
		PopUpManager._instance._showPopup(this);
	}


	public void _close()
	{
		PopUpManager._instance._closePopup(this);
	}


	public void _setAttack(String type)
	{
		_unit._turnAI._attackPriority = type;
	}


	public void _setProtect(String type)
	{
		_unit._turnAI._protectFrom = type;
	}

	public bool _blockAllOthers()
	{
		return true;
	}

	public void _openAnim()
	{
		Tweens._show(transform);
	}

	public void _closeAnim()
	{
		Tweens._hideWithBump(transform);
	}
}
