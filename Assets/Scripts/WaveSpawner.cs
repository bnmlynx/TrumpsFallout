using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	//created a public class to hold properties of the waves I'm going to be spawning
	[System.Serializable]
	public class Wave 
	{
		public string name;
		public GameObject donald;
		public int count;
		public float minRate;
		public float maxRate;
	}
		
	public Wave[] waves;
	public Transform[] spawnPoints;
	public Transform center;
	public float timeBetweenWaves = 5f;
	public float waveCountdown = 0f;
	public Text text;
	public Text timeText;
	public int donaldMultiplier;

	private float rate;
	private int nextWave = 0;
	private float searchCountdown = 1f;
	private ScoreManager scoreManager;
	private SpawnState state = SpawnState.COUNTING;
	private AudioSource audio;

	void Awake() 
	{

		scoreManager = GetComponent<ScoreManager> ();

		scoreManager.totalTrumps = waves [0].count;
	}


	void Start() 
	{
		waveCountdown = timeBetweenWaves;
	}

	void Update() 
	{

		if (state == SpawnState.WAITING) {
			//if no enemies are alive run wave complete function
			if (!EnemyIsAlive ()) {
				WaveCompleted ();
			} else {
				return;
			}
		}

		if (waveCountdown <= 0) 
		{

			timeText.enabled = false;

			if (state != SpawnState.SPAWNING) 
			{
				//starts spawning the wave
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		} else {
			//begin count down to next wave
			timeText.enabled = true;
			waveCountdown -= Time.deltaTime;
			timeText.text = "Next wave in " + waveCountdown.ToString ("F1"); 
		}
	}
		
	void SpawnEnemy(GameObject _enemy)
	{

		Transform _sp = spawnPoints [Random.Range (0, spawnPoints.Length)];

		GameObject don = Instantiate (_enemy, (Random.insideUnitSphere * 8) + _sp.position, _sp.rotation);
		audio = don.GetComponent<AudioSource> ();

		DonaldBehaviour getAudio = don.GetComponent<DonaldBehaviour> ();
		AudioClip randomSounds = getAudio.donaldSounds [0];

		audio.PlayOneShot(randomSounds);
		don.transform.LookAt(center);

	}

	IEnumerator SpawnWave(Wave _wave)
	{

		state = SpawnState.SPAWNING;

		for (int i = 0; i < _wave.count; i++) 
		{
			SpawnEnemy (_wave.donald);
			rate = Random.Range (_wave.minRate, _wave.maxRate);
			yield return new WaitForSeconds(rate);
		}

		scoreManager.totalTrumps = _wave.count;

		state = SpawnState.WAITING;

		yield break;
	}


	void WaveCompleted()
	{

		Debug.Log ("wave completed");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1) 
		{
			nextWave = 0;
			Debug.Log ("completed all waves");
		}

		scoreManager.numOfWaves++;
		nextWave++;

	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;

		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag ("Enemy") == null) 
			{
				return false;
			}
		}

		return true;
	}

}
