using System.Collections;
using TMPro;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
#endif

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent = null;
    public string[] lines = null;
    public float textSpeed = 0;

    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static Dialogue _instance;

    private void OnEnable()
    {
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }

    // Update is called once per frame
    void Update()
    {
        bool clicked = false;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        if (Mouse.current != null)
        {
            clicked = Mouse.current.leftButton.wasPressedThisFrame;
        }
#else
        clicked = Input.GetMouseButtonDown(0);
#endif

        if (clicked)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if (index<lines.Length -1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
