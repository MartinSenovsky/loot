using UnityEngine;
using System.Collections;

public class ColorManager : MonoBehaviour
{
	public Color _blue;
	public Color _red;
	public Color _green;
	public Color _gold;
	public Color _purple;

	public static ColorManager _instance;


	void Start()
	{
		_instance = this;
	}

	void Update()
	{

	}
}
