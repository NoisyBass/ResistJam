using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteButton : MonoBehaviour {

	public enum ButtonType
	{
		BUTTON_A,
		BUTTON_B,
		BUTTON_X,
		BUTTON_Y,
		BUTTON_DOWN,
		BUTTON_RIGHT,
		BUTTON_LEFT,
		BUTTON_UP
	}

	private ButtonType _type;
	public ButtonType Type
	{
		get { return _type; }
	}

    [SerializeField]
    private SpriteRenderer _label;
    private Animator _anim;

	private Vector3 _startPosition;
	private Vector3 _goalPosition;
	private float _time = 10.0f;
	private float _currentTime = 0.0f;
	private bool _move = false;

	private float _speed;

	private bool _canHit = false;
	public bool CanHit
	{
		get { return _canHit; }
	}

	public float Distance
	{
		get { return Vector3.Distance (transform.position, _goalPosition); }
	}

    [SerializeField]
    private GameObject _goodLabel;
    [SerializeField]
    private GameObject _greatLabel;
    [SerializeField]
    private GameObject _perfectLabel;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
	{
		if (_move) {

			//if (_currentTime < _time) {
			//	Vector3 newPosition = Vector3.Lerp (_startPosition, _goalPosition, _currentTime / _time);
			//
			//	transform.position = newPosition;
			//
			//	_currentTime += Time.deltaTime;
			//} else {
			//	EventManager.Instance.OnEvent (this, new EndNoteEvent (gameObject));
			//}

			if (transform.position.x > _goalPosition.x - 0.6) {
				Vector3 newPos = transform.position + Vector3.left * _speed * Time.deltaTime;
				transform.position = newPos;
			} else {
				_move = false;
				EventManager.Instance.OnEvent (this, new EndNoteEvent (gameObject, false));
			}
		}
	}



	public void Init(Vector3 startPosition, Vector3 goalPosition, float time, ButtonType type, Sprite label)
	{
		_type = type;

        _label.sprite = label;
        _label.gameObject.transform.localScale = new Vector2(1.0f, 1.0f);
        _label.enabled = true;
        _anim = GetComponent<Animator>();
        _anim.SetBool("active", false);
        _anim.SetBool("end", false);

		_currentTime = 0.0f;
		_time = time;
		transform.position = _startPosition = startPosition;
		_goalPosition = goalPosition;

		_speed = Vector3.Distance (_startPosition, _goalPosition)/time;

		_move = true;
		_canHit = false;
	}

    public IEnumerator Hit()
    {
        _move = false;
        _label.enabled = false;
        _anim.SetBool("end", true);

        yield return new WaitForSeconds(0.5f);

        EventManager.Instance.OnEvent(this, new EndNoteEvent(gameObject, true));
    }

    public void GoodHit()
    {
        _goodLabel.SetActive(true);
    }


    public void GreatHit()
    {
        _greatLabel.SetActive(true);
    }

    public void PerfectHit()
    {
        _perfectLabel.SetActive(true);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("ENTER");
		_canHit = true;
        _anim.SetBool("active", true);
        _label.gameObject.transform.localScale = new Vector2(1.4f, 1.4f);
    }

	void OnTriggerExit2D(Collider2D other)
	{
		//Debug.Log ("EXIT");
		_canHit = false;
        _anim.SetBool("active", false);
        _label.gameObject.transform.localScale = new Vector2(1.0f, 1.0f);

        //transform.localScale = new Vector2 (1.0f, 1.0f);
    }
}
