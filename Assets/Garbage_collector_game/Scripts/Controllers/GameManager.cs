using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InGameAllReferences inGameAllReferences;
    public void AddScore()
    {
        Debug.Log("Greate!");
        AskIfAllTrashObjectsAreDestoyed();
    }
    private void Start()
    {
        TrashDropArea.onTrashDropped += AddScore;
    }

    private void AskIfAllTrashObjectsAreDestoyed()
    {
        if (TrashObject.trashitems.Count == 1) // porque siempre es 1 mas. aun no se destruye en este punto.
        {
            Debug.Log("You win!");
        }
    }
}
