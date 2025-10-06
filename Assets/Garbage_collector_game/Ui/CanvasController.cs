using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject OnWinText;

    public void ShowWin()
    {
        OnWinText.SetActive(true);
    }
}
