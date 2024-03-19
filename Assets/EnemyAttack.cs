using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject shadowBallPrefab; // Prefab för Shadow Ball
    public Transform attackPoint; // Punkt där Shadow Ball ska kastas från

    public float cooldownTime = 2f; // Tid mellan attacker
    private float nextAttackTime = 0f; // Tidpunkt för nästa attack

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + cooldownTime;
        }
    }

    void Attack()
    {
        // Skapa en instans av Shadow Ball prefab vid attackpunktens position och rotation
        Instantiate(shadowBallPrefab, attackPoint.position, attackPoint.rotation);
    }
}
