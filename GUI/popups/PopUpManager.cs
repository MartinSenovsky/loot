using System.Collections.Generic;
using Assets.M.Scripts.GUI;
using Assets.M.Scripts.Utils;
using UnityEngine;
using System.Collections;

public class PopUpManager : MonoBehaviour
{
	public static PopUpManager _instance;


	public MouseClickBlocker _mouseClickBlocker;



	void Start()
	{
		_instance = this;
	}

	void Update()
	{

	}


	public void _showPopup(IPopup popup)
	{
		if (popup._blockAllOthers())
		{
			_mouseClickBlocker._tweenIn();
		}

		popup._openAnim();
	}


	public void _closePopup(IPopup popup)
	{
		if (popup._blockAllOthers())
		{
			_mouseClickBlocker._tweenOut();
		}

		popup._closeAnim();
	}
}
