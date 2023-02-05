using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI creditsText;

    private AudioSource playerAudio;
    public AudioClip deathSound;

    private int score;

    public GameObject powerupPrefab;
    public GameObject potatoPrefab;
    public GameObject farmerPrefab;
    public GameObject farmerYPrefab;
    public GameObject farmerBotPrefab;
    public GameObject farmerLeftPrefab;
    public GameObject titleScreen;
    public GameObject player;
    public GameObject scoreTextx;
    public GameObject soundManager;
    public GameObject restartText;

    public Button restartButton;

    public bool isPotato = false;
    public bool isGameActive;

    private float spawnRangeX = 5;
    private float spawnRangeY = 3.5f;
    private float spawnInterval = 1.5f;
    private float startDelay = 1;
    private float spawnRangeFarmerX = 5f;
    private float spawnPosFarmerY = 6;
    private float spawnRangeFarmerY = 4;
    private float spawnPosFarmerX = 6;
    private float spawnPosFarmerBot = -5f;
    private float spawnRangeFarmerBot = -6f;
    private float spawnPosFarmerLeft = -6f;
    private float spawnRangeFarmerLeft = -4f;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetKey(KeyCode.Space))
         {
             if(isGameActive == false)
             {
                 StartGame();
             }

             Debug.Log("Space has been pressed");
         } */

        if (isGameActive == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void SpawnFarmerX()
    {
            Vector2 spawnPos = new Vector2(Random.Range(-spawnRangeFarmerX, spawnRangeFarmerX), spawnPosFarmerY);
            Instantiate(farmerPrefab, spawnPos, farmerPrefab.transform.rotation);
    }

    public void SpawnFarmerY()
    {
            Vector2 spawnPos = new Vector2(spawnPosFarmerX, Random.Range(-spawnRangeFarmerY, spawnRangeFarmerY));
            Instantiate(farmerYPrefab, spawnPos, farmerPrefab.transform.rotation);
    }

    public void SpawnFarmerBot()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-spawnRangeFarmerBot, spawnRangeFarmerBot), spawnPosFarmerBot);
        Instantiate(farmerBotPrefab, spawnPos, farmerPrefab.transform.rotation);
    }

    public void SpawnFarmerLeft()
    {
        Vector2 spawnPos = new Vector2(spawnPosFarmerLeft, Random.Range(-spawnRangeFarmerLeft, spawnRangeFarmerLeft));
        Instantiate(farmerLeftPrefab, spawnPos, farmerPrefab.transform.rotation);
    }

        public void SpawnPowerup()
    {
            Instantiate(powerupPrefab, /*GenerateSpawnPosition()*/new Vector2(0, 1), powerupPrefab.transform.rotation);
    }

    public void SpawnPotato()
    {
            Instantiate(potatoPrefab, GenerateSpawnPosition(), potatoPrefab.transform.rotation);
            isPotato = false;
    }

    IEnumerator PotatoSpawner()
    {
        while (isGameActive == true)
        {
            isPotato = true;
            yield return new WaitForSeconds(1);
            SpawnPotato();
        }
    }

    private Vector2 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosY = Random.Range(-spawnRangeY, spawnRangeY);

        Vector2 randomPos = new Vector2(spawnPosX, spawnPosY);

        return randomPos;
    }

    public void UpdateScore(int scoreToAdd) // Updates the score
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score; // Adds the score variable into the scene when playing
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void StartGame()
    {
        isGameActive = true;

        StartCoroutine(PotatoSpawner());

        SpawnPowerup();
        SpawnPotato();
        InvokeRepeating("SpawnFarmerX", startDelay, spawnInterval);
        InvokeRepeating("SpawnFarmerY", startDelay, spawnInterval);
        InvokeRepeating("SpawnFarmerBot", startDelay, spawnInterval);
        InvokeRepeating("SpawnFarmerLeft", startDelay, spawnInterval);

        player.gameObject.SetActive(true);
        scoreTextx.gameObject.SetActive(true);
        soundManager.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        playerAudio.PlayOneShot(deathSound, 1.0f);
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        soundManager.gameObject.SetActive(false);
        isGameActive = false;
        //playerAudio.Stop(musicPlaying);

    }
}
