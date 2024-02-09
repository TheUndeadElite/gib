using UnityEngine;
using UnityEngine.Events;

public class EnemyRange : MonoBehaviour
{
    public float shootingRange = 10f; // Maximalt skjutavst�nd
    public float moveSpeed = 5f; // Fiendens r�relsehastighet
    public string playerTag = "Player"; // Tagg f�r spelaren
    public UnityEvent onPlayerInRange; // H�ndelse att utl�sa n�r spelaren �r inom skjutintervallen
    public UnityEvent onPlayerOutOfRange; // H�ndelse att utl�sa n�r spelaren �r utanf�r skjutintervallen

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

        // Ber�kna avst�ndet till spelaren
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= shootingRange)
        {
            // Om spelaren �r inom skjutintervall, trigga "p� range"-h�ndelsen
            onPlayerInRange.Invoke();
        }
        else
        {
            // Om spelaren �r utanf�r skjutintervall, trigga "utanf�r range"-h�ndelsen
            onPlayerOutOfRange.Invoke();

            // R�ra sig mot spelaren
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Rita en cirkel runt fienden f�r att representera skjutintervallen
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    public void MethodToInvoke()
    {

    }
}
