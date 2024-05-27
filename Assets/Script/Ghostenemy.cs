using UnityEngine;

public class Ghostenemy : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float retreatSpeed = 0.2f;
    public float attackDistance = 5.0f; // Attackräckvidd
    public float retreatDistance = 1.25f; // Fjärdedel av attackavståndet för reträt
    public float attackCooldown = 2.0f; // Attackhastighet

    private Rigidbody2D rb2D;
    private Transform player;
    private EnemyAttack enemyAttack;
    private bool isWithinAttackRange = false; // Kontrollerar om fienden är inom attackräckvidden
    private bool isRetreating = false; // Kontrollerar om fienden drar sig tillbaka
    private float timeSinceLastAttack = 0.0f; // Tid sedan senaste attack

    
    private bool isVisible = false; // Om fienden är synlig på kameran

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Knight").transform;
        enemyAttack = GetComponent<EnemyAttack>();
      

        // Aktivera fienden vid start
        gameObject.SetActive(true);
    }

    void Update()
    {
        
      

       

        MoveTowardsPlayer();

        // Kolla om spelaren är levande och om fienden är inom attackavståndet
        if (Vector2.Distance(transform.position, player.position) <= attackDistance)
        {
            isWithinAttackRange = true; // Aktivera attackläge
        }
        else
        {
            isWithinAttackRange = false; // Inaktivera attackläge
        }

        // Kolla om fienden är stilla och inom attackavståndet, och att det har gått tillräckligt med tid sedan senaste attacken
        if (!isRetreating && isWithinAttackRange && rb2D.velocity.magnitude < 0.1f && Time.time - timeSinceLastAttack >= attackCooldown)
        {
            enemyAttack.enabled = true; // Aktivera attacken
            timeSinceLastAttack = Time.time; // Uppdatera tiden sedan senaste attacken
        }

        // Vänd fienden mot spelaren baserat på deras position på skärmen
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(player.position);

        if (screenPos.x < playerScreenPos.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void MoveTowardsPlayer()
    {
        if (player == null)
            return;

        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPosition = new Vector2(player.position.x, player.position.y);

        float distanceToPlayer = Vector2.Distance(enemyPosition, playerPosition);

        if (distanceToPlayer > attackDistance * 0.75f) // När spelaren är utanför tre fjärdedelar av attackavståndet
        {
            // Rör fienden mot spelaren med moveSpeed
            Vector2 moveDirection = (playerPosition - enemyPosition).normalized;
            rb2D.velocity = moveDirection * moveSpeed;
            isRetreating = false; // Fienden rör sig inte bakåt
        }
        else if (distanceToPlayer < attackDistance * 0.25f) // När spelaren är inom en fjärdedel av attackavståndet
        {
            // Backa bort från spelaren med retreatSpeed
            Vector2 retreatDirection = (enemyPosition - playerPosition).normalized;
            rb2D.velocity = retreatDirection * retreatSpeed;
            isRetreating = true; // Fienden drar sig tillbaka
        }
        else // När spelaren är inom tre fjärdedelar men utanför en fjärdedel av attackavståndet
        {
            // Sluta röra sig
            rb2D.velocity = Vector2.zero;
            isRetreating = false; // Fienden rör sig inte bakåt
        }
    }

    public void KnightDied()
    {
        // Stoppar fiendens attacker när riddaren dör
        enemyAttack.enabled = false;
    }

    
}
