using System;
using UnityEngine;

public class ExitArea : MonoBehaviour
{
    private Action onPlayerExit;
    public static ExitArea instance;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (InGameAllReferences.Instance.IsPlayer(other.gameObject))
        {
            onPlayerExit?.Invoke();
        }
    }
    public void SetOnPlayerExit(Action onPlayerExit)
    {
        this.onPlayerExit = onPlayerExit;
    }
    public void TurnOnArea()
    {
        this.gameObject.SetActive(true);
    }
    public void TurnOffArea()
    {
        this.gameObject.SetActive(false);
    }
}
