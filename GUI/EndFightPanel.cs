using Assets.M.Scripts.GUI;
using Assets.M.Scripts.Utils;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndFightPanel : MonoBehaviour, IPopup
{

	public Text _labelText;
	public Animator _labelAnimator;

	void Start()
	{
		Tweens._hideInstant(transform);
	}

	void Update()
	{

	}


	public bool _blockAllOthers()
	{
		return true;
	}

	public void _openAnim()
	{
		
	}

	public void _closeAnim()
	{
		
	}
}
