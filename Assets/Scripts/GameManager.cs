using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	[SerializeField]
	private int _maxPoints = 200;
	[SerializeField]
	private int _perfectPoints = 20;
	[SerializeField]
	private int _greatPoints = 10;
	[SerializeField]
	private int _goodPoints = 5;
	[SerializeField]
	private int _failPoints = 10;
	private int _currentPoints;

	[SerializeField]
	private Transform[] _startPositions;
	[SerializeField]
	private Transform[] _endPositions;
	[SerializeField]
	private GameObject _buttonPrefab;
	[SerializeField]
	private Sprite[] _buttonLabels;

	private AudioSource _audioSource;
	private float _beatTime = 0.49f;

	private ObjectPool _buttonsPool;
	private List<NoteButton> _activeNotes;

	private int _happyCitizens = 0;

	public int CurrentPoints
	{
		get { return _currentPoints; }
	}

	public int MaxPoints {
		get { return _maxPoints; }
	}

	public float PointsProgress
	{
		get { return (float)_currentPoints / (float)_maxPoints; }
	}

	public int HappyCitizens
	{
		get { return _happyCitizens; }
	}

	protected override void Awake () {

		base.Awake();

		_audioSource = GetComponent<AudioSource> ();
		//Screen.SetResolution (800, 600, false);
		_buttonsPool = new ObjectPool(5, _buttonPrefab);
		_activeNotes = new List<NoteButton> ();

        //_currentPoints = _maxPoints / 2;
        _currentPoints = 0;
	}

	void Start()
	{
		EventManager.Instance.AddHandler(GameEvent.GameEventType.END_NOTE, EndNoteHandler);
		EventManager.Instance.AddHandler (GameEvent.GameEventType.BUTTON_DOWN, ButtonDownHandler);
		EventManager.Instance.AddHandler (GameEvent.GameEventType.CITIZEN_HAPPY, CitizenHappyHandler);
		StartCoroutine(StartSong());
	}

	private void EndNoteHandler(object sender, GameEvent e)
	{
		GameObject noteToDestroy = ((EndNoteEvent)e).Note;

        if (((EndNoteEvent)e).Hit)
        {
            _buttonsPool.DestroyObject(noteToDestroy);
        }
        else
        {
            _buttonsPool.DestroyObject(noteToDestroy);
            _activeNotes.Remove(noteToDestroy.GetComponent<NoteButton>());

            Debug.Log("FAIL :(");
            _currentPoints -= _failPoints;
            if (_currentPoints < 0)
                _currentPoints = 0;
        }
		
	}

	private void ButtonDownHandler(object sender, GameEvent e)
	{
		if (_activeNotes.Count > 0) {
			Debug.Log ("Evento");
			if (((ButtonDownEvent)e).ButtonType == _activeNotes [0].Type && _activeNotes [0].CanHit) {
				if (_activeNotes [0].Distance < 0.1) {
					Debug.Log ("PERFECT!!");
                    _activeNotes[0].PerfectHit();
                    _currentPoints += _perfectPoints;
				} else if (_activeNotes [0].Distance < 0.25) {
					Debug.Log ("GREAT!!");
                    _activeNotes[0].GreatHit();
					_currentPoints += _greatPoints;
				} else {
					Debug.Log ("GOOD!!");
                    _activeNotes[0].GoodHit();
					_currentPoints += _goodPoints;
				}

                StartCoroutine(_activeNotes[0].Hit());
                _activeNotes.Remove(_activeNotes[0]);
            } else {
				Debug.Log ("FAIL :(");
				_currentPoints -= _failPoints;

                _buttonsPool.DestroyObject(_activeNotes[0].gameObject);
                _activeNotes.Remove(_activeNotes[0]);
            }
		}
	}

	private void CitizenHappyHandler(object sender, GameEvent e)
	{
		_happyCitizens++;
	}
	
	private IEnumerator StartSong()
	{
		//_audioSource.Play ();

		GameObject newButton;
		int button;

		while (true) {

			if (InputManager.Instance.ControllerConnected)
				button = UnityEngine.Random.Range (0, 4);
			else
				button = UnityEngine.Random.Range (4, 8);

			newButton = _buttonsPool.CreateObject ();

			if (newButton != null) {
				newButton.GetComponent<NoteButton> ().Init (_startPositions[button % 4].position, 
															_endPositions[button % 4].position, 
															_beatTime*8.0f, 
															(NoteButton.ButtonType)button, 
															_buttonLabels[button]);
				
				_activeNotes.Add(newButton.GetComponent<NoteButton>());
				newButton.SetActive (true);
			}

			yield return new WaitForSeconds (_beatTime*4.0f);
		}
	}
}
