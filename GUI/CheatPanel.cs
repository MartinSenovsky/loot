using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CheatPanel : MonoBehaviour
{
	public GameMain GameMain;
	
	void Start()
	{

	}


	void Update()
	{

	}


	public void _healAll()
	{
		GameMain._unitsManager._healAll();
	}


	public void _addRandomItem()
	{
		GameMain._guiManager._addTestItem();
	}
}
