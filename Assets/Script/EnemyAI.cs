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
        // Kolla avst�ndet mellan fienden och spelaren
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootingRange)
        {
            // Om spelaren �r inom skjutintervall, kan fienden skjuta
            canShoot = true;

            // Stanna om fienden �r n�ra nog
            if (distanceToPlayer < 2f)
                isMoving = false;
        }
        else
        {
            // Annars kan fienden inte skjuta och b�r f�rf�lja spelaren
            canShoot = false;
            isMoving = true;
        }

        // Flytta mot spelaren om isMoving �r true
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        // Skjut om canShoot �r true
        if (canShoot)
        {
            // Skjut kod h�r (t.ex. anropa en Shoot-metod)
            Shoot();
        }
    }

    void Shoot()
    {
        // Implementera skjutlogik h�r
        Debug.Log("Fienden skjuter!");
    }
}
