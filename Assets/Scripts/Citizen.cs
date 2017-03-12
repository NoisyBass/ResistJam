using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour {

	public enum CitizenState
	{
		WALK_IN,
		STAND,
		WALK_OUT
	}

	public enum CitizenDirection
	{
		RIGHT,
		LEFT
	}

	private CitizenState _state;

	private Vector3 _startPosition;
	private Vector3 _standPosition;
	private Vector3 _endPosition;

	private float _speed;
	private float _standingTime;
	private Vector3 _direction;
	private int _row;

	private bool _walkInEnded = false;
	private bool _standEnded = false;
	private bool _walkOutEnded = false;

	private EndCitizenEvent _endCitizenEvent;

	private SpriteRenderer _sprite;
    private Animator _anim;

	void Awake()
	{
		_endCitizenEvent = new EndCitizenEvent ();
		_sprite = GetComponent<SpriteRenderer> ();
        _anim = GetComponent<Animator>();
	}

	void OnEnable()
	{
		_sprite.sortingOrder = _row;
	}

	public void Init(Vector3 startPosition, Vector3 standPosition, Vector3 endPosition, CitizenDirection direction, float speed, float standingTime, int row)
	{
		_state = CitizenState.WALK_IN;
		_walkInEnded = false;
		_standEnded = false;
		_walkOutEnded = false;

		transform.position = _startPosition = startPosition;
		_standPosition = standPosition;
		_endPosition = endPosition;

		_speed = speed;
		_standingTime = standingTime;
        Vector3 newScale = transform.localScale;
        if (direction == CitizenDirection.LEFT)
        {
            _direction = Vector3.left;
            newScale.x = -0.85f;
        }
        else
        {
            _direction = Vector3.right;
            newScale.x = 0.85f;
        }
        transform.localScale = newScale;
        _row = row;

        _anim = GetComponent<Animator>();
        _anim.SetBool("stand", false);
        _anim.SetBool("happy", false);
        _anim.SetBool("sad", false);
	}

	void Update()
	{
		switch (_state) {
		case CitizenState.WALK_IN:
			WalkIn ();
			if (_walkInEnded) {
				_state = CitizenState.STAND;
				Stand ();
			}
			break;
		case CitizenState.STAND:
			if (_standEnded)
				_state = CitizenState.WALK_OUT;
			break;
		case CitizenState.WALK_OUT:
			WalkOut ();
			if (_walkOutEnded) {
				_endCitizenEvent.Citizen = gameObject;
				EventManager.Instance.OnEvent (this, _endCitizenEvent);
			}
			break;
		default:
			break;
		}
	}

	private void WalkIn()
	{
		Vector3 newPosition = transform.position + Time.deltaTime * _speed * _direction;
		transform.position = newPosition;

		//Debug.Log (Vector3.Distance (transform.position, _standPosition));
		_walkInEnded = Vector3.Distance (transform.position, _standPosition) < 0.5f;
	}

	private void Stand()
	{
        _anim.SetBool("stand", true);
		StartCoroutine (StandWaiting ());
	}

	private IEnumerator StandWaiting()
	{
		yield return new WaitForSeconds (_standingTime);

        if (UnityEngine.Random.Range(0.0f, 1.0f) < GameManager.Instance.PointsProgress)
        {
            _anim.SetBool("stand", false);
            _anim.SetBool("happy", true);
            EventManager.Instance.OnEvent(this, new GameEvent(GameEvent.GameEventType.CITIZEN_HAPPY));
        }
        else
        {
            _anim.SetBool("stand", false);
            _anim.SetBool("sad", true);
        }
        _standEnded = true;
	}

	private void WalkOut()
	{
		Vector3 newPosition = transform.position + Time.deltaTime * _speed * _direction;
		transform.position = newPosition;

		//Debug.Log (Vector3.Distance (transform.position, _endPosition));
		_walkOutEnded = Vector3.Distance (transform.position, _endPosition) < 0.5f;
	}
}
