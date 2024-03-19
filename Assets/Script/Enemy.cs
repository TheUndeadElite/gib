using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float retreatSpeed = 0.2f;
    public float stoppingDistance = 2.0f;

    private Rigidbody2D rb2D;
    private Transform player;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Knight").transform;
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player == null)
            return;

        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPosition = new Vector2(player.position.x, player.position.y);

        float distanceToPlayer = Vector2.Distance(enemyPosition, playerPosition);

        // Rör fienden mot spelaren med moveSpeed
        if (distanceToPlayer > stoppingDistance)
        {
            Vector2 moveDirection = (playerPosition - enemyPosition).normalized;
            rb2D.velocity = moveDirection * moveSpeed;
        }
        // Backa bort från spelaren med retreatSpeed
        else if (distanceToPlayer < stoppingDistance && distanceToPlayer > stoppingDistance / 2)
        {
            Vector2 retreatDirection = (enemyPosition - playerPosition).normalized;
            rb2D.velocity = retreatDirection * retreatSpeed;
        }
        else
        {
            rb2D.velocity = Vector2.zero;
        }

        // Kolla på spelaren beroende på deras position i förhållande till skärmen
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(player.position);

        // Om fienden är till vänster om spelaren på skärmen
        if (screenPos.x < playerScreenPos.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // Om fienden är till höger om spelaren på skärmen
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
