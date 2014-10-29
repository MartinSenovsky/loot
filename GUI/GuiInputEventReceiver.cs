using UnityEngine;
using System.Collections;

public class GuiInputEventReceiver : MonoBehaviour
{

	public void _onPointerDown()
	{
		GameMain._instance._guiManager._disableUnitDeselecting();
		Invoke("_disableUnitDeselecting", 0.01f);
	}


	private void _disableUnitDeselecting()
	{
		GameMain._instance._guiManager._disableUnitDeselecting();
	}

	void Start()
	{

	}

	void Update()
	{

	}
}
