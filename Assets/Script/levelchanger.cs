using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelchanger : MonoBehaviour
{
    public Animator animator;

    private int LevelToLoad;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            FadeToLevel(4);
            Debug.Log("4");
        }
    }

    public void FadeToLevel (int levelindex)
    {
        
        LevelToLoad = levelindex;
        Debug.Log(levelindex);
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete ()
    {
        Debug.Log(LevelToLoad);
        SceneManager.LoadScene(LevelToLoad);
    }

}
