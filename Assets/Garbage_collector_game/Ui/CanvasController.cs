using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject OnWinText;

    public void ShowWin()
    {
        OnWinText.SetActive(true);
    }
    public void HideWin()
    {
        OnWinText.SetActive(false);
    }

    public void ShowGameComplete()
    {
        OnWinText.SetActive(true);
        OnWinText.GetComponent<UnityEngine.UI.Text>().text = "Game Complete!";
    }
}
