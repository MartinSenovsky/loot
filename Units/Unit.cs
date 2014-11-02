using Holoville.HOTween;
using SULogger.Primitives;
using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
	public static string _TAG = "unit";

	[HideInInspector]
	public UnitHud _unitHud;

	[HideInInspector]
	public UnitStats _unitStats;

	[HideInInspector]
	public TurnAI _turnAI;

	[HideInInspector]
	public GameObject _model;

	[HideInInspector]
	public GameObject _unitRoot;

	public Signal<Unit> _signalTurnCompleted = new Signal<Unit>();
	public Signal _signalAttackAnimDamagePoint = new Signal();
	public Signal _signalAttackAnimFinished = new Signal();

	public float _height;

	public Animator _animator;

	void Start()
	{
		_unitRoot = transform.parent.gameObject;

		_unitHud = GetComponent<UnitHud>();
		_unitStats = GetComponent<UnitStats>();
		_turnAI = GetComponent<TurnAI>();

		_model = _getModelGameObject();
		_model.transform.tag = _TAG;

		_height = _model.GetComponentInChildren<Renderer>().bounds.extents.y * 2;

		_animator = _model.GetComponent<Animator>();

		transform.Translate(0, 2, 0);
	}


	private GameObject _getModelGameObject()
	{
		foreach (Transform child in transform.parent)
		{
			if (child.GetComponent<Unit>())
			{
				continue;
			}

			return child.gameObject;
		}

		return null;
	}


	void Update()
	{

	}


	public void _doOnTurnAction()
	{
		_turnAI._signalTurnCompleted.AddOnce(_endTurn);
		_turnAI._makeTurn();
	}


	private void _endTurn()
	{
		_signalTurnCompleted.Dispatch(this);
	}


	public void _beforeAttackerAttack(Unit attacker)
	{

	}


	public void _playAttackAnim()
	{
		Invoke("_onAttackAnimDamagePoint", 0.1f);
		Invoke("_onAttackAnimDamagePoint", 0.2f);
		Invoke("_onAttackAnimFinished", 0.3f);
	}


	void _onAttackAnimDamagePoint()
	{
		_signalAttackAnimDamagePoint.Dispatch();
	}


	void _onAttackAnimFinished()
	{
		_signalAttackAnimFinished.Dispatch();
	}


	public void _playDieAnim()
	{
		HOTween.To(_model.transform, 1.0f, new TweenParms().Prop("rotation", new Quaternion(-90, 0, 0, 0)));
	}


	public void _playRotAnim()
	{
		float time = 1.0f;

		Vector3 pos = _model.transform.position;
		pos.y += 1;
		HOTween.To(_model.transform, time, new TweenParms().Prop("position", pos).OnComplete(_onRotAnimCompleted));
	}


	private void _onRotAnimCompleted()
	{

	}


	public void _onAttackHitEnemy(Unit enemy)
	{
		int damage = DamageCounter._attackDamage(this, enemy);

		enemy._unitStats.Hp -= damage;
		enemy._onAttackHitMe(this);

		if (enemy._unitStats.Hp <= 0)
		{
			_onAttackKilledEnem(enemy);
		}
	}

	private void _onAttackKilledEnem(Unit enemy)
	{
		// todo add bonus to special move charge
	}


	private void _onAttackHitMe(Unit attacker)
	{
		// show some blood effect
		Vector3 bloodSplatPosition = Random.onUnitSphere * 0.2f + _unitRoot.transform.position;
		bloodSplatPosition.y += 0.4f;
		GameMain._instance._effectManager._makeEffect(EffectManager._BLOOD_HIT, bloodSplatPosition, new Quaternion());


		// check if dead
		if (_unitStats.Hp <= 0)
		{
			_unitStats.Hp = 0;
			_playDieAnim();
		}
	}


	public void _lookAt(Vector3 pos)
	{
		Vector3 targetRotationPosition = pos;
		targetRotationPosition.y = transform.parent.transform.position.y;
		transform.parent.transform.LookAt(targetRotationPosition);
	}


	public void _lookAt(Unit unit)
	{
		if (unit == null)
		{
			return;
		}

		Vector3 targetRotationPosition = unit._unitRoot.transform.position;
		targetRotationPosition.y = unit._unitRoot.transform.position.y;
		unit._unitRoot.transform.LookAt(targetRotationPosition);
	}


	public void _destroy()
	{
		Destroy(_model.gameObject);
		Destroy(gameObject);
	}


	public void _heal(float amount, bool isAbsolute = true)
	{
		// convert to absolute value
		if (isAbsolute == false)
		{
			amount = amount * _unitStats.HpMax;
		}

		// add hp
		_unitStats.Hp += (int)amount;

		// clamp to max
		if (_unitStats.Hp > _unitStats.HpMax)
		{
			_unitStats.Hp = _unitStats.HpMax;
		}

		// play anim heal
		GameMain._instance._effectManager._makeEffect(EffectManager._HERO_SPAWN, _model.transform.position, new Quaternion());
	}
}
