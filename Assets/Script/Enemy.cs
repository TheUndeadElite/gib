using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float retreatSpeed = 0.2f;
    public float attackDistance = 5.0f; // Attackräckvidd

    private Rigidbody2D rb2D;
    private Transform player;
    private EnemyAttack enemyAttack;
    private bool isWithinAttackRange = false; // Kontrollerar om fienden är inom attackräckvidden

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
        // Kolla om fienden är synlig för kameran
        if (IsVisible())
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

            // Om fienden är inom attackavståndet och attackläget är aktiverat, aktivera attacken
            if (!enemyAttack.isActiveAndEnabled && isWithinAttackRange)
            {
                enemyAttack.enabled = true;
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
        else
        {
            // Om fienden inte är synlig för kameran, inaktivera den
            gameObject.SetActive(false);
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
            // Rör fienden mot spelaren med moveSpeed
            Vector2 moveDirection = (playerPosition - enemyPosition).normalized;
            rb2D.velocity = moveDirection * moveSpeed;
        }
        else if (distanceToPlayer < attackDistance && distanceToPlayer > attackDistance / 2)
        {
            // Sluta röra sig
            rb2D.velocity = Vector2.zero;
        }
        else
        {
            // Backa bort från spelaren med retreatSpeed
            Vector2 retreatDirection = (enemyPosition - playerPosition).normalized;
            rb2D.velocity = retreatDirection * retreatSpeed;
        }
    }

    bool IsVisible()
    {
        // Kontrollera om fienden är synlig för kameran
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds);
    }
}
