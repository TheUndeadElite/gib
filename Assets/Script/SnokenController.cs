using UnityEngine;

public class SnokenController : MonoBehaviour
{
    [SerializeField] Transform player;

    Vector2 relativePoint;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
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