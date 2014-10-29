using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class InventoryItemSprites : MonoBehaviour
{
	public static InventoryItemSprites _instance;

	public List<Sprite> _swordSprites; 

	void Start()
	{
		_instance = this;
	}

	void Update()
	{

	}

	internal Sprite _getRandomSword()
	{
		return _swordSprites[Random.Range(0, _swordSprites.Count)];
	}
}
