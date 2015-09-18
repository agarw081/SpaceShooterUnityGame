/// Umang Agarwal

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text wave;
	public Text hscoreText;
	public Text newscore;

	private bool gameOver;
	private bool restart;
	public int score;
	public int highscore= 0;
	public int wavecount;
	
	void Start ()
	{
		highscore=PlayerPrefs.GetInt ("High Score");
		hscoreText.text = "";
		gameOver = false;
		wavecount = 1;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}
	
	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R) || Input.GetTouch(0).phase == TouchPhase.Moved )  
  			{
				hscoreText.text = "";
				wavecount = 1;
				wave.text = "";
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
	
	IEnumerator SpawnWaves ()
	{   wave.text = "Wave " + wavecount;
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < (hazardCount * wavecount) ; i++)
			{   if(gameOver) break; 
				else {
				wave.text = "";
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
				}}
			if (!gameOver)
			{
				wavecount = 1 + wavecount;
				wave.text = "Wave " + wavecount;
			}
			yield return new WaitForSeconds (waveWait);


			if (gameOver)
			{
				wave.text = "";
				restartText.text = "Swipe to Restart";
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{
		if (score > highscore) 
		{ 
						highscore = score;
						PlayerPrefs.SetInt ("High Score", highscore);
		}
		hscoreText.text = "High Score:" + highscore;
		newscore.text = "Score: " + score;
		gameOverText.text = "Game Over!";
		wave.text = "";
		gameOver = true;
	}
}