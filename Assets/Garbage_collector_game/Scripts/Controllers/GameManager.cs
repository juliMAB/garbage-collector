using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InGameAllReferences inGameAllReferences;
    public void AddScore()
    {
        Debug.Log("Greate!");
    }
    private void Start()
    {
        TrashDropArea.onTrashDropped += AddScore;
    }
}
