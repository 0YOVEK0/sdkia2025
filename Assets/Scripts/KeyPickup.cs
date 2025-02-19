using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                // Añadir la llave al inventario
                inventory.AddKey(keyName);
                Debug.Log("Llave recogida: " + keyName);

                // Destruir la llave después de recogerla
                Destroy(gameObject);
            }
        }
    }
}
