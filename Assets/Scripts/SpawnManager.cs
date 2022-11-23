using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject powerupPrefab;
    public GameObject potatoPrefab;

    public bool isPotato = false;

    private float spawnRange = 4;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPowerup();
        SpawnPotato();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(PotatoSpawner());
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
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = Random.Range(-spawnRange, spawnRange);

        Vector2 randomPos = new Vector2(spawnPosX, spawnPosY);

        return randomPos;
    }
}
