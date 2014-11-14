using Assets.M.Scripts.Utils;
using SULogger.Primitives;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleStartButton : MonoBehaviour
{
	public Text _levelText;
	public Button _levelPlusButton;
	public Button _levelMinusButton;

	[HideInInspector] 
	public Signal<int> _signalStartBattlePressed = new Signal<int>();

	private int _level;
	private int _maxLevelUnlocked;

	void Start()
	{
		_level = MSaveLoader._getInt(MSaveLoader.LAST_SELECTED_LEVEL);
		_maxLevelUnlocked = MSaveLoader._getInt(MSaveLoader.LAST_UNLOCKED_LEVEL);
	}

	void Update()
	{

	}


	public void _startPress()
	{
		_signalStartBattlePressed.Dispatch(_level);
	}


	public void _plusPress()
	{
		if (_level < _maxLevelUnlocked)
		{
			_level += 1;
			MSaveLoader._saveInt(MSaveLoader.LAST_SELECTED_LEVEL, _level);
		}
	}


	public void _minusPress()
	{
		if (_level > 0)
		{
			_level -= 1;
			MSaveLoader._saveInt(MSaveLoader.LAST_SELECTED_LEVEL, _level);
		}
	}
}
