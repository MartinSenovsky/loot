using UnityEngine;
using System.Collections;

public class ErrorManager : MonoBehaviour
{
	static public void _error(string msg)
	{
		Debug.LogError(msg);
	}
}
