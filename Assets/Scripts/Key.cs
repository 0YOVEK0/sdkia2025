using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyName = "DefaultKey"; // Nombre de la llave

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKey(keyName); //  Agrega la llave al inventario
                Destroy(gameObject); //  Destruye la llave después de recogerla
            }
            else
            {
                Debug.LogError("El jugador no tiene un PlayerInventory asignado.");
            }
        }
    }
}
