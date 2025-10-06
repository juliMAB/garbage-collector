using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InGameAllReferences inGameAllReferences;

    public List<My_Level_Controller> levels = null;

    public CanvasController canvasController = null;

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
}
