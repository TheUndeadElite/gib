using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array av fiendens prefabs som ska spawnas
    public float spawnRadius = 5.0f; // Maximalt avstånd från spawner där fienden kan spawnas
    public int numberOfEnemiesToSpawn = 5; // Antalet fiender som ska spawnas

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            // Välj en slumpmässig fiende från arrayen
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Beräkna en slumpmässig position inom spawnradie runt spawnerns position
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnRadius;

            // Skapa en fiende vid spawnpositionen
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Rita en gizmo för att visa spawnradie i scenen
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
