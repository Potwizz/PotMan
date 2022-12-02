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

    private int score;

    public GameObject powerupPrefab;
    public GameObject potatoPrefab;
    public GameObject farmerPrefab;
    public GameObject farmerYPrefab;
    public GameObject titleScreen;
    public GameObject player;
    public GameObject scoreTextx;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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


    public void SpawnPowerup()
    {
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
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
            yield return new WaitForSeconds(3);
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
        Debug.Log("Test");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isGameActive = true;

        StartCoroutine(PotatoSpawner());

        SpawnPowerup();
        SpawnPotato();
        InvokeRepeating("SpawnFarmerX", startDelay, spawnInterval);
        InvokeRepeating("SpawnFarmerY", startDelay, spawnInterval);

        titleScreen.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        scoreTextx.gameObject.SetActive(true);

    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }


}
