using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{

		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text;
//		text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		text = string.Format("{0:0.}", fps);
		GetComponent<Text>().text = text;
	}
}