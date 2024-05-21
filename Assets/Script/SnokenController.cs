using UnityEngine;

public class SnokenController : MonoBehaviour
{
    [SerializeField] public Transform player; // Kontrollera att detta är synligt i inspektorn

    Vector2 relativePoint;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned!");
            return;
        }

        relativePoint = transform.InverseTransformPoint(player.position);

        if (relativePoint.x < 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
        {
            spriteRenderer.flipX = true;
        }
        else if (relativePoint.x > 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
        {
            spriteRenderer.flipX = false;
        }
    }
}
