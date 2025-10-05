using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuraci�n")]
    [SerializeField] private string pickableLayerName = "pikapeableObjet";
    [SerializeField] private Transform itemHolder; // Punto donde se colocar� el objeto al recogerlo

    [Header("Estado")]
    [SerializeField] private GameObject nearbyPickable; // Objeto detectado en el trigger
    [SerializeField] private GameObject heldItem;       // Objeto actualmente recogido

    private int pickableLayer = -1;

    // Input
    private InputAction interactAction;

    public GameObject NearbyPickable => nearbyPickable;
    public GameObject HeldItem => heldItem;

    private void Awake()
    {
        pickableLayer = LayerMask.NameToLayer(pickableLayerName);
        if (pickableLayer == -1)
        {
            Debug.LogWarning($"La capa \"{pickableLayerName}\" no existe. Crea la capa en Project Settings > Tags and Layers y as�gnala a los objetos recogibles.");
        }
    }

    private void OnEnable()
    {
        if (interactAction == null)
        {
            interactAction = new InputAction(
                name: "Interact",
                type: InputActionType.Button
            );
            // Teclado: E (dispara en press)
            interactAction.AddBinding("<Keyboard>/e").WithInteraction("Press");
            // Gamepad: bot�n sur (A/Cross)
            interactAction.AddBinding("<Gamepad>/buttonSouth").WithInteraction("Press");
        }

        interactAction.performed += OnInteractPerformed;
        interactAction.Enable();
    }

    private void OnDisable()
    {
        if (interactAction != null)
        {
            interactAction.performed -= OnInteractPerformed;
            interactAction.Disable();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (InGameAllReferences.Instance.IsPickable(other.gameObject))
        {
            nearbyPickable = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (nearbyPickable == other.gameObject)
        {
            nearbyPickable = null;
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext ctx)
    {
        if (heldItem == null)
        {
            TryPickUp();
        }
        else
        {
            TryDrop();
        }
    }

    // Llama a este m�todo cuando quieras recoger el objeto detectado.
    public bool TryPickUp()
    {
        if (heldItem != null || nearbyPickable == null) return false;

        heldItem = nearbyPickable;

        // Parent al holder para fijar su posici�n/rotaci�n
        if (itemHolder != null)
        {
            var tr = heldItem.transform;
            tr.SetParent(itemHolder, worldPositionStays: false);
            tr.localPosition = Vector3.zero;
            tr.localRotation = Quaternion.identity;
            tr.localScale = Vector3.one;
        }

        // Opcional: desactivar colisi�n/f�sica del objeto recogido
         var col = heldItem.GetComponent<Collider2D>(); if (col) col.enabled = false;

        // Limpia la referencia cercana (ya fue recogido)
        nearbyPickable = null;
        return true;
    }

    // Llama a este m�todo para soltar el objeto almacenado.
    public bool TryDrop()
    {
        if (heldItem == null) return false;

        // Opcional: revertir cambios de TryPickUp
         var col = heldItem.GetComponent<Collider2D>(); if (col) col.enabled = true;

        // Mantener posici�n en el mundo al desparentar
        heldItem.transform.SetParent(null, worldPositionStays: true);

        heldItem = null;
        return true;
    }
}
