using System.Collections.Generic;
using UnityEngine;

public class InGameAllReferences : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string pickableLayerName = "pikapeableObjet";
    [SerializeField] private List<GameObject> pickupItemsPrefabs;

    static InGameAllReferences _instance;

    public static InGameAllReferences Instance;

    private int pickableLayer = -1;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject ExitArea = null;
    public bool IsPlayer(GameObject other)
    {
        return player == other;
    }

    private void Awake()
    {
        void SingletonInicializer()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                Instance = this;
            }
        }

        SingletonInicializer();

        pickableLayer = LayerMask.NameToLayer(pickableLayerName);
        if (pickableLayer == -1)
        {
            Debug.LogWarning($"La capa \"{pickableLayerName}\" no existe. Crea la capa en Project Settings > Tags and Layers y asígnala a los objetos recogibles.");
        }
    }

    public bool IsPickable(GameObject go)
    {
        return pickableLayer != -1 && go.layer == pickableLayer;
    }

    public GameObject GetRandomPickupItemPrefab()
    {
        if (pickupItemsPrefabs.Count == 0) return null;
        int randomIndex = Random.Range(0, pickupItemsPrefabs.Count);
        return pickupItemsPrefabs[randomIndex];
    }

    public void StartLevel()
    {
        ExitArea.SetActive(false);
    }
    public void EndLevel()
    {
        ExitArea.SetActive(true);
    }
}
