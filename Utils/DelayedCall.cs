using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.M.Scripts.Utils
{
	class DelayedCall
	{
		public static void To(MonoBehaviour monoBehaviour, Action func, float delaySeconds)
		{
			monoBehaviour.Invoke(func.Method.Name, delaySeconds);
		}
	}
}
