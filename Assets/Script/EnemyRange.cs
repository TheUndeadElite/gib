using UnityEngine;
using UnityEngine.Events;

public class EnemyRange : MonoBehaviour
{
    public float shootingRange = 10f; // Maximalt skjutavstånd
    public float moveSpeed = 5f; // Fiendens rörelsehastighet
    public string playerTag = "Player"; // Tagg för spelaren
    public UnityEvent onPlayerInRange; // Händelse att utlösa när spelaren är inom skjutintervallen
    public UnityEvent onPlayerOutOfRange; // Händelse att utlösa när spelaren är utanför skjutintervallen

    private Transform playerTransform; // Referens till spelarens transform

    void Start()
    {
        // Hitta spelarens transform
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        // Beräkna avståndet till spelaren
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= shootingRange)
        {
            // Om spelaren är inom skjutintervall, trigga "på range"-händelsen
            onPlayerInRange.Invoke();
        }
        else
        {
            // Om spelaren är utanför skjutintervall, trigga "utanför range"-händelsen
            onPlayerOutOfRange.Invoke();

            // Röra sig mot spelaren
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Rita en cirkel runt fienden för att representera skjutintervallen
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    public void MethodToInvoke()
    {

    }
}
