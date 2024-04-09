
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array av fiendens prefabs som ska spawnas
    public float spawnInterval = 5.0f; // Intervall mellan varje spawn
    public float spawnRadius = 5.0f; // Maximalt avst�nd fr�n spawner d�r fienden kan spawnas



    private float timeSinceLastSpawn = 0.0f;
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        // Kontrollera om det �r dags att spawn en ny fiende
        if (timeSinceLastSpawn > spawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0.0f;
        }


    }

    void SpawnEnemy()
    {
        // V�lj en slumpm�ssig fiende fr�n arrayen
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Ber�kna en slumpm�ssig position inom spawnradie runt spawnerns position
        Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnRadius;

        // Skapa en fiende vid spawnpositionen
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    void OnDrawGizmosSelected()
    {
        // Rita en gizmo f�r att visa spawnradie i scenen
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
