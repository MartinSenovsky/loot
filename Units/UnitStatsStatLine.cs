using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitStatsStatLine : MonoBehaviour
{

	public Text _textText;
	public Text _valueText;

	public string _name;
	public float _value;
	public bool _showPercent;
	public Color _color;

	void Start()
	{

	}

	void Update()
	{

	}


	public void _set(string name, float value, bool showPercent, Color color)
	{
		_name = name;
		_value = value;
		_showPercent = showPercent;
		_color = color;

		_textText.text = _name;
		_valueText.text = value.ToString();

		if (_showPercent)
		{
			_valueText.text += "%";
		}

		_textText.color = _color;
		_valueText.color = _color;
	}
}
