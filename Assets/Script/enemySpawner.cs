using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnRadius = 5.0f;

    [HideInInspector]
    public bool canSpawn = false;

    [SerializeField] private int numberOfType1ToSpawn = 5;
    [SerializeField] private int numberOfType2ToSpawn = 5;

    void Update()
    {
        if (canSpawn)
        {
            SpawnEnemies();
            canSpawn = false;
        }
    }

    // Uppdatera metoden för att ta emot två argument för antalet fiender av varje typ som ska spawna
    public void ActivateSpawner(int type1Count, int type2Count)
    {
        numberOfType1ToSpawn = type1Count;
        numberOfType2ToSpawn = type2Count;
        canSpawn = true;
    }

    public void SpawnEnemies()
    {
        SpawnEnemyType(0, numberOfType1ToSpawn); // Spawn enemies of type 1
        SpawnEnemyType(1, numberOfType2ToSpawn); // Spawn enemies of type 2
    }

    void SpawnEnemyType(int enemyTypeIndex, int count)
    {
        int prefabIndex = Mathf.Clamp(enemyTypeIndex, 0, enemyPrefabs.Length - 1);
        GameObject enemyPrefab = enemyPrefabs[prefabIndex];

        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    void Reset()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
        }

        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }
}
