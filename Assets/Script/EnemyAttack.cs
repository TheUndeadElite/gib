using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject shadowBallPrefab; // Prefab för Shadow Ball
    public Transform attackPoint; // Punkt där Shadow Ball ska kastas från

    public float cooldownTime = 2f; // Tid mellan attacker
    private float nextAttackTime = 0f; // Tidpunkt för nästa attack

    private Transform player; // Spelarens position

    void Start()
    {
        // Hitta spelarobjektet och spara dess position
        player = GameObject.FindGameObjectWithTag("Knight").transform;
    }

    void Update()
    {
        // Om det är dags för nästa attack
        if (Time.time >= nextAttackTime)
        {
            // Sikta attackpunkten mot spelaren
            if (player != null)
            {
                Vector3 direction = player.position - attackPoint.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                attackPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                // Attacka
                Attack();

                // Uppdatera tidpunkten för nästa attack
                nextAttackTime = Time.time + cooldownTime;
            }
        }
    }

    public void Attack()
    {
        // Skapa en instans av Shadow Ball prefab vid attackpunktens position och rotation
        Instantiate(shadowBallPrefab, attackPoint.position, attackPoint.rotation);
    }
}
