﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public static int score;
	public static float time;

	public Text wavesLasted;
	public Text donaldsKilled;
	public Text finalScore;
	public Text finalWaves;
	public Image radiationBar;
	public int numOfWaves = 0;
	public int numOfDonaldsAlive;
	public GameObject gameOverScreen;

	public float startRadiation = 0f;
	public float totalTrumps;
	public float endRadiation;
	public float radiation;

	public float test;


	private WaveSpawner waveSpawnerScript;

	// Use this for initialization
	void Start () {
		gameOverScreen.SetActive (false);
		score = 0;
		radiation = startRadiation;
		numOfDonaldsAlive = 0;
		waveSpawnerScript = GetComponent<WaveSpawner> ();
	}

	// Update is called once per frame
	void Update () {

		endRadiation = (totalTrumps / 100f) * 75f;

		endRadiation = endRadiation * 10f;



		//Debug.Log (endRadiation);

		wavesLasted.text = "WAVE " + numOfWaves.ToString();
		donaldsKilled.text = "SCORE " +  score.ToString();


		if (radiation >= endRadiation) {
			EndGame ();
		}	
	}

	public void EndGame()
	{
		gameOverScreen.SetActive (true);
		finalScore.text = "SCORE " + score.ToString ();
		finalWaves.text = "WAVES " + numOfWaves.ToString ();
	}

	public void increaseCounter() {

		radiation += 10f;
		radiationBar.fillAmount = radiation / endRadiation;
		numOfDonaldsAlive++;
	}

	public void decreaseCounter() {
		radiation -= 10f;
		radiationBar.fillAmount = radiation / endRadiation;
		numOfDonaldsAlive--;
	}
}
