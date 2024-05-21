using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private GameObject knight;
    public Sprite openDoorImage;
    public Sprite closeDoorImage;
    public float timeBeforeNextScene;
    public SceneAsset targetScene;
    public bool playerIsAtTheDoor;

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private Vector3 originalPosition; // Store the original position of the door
    GameManager s_gameManager;

    Sign sign;
    

    void Start()
    {
        s_gameManager = FindObjectOfType<GameManager>();
        sign = FindObjectOfType<Sign>();

        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        originalPosition = transform.position; // Store the original position

        if (knight == null)
        {
            knight = GameObject.FindGameObjectWithTag("Knight");
            if (knight == null)
            {
                Debug.LogError("No game object with tag 'Knight' found!");
            }
        }

        // Automatically assign Open Door Sprite reference
        openDoorImage = Resources.Load<Sprite>("Openeddoors");
        if (openDoorImage == null)
        {
            Debug.LogError("Opened Door Sprite not found. Make sure the sprite is in the 'Resources' folder and named 'Openeddoors'.");
        }

        // Automatically assign Closed Door Sprite reference
        closeDoorImage = Resources.Load<Sprite>("Closeddoors");
        if (closeDoorImage == null)
        {
            Debug.LogError("Closed Door Sprite not found. Make sure the sprite is in the 'Resources' folder and named 'Closeddoors'.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsAtTheDoor)
        {
            if (sign.GetHasReadSign() == false)
            {
                return;
            }

            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        // Change sprite to open door
        spriteRenderer.sprite = openDoorImage;

        // Adjust position if needed
        transform.position += new Vector3(-0.088f, -0.23f, 0); // Move the door to the left and down

        yield return new WaitForSeconds(timeBeforeNextScene);

        if (knight != null)
        {
            knight.SetActive(false); // Deactivate the Knight object
        }
        else
        {
            Debug.LogError("No game object with tag 'Knight' assigned!");
        }

        yield return new WaitForSeconds(timeBeforeNextScene);

        // Change sprite back to closed door
        spriteRenderer.sprite = closeDoorImage;

        // Restore original position
        transform.position = originalPosition;

        yield return new WaitForSeconds(timeBeforeNextScene);

        LoadScene(); // Load the specified scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Knight"))
        {
            playerIsAtTheDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Knight"))
        {
            playerIsAtTheDoor = false;
        }
    }

    private void LoadScene()
    {
        if (targetScene != null && !s_gameManager.gameIsPaused)
        {
            SceneManager.LoadScene(targetScene.name);


        }
        else
        {
            Debug.LogError("No target scene selected!");
        }
    }
}
