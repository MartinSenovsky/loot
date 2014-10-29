using System.Collections.Generic;
using Holoville.HOTween;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponInfo : MonoBehaviour
{
	public static WeaponInfo _instance;

	private static bool _isHiding;
	
	public Text _nameText;
	public Text _subNameText;
	public Text _damageText;
	public Image _image;

	public List<Text> _statTexts; 

	private RectTransform _panelTransform;
	// todo show list of item stats


	void Start()
	{
		_instance = this;
		_panelTransform = GetComponent<RectTransform>();
		transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
	}

	void Update()
	{

	}

	private void _updateStats(List<InventoryItemStat> stats)
	{
		int numVisible = 0;

		for (int i = 0; i < _statTexts.Count; i++)
		{
			_statTexts[i].text = "";
		}


		for (int i = 0; i < stats.Count; i++)
		{
			InventoryItemStat stat = stats[i];
			Text statText = _statTexts[i];
			numVisible += 1;

			statText.text = stat._description();
		}

		_panelTransform.sizeDelta = new Vector2(155, 100 + numVisible * 30);
	}











	static public void _show(InventoryItem item)
	{
		_instance._image.sprite = item._sprite;
		_instance._damageText.text = item._damage._min + "-" + item._damage._max;

		_instance._updateStats(item._stats);

		Vector3 pos = Camera.main.WorldToScreenPoint(item.transform.position);
		_instance.transform.position = pos;
		pos.x += 120;

		int halfWidth = (int)(_instance.transform.parent.GetComponent<ReferenceResolution>().resolution.x/2);
		int halfHeight = (int)(_instance.transform.parent.GetComponent<ReferenceResolution>().resolution.y / 2);

		// clamp to screen
		if (pos.x < -halfWidth)
		{
			pos.x = -halfWidth;
			Debug.Log("too left of screen");
		}
		else if (pos.x > halfWidth)
		{
			pos.x = halfWidth;
			Debug.Log("too right of screen");
		}

		if (pos.y < -halfHeight)
		{
			pos.y = -halfHeight;
			Debug.Log("too down of screen");
		}
		else if (pos.y > halfHeight + _instance._panelTransform.sizeDelta.y - 5)
		{
			pos.y = halfHeight + _instance._panelTransform.sizeDelta.y - 5;
			Debug.Log("too up of screen");
		}

		pos.x = (int)pos.x;
		pos.y = (int)pos.y;
		pos.z = (int)pos.z;

//		_instance.transform.position = pos;
//		HOTween.To(_instance.transform, 0.3f, new TweenParms().Prop("localScale", new Vector3(1, 1, 1)));
		HOTween.To(_instance.transform, 0.3f, new TweenParms().Prop("localScale", new Vector3(1, 1, 1)).Prop("position", pos));
	}


	static public void _hide(InventoryItem item)
	{
		_isHiding = true;
		Vector3 pos = Camera.main.WorldToScreenPoint(item.transform.position);
		HOTween.To(_instance.transform, 0.3f, new TweenParms().Prop("localScale", new Vector3(0.001f, 0.001f, 0.001f)).Prop("position", pos).OnComplete(_onHidden));	
	}

	private static void _onHidden()
	{
		_isHiding = false;
	}
}
