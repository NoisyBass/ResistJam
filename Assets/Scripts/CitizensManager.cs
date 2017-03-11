using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizensManager : Singleton<CitizensManager> {

	[SerializeField]
	private Transform[] _leftPositions;
	[SerializeField]
	private Transform[] _middlePositions;
	[SerializeField]
	private Transform[] _rightPositions;

	private ObjectPool _citizensPool;
	[SerializeField]
	private GameObject _citizenPrefab;
	[SerializeField]
	private float _citizensSpeedMin = 0.75f;
	[SerializeField]
	private float _citizensSpeedMax = 1.5f;
	[SerializeField]
	private float _citizensStandingTimeMin = 2.0f;
	[SerializeField]
	private float _citizensStandingTimeMax = 5.0f;

	[SerializeField]
	private float _spawnTime = 4.0f;
	private int _spawnLine = 0;

	protected override void Awake () {

		base.Awake();

		_citizensPool = new ObjectPool (5, _citizenPrefab);
	}

	void Start()
	{
		EventManager.Instance.AddHandler (GameEvent.GameEventType.END_CITIZEN, EndCitizenHandler);
		StartCoroutine (SpawnCoroutine());
	}

	private void EndCitizenHandler (object sender, GameEvent e)
	{
		EndCitizenEvent endCitizen = (EndCitizenEvent)e;

		_citizensPool.DestroyObject (endCitizen.Citizen);
	}

	private IEnumerator SpawnCoroutine()
	{
		int rightOrLeft;
		float speed;
		float standingTime;
		GameObject newCitizen;

		while (true) {

			newCitizen = _citizensPool.CreateObject ();

			if (newCitizen != null) {
				// Right or Left?
				rightOrLeft = UnityEngine.Random.Range (0, 2);
				speed = UnityEngine.Random.Range (_citizensSpeedMin, _citizensSpeedMax);
				standingTime = UnityEngine.Random.Range (_citizensStandingTimeMin, _citizensStandingTimeMax);

				if (rightOrLeft == 0) { // RIGHT
					newCitizen.GetComponent<Citizen> ().Init (
						_leftPositions [_spawnLine].position,
						_middlePositions [_spawnLine].position,
						_rightPositions [_spawnLine].position,
						Citizen.CitizenDirection.RIGHT,
						speed,
						standingTime,
						_spawnLine);
				} else { // LEFT
					newCitizen.GetComponent<Citizen> ().Init (
						_rightPositions [_spawnLine].position,
						_middlePositions [_spawnLine].position,
						_leftPositions [_spawnLine].position,
						Citizen.CitizenDirection.LEFT,
						speed,
						standingTime,
						_spawnLine);
				}
				newCitizen.SetActive (true);
				_spawnLine = (_spawnLine + 1) % 3;
			}

			yield return new WaitForSeconds (_spawnTime);
		}
	}
}
