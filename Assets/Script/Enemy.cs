using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float retreatSpeed = 0.2f;
    public float attackDistance = 5.0f; // Attackr�ckvidd

    private Rigidbody2D rb2D;
    private Transform player;
    private EnemyAttack enemyAttack;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Knight").transform;
        enemyAttack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        MoveTowardsPlayer();

        // Om spelaren �r inom attackr�ckvidden, attackera
        if (Vector2.Distance(transform.position, player.position) <= attackDistance)
        {
            enemyAttack.Attack();
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

        if (distanceToPlayer > attackDistance)
        {
            // R�r fienden mot spelaren med moveSpeed
            Vector2 moveDirection = (playerPosition - enemyPosition).normalized;
            rb2D.velocity = moveDirection * moveSpeed;
        }
        else if (distanceToPlayer < attackDistance && distanceToPlayer > attackDistance / 2)
        {
            // Sluta r�ra sig
            rb2D.velocity = Vector2.zero;
        }
        else
        {
            // Backa bort fr�n spelaren med retreatSpeed
            Vector2 retreatDirection = (enemyPosition - playerPosition).normalized;
            rb2D.velocity = retreatDirection * retreatSpeed;
        }
    }
}
