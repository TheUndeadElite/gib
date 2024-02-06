using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float shootingRange = 10f;
    public float moveSpeed = 5f;

    private bool canShoot = false;
    private bool isMoving = false;

    void Update()
    {
        // Kolla avståndet mellan fienden och spelaren
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootingRange)
        {
            // Om spelaren är inom skjutintervall, kan fienden skjuta
            canShoot = true;

            // Stanna om fienden är nära nog
            if (distanceToPlayer < 2f)
                isMoving = false;
        }
        else
        {
            // Annars kan fienden inte skjuta och bör förfölja spelaren
            canShoot = false;
            isMoving = true;
        }

        // Flytta mot spelaren om isMoving är true
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        // Skjut om canShoot är true
        if (canShoot)
        {
            // Skjut kod här (t.ex. anropa en Shoot-metod)
            Shoot();
        }
    }

    void Shoot()
    {
        // Implementera skjutlogik här
        Debug.Log("Fienden skjuter!");
    }
}
