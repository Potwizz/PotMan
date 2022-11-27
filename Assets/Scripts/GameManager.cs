using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int score;

    public GameObject powerupPrefab;
    public GameObject potatoPrefab;
    public GameObject farmerPrefab;
    public GameObject farmerYPrefab;

    public bool isPotato = false;

    private float spawnRangeX = 8;
    private float spawnRangeY = 3.5f;
    private float spawnInterval = 1.5f;
    private float startDelay = 1;
    private float spawnRangeFarmerX = 7.5f;
    private float spawnPosFarmerY = 6;
    private float spawnRangeFarmerY = 4;
    private float spawnPosFarmerX = 13;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPowerup();
        SpawnPotato();
        InvokeRepeating("SpawnFarmerX", startDelay, spawnInterval);
        InvokeRepeating("SpawnFarmerY", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(PotatoSpawner());
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
        if (isPotato == false)
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
}
