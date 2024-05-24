using UnityEngine;

public class Ghostenemy : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float retreatSpeed = 0.2f;
    public float attackDistance = 5.0f; // Attackr�ckvidd
    public float retreatDistance = 1.25f; // Fj�rdedel av attackavst�ndet f�r retr�t
    public float attackCooldown = 2.0f; // Attackhastighet

    private Rigidbody2D rb2D;
    private Transform player;
    private EnemyAttack enemyAttack;
    private bool isWithinAttackRange = false; // Kontrollerar om fienden �r inom attackr�ckvidden
    private bool isRetreating = false; // Kontrollerar om fienden drar sig tillbaka
    private float timeSinceLastAttack = 0.0f; // Tid sedan senaste attack

    
    private bool isVisible = false; // Om fienden �r synlig p� kameran

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

        // Kolla om spelaren �r levande och om fienden �r inom attackavst�ndet
        if (Vector2.Distance(transform.position, player.position) <= attackDistance)
        {
            isWithinAttackRange = true; // Aktivera attackl�ge
        }
        else
        {
            isWithinAttackRange = false; // Inaktivera attackl�ge
        }

        // Kolla om fienden �r stilla och inom attackavst�ndet, och att det har g�tt tillr�ckligt med tid sedan senaste attacken
        if (!isRetreating && isWithinAttackRange && rb2D.velocity.magnitude < 0.1f && Time.time - timeSinceLastAttack >= attackCooldown)
        {
            enemyAttack.enabled = true; // Aktivera attacken
            timeSinceLastAttack = Time.time; // Uppdatera tiden sedan senaste attacken
        }

        // V�nd fienden mot spelaren baserat p� deras position p� sk�rmen
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

        if (distanceToPlayer > attackDistance * 0.75f) // N�r spelaren �r utanf�r tre fj�rdedelar av attackavst�ndet
        {
            // R�r fienden mot spelaren med moveSpeed
            Vector2 moveDirection = (playerPosition - enemyPosition).normalized;
            rb2D.velocity = moveDirection * moveSpeed;
            isRetreating = false; // Fienden r�r sig inte bak�t
        }
        else if (distanceToPlayer < attackDistance * 0.25f) // N�r spelaren �r inom en fj�rdedel av attackavst�ndet
        {
            // Backa bort fr�n spelaren med retreatSpeed
            Vector2 retreatDirection = (enemyPosition - playerPosition).normalized;
            rb2D.velocity = retreatDirection * retreatSpeed;
            isRetreating = true; // Fienden drar sig tillbaka
        }
        else // N�r spelaren �r inom tre fj�rdedelar men utanf�r en fj�rdedel av attackavst�ndet
        {
            // Sluta r�ra sig
            rb2D.velocity = Vector2.zero;
            isRetreating = false; // Fienden r�r sig inte bak�t
        }
    }

    public void KnightDied()
    {
        // Stoppar fiendens attacker n�r riddaren d�r
        enemyAttack.enabled = false;
    }

    
}
