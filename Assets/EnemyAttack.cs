using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject shadowBallPrefab; // Prefab f�r Shadow Ball
    public Transform attackPoint; // Punkt d�r Shadow Ball ska kastas fr�n

    public float cooldownTime = 2f; // Tid mellan attacker
    private float nextAttackTime = 0f; // Tidpunkt f�r n�sta attack

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
