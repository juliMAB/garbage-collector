using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InGameAllReferences inGameAllReferences;

    public List<My_Level_Controller> levels = null;

    public CanvasController canvasController = null;
    private int currentLevelIndex = 0;

    public void AddScore()
    {
        Debug.Log("Greate!");
    }
    private void Start()
    {
        TrashDropArea.onTrashDropped += AddScore;
        levels[0].StartLevel(OnWinLevel);
        canvasController = FindAnyObjectByType<CanvasController>();
    }
    private void OnWinLevel()
    {
        Debug.Log("Win!");
        canvasController.ShowWin();
    }
    private void GoToNextLevel()
    {
        levels[0].gameObject.SetActive(false);
        if (levels.Count > 0)
        {
            levels[currentLevelIndex].gameObject.SetActive(true);
            levels[currentLevelIndex].StartLevel(OnWinLevel);
            canvasController.HideWin();
        }
        else
        {
            Debug.Log("No more levels!");
            canvasController.ShowGameComplete();
        }
    }
}
