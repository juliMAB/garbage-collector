using System;
using System.Collections.Generic;
using UnityEngine;

public class My_Level_Controller : MonoBehaviour
{
    public Transform parent_objects = null;
    public List<TrashObject> pickupItemsInLevel = null;
    public List<Transform> spawnPointPosition = null;
    private Action OnWin = null;
    public void StartLevel(Action OnWin)
    {
        //spawn some pickupitems.
        //start timer.
        for (int i = 0; i < spawnPointPosition.Count; i++)
        {
            GameObject go = Instantiate(InGameAllReferences.Instance.GetRandomPickupItemPrefab(), spawnPointPosition[i].position, Quaternion.identity, parent_objects);
            pickupItemsInLevel.Add(go.GetComponent<TrashObject>());
        }
        for (int i = 0; i < pickupItemsInLevel.Count; i++)
        {
            pickupItemsInLevel[i].OnCallOnDestroy += tryToEnd;
        }
        this.OnWin += OnWin;
    }
    void tryToEnd(TrashObject go)
    {
        pickupItemsInLevel.Remove(go);
        if (pickupItemsInLevel.Count == 0)
        {
            endLevel();
        }
    }

    void endLevel()
    {
        OnWin?.Invoke();
        //stop timer.
        //show score.
        //canvasController.ShowWin();
    }
}
