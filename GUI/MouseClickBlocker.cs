using Assets.M.Scripts.Utils;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseClickBlocker : MonoBehaviour
{


	void Start()
	{
		GetComponent<RectTransform>().anchoredPosition = new Vector2();
		_tweenOut();
	}

	void Update()
	{

	}


	public void _tweenIn(float time = 0.5f)
	{
		Tweens._alphaIn(GetComponent<Image>(), 0.9f, time);
	}


	public void _tweenOut(float time = 0.5f)
	{
		Tweens._alphaOut(GetComponent<Image>(), time);
	}
}
