
using UnityEngine;
using TMPro;
public class digaLogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;

    private void Start()
    {
        GetComponent<TypwriterEffect>().Run("this is a bit of text\n Hello", textLabel);
    }
}
