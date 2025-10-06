using UnityEngine;

public class TrashObject : MonoBehaviour
{
    public System.Action<TrashObject> OnCallOnDestroy;

    public bool IsTutorial = false;
    public void OnDestroy()
    {
        OnCallOnDestroy?.Invoke(this);
    }
}
