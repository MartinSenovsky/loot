using UnityEngine;
using System.Collections;

public class UnitsManager : MonoBehaviour
{
	public GameMain _gameMain;



	void Start()
	{
		_gameMain = GetComponent<GameMain>();
	}

	void Update()
	{

	}

	public void _healAll()
	{
		foreach (Unit unit in _gameMain._units)
		{
			unit._heal(1, false);
		}

	}
}
