using System.Collections;
using UnityEngine;

public class CameraFollowerObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _PlayerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] private float _flipYRotationTime = 0.5f;


    private Coroutine _trunCoroutine;

    private PlayerController _Player;

    private bool _isFacingRight;

    private void Awake()
    {
        _Player = _PlayerTransform.gameObject.GetComponent<PlayerController>();

        _isFacingRight = _Player.IsFacingRight;

    }

    // Update is called once per frame
    void Update()
    {
        // make the camreaFollowObject follow the Player`s positon
        transform.position = _Player.transform.position;
    }

    public void CallTurn()
    {
        _trunCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DeterminEndRotation();
        float yRotation = 0f;

        float elsapsedTime = 0f;
        while(elsapsedTime < _flipYRotationTime)
        {
            elsapsedTime += Time.deltaTime;

            // lerp the y rotation 
            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elsapsedTime / _flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            yield return null;
        }
    }

    private float DeterminEndRotation()
    {
        _isFacingRight = !_isFacingRight;

        if (_isFacingRight )
        {
            return 180f;
        }
        else
        {
            return 0f;
        }
    }
}
