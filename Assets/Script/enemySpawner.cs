using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnRadius = 5.0f;
    public int numberOfEnemiesToSpawn = 5;

    bool canSpawn = false;

    void Update()
    {
        // Kolla om vi kan spawn och sedan spawnar fiender
        if (canSpawn)
        {
            SpawnEnemy();
            canSpawn = false; // Återställ flaggan efter spawn
        }
    }

    // När en kolliderare träffar den här spawneren
    void OnTriggerEnter2D(Collider2D other)
    {
        // Kolla om det är spelaren som träffar
        if (other.CompareTag("Player"))
        {
            // Aktivera spawneren för att spawn fiender
            canSpawn = true;
        }
    }

    // Funktion för att spawn fiender
    public void SpawnEnemy()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Visar en visuell indikation av spawnradie i Unity-editorn
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
