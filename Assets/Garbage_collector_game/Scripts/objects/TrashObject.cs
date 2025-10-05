using System.Collections.Generic;
using UnityEngine;

public class TrashObject : MonoBehaviour
{
    public static List<GameObject> trashitems;

    public static System.Action OnCallOnDestroy;

    public bool IsTutorial = false;

    private void Awake()
    {
        if (trashitems == null)
        {
            trashitems = new List<GameObject>();
        }
        trashitems.Add(this.gameObject);
    }
    public void OnDestroy()
    {
        trashitems.Remove(this.gameObject);
        OnCallOnDestroy?.Invoke();
    }
}
