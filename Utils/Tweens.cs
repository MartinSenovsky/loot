using System;
using System.Collections.Generic;
using DG.Tweening;
using Holoville.HOTween;
using UnityEngine;
using Tweener = Holoville.HOTween.Tweener;
using TweenParms = Holoville.HOTween.TweenParms;

namespace Assets.M.Scripts.Utils
{
	class Tweens
	{
		static private void _completeTweensOf(Transform transform)
		{
			List<Tweener> list = HOTween.GetTweenersByTarget(transform, false);
			foreach (Tweener tweener in list)
			{
				tweener.Complete();
			}
		}

		static public void _showUpWithBump(Transform transform, float timeSec = 0.5f)
		{
			_completeTweensOf(transform);

			transform.localScale = new Vector3(1, 1, 1);
			HOTween.From(transform, timeSec, new TweenParms().Prop("localScale", new Vector3(2, 2, 2)).Ease(EaseType.EaseInOutBack));
		}


		static public void _hideWithBump(Transform transform, float timeSec = 0.5f)
		{
			_completeTweensOf(transform);
			HOTween.To(transform, timeSec, new TweenParms().Prop("localScale", new Vector3(0.001f, 0.001f, 0.001f)).Ease(EaseType.EaseInOutBack));
		}


		static public void _show(Transform transform, float timeSec = 0.5f)
		{
			_completeTweensOf(transform);
			HOTween.To(transform, timeSec, new TweenParms().Prop("localScale", new Vector3(1, 1, 1)));
		}


		static public void _showUnitInventory(Transform transform, float timeSec = 0.5f)
		{
			_completeTweensOf(transform);
			_hideInstant(transform);
			transform.localPosition = new Vector3(187, -250, 0);
			HOTween.To(transform, timeSec, new TweenParms().Prop("localScale", new Vector3(1, 1, 1)).Prop("localPosition", new Vector3(187, 110, 0)));
		}


		static public void _hideUnitInventory(Transform transform, float timeSec = 0.5f)
		{
			_completeTweensOf(transform);
			_showInstant(transform);
			transform.localPosition = new Vector3(187, 110, 0);
			HOTween.To(transform, timeSec, new TweenParms().Prop("localScale", new Vector3(0.001f, 0.001f, 0.001f)).Prop("localPosition", new Vector3(187, -250, 0)));
		}


		static public void _showUnitStats(Transform transform, float timeSec = 0.5f)
		{
			_completeTweensOf(transform);
			_hideInstant(transform);
			transform.localPosition = new Vector3(0, -200, 0);
			HOTween.To(transform, timeSec, new TweenParms().Prop("localScale", new Vector3(1, 1, 1)).Prop("localPosition", new Vector3(-100, -60, 0)));
		}


		static public void _hideUnitStats(Transform transform, float timeSec = 0.5f)
		{
			_completeTweensOf(transform);
			_showInstant(transform);
			transform.localPosition = new Vector3(-100, -60, 0);
			HOTween.To(transform, timeSec, new TweenParms().Prop("localScale", new Vector3(0.001f, 0.001f, 0.001f)).Prop("localPosition", new Vector3(0, -200, 0)));
		}


		static public void _hideInstant(Transform transform)
		{
			transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
		}


		static public void _showInstant(Transform transform)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
	}
}
