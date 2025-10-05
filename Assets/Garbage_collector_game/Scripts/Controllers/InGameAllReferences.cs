using UnityEngine;

public class InGameAllReferences : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string pickableLayerName = "pikapeableObjet";

    static InGameAllReferences _instance;

    public static InGameAllReferences Instance;

    private int pickableLayer = -1;

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
                DontDestroyOnLoad(this.gameObject);
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
}
