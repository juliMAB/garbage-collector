using System;
using UnityEngine;

public class TrashDropArea : MonoBehaviour
{
    public static Action onTrashDropped;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (InGameAllReferences.Instance.IsPickable(other.gameObject))
        {
            onTrashDropped?.Invoke();
        }
    }
}
