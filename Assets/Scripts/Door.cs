using UnityEngine;

public class Door : MonoBehaviour
{
    public Room sideA;
    public Room sideB;

    public Collider sideACollider;
    public Collider sideBCollider;

    public bool requiresKey = false;
    public string requiredKey = "DefaultKey";

    public bool lockedFromA = false;
    public bool lockedFromB = false;

    private bool isOpen = false;

    // ✅ Constructor que recibe los dos Rooms
    public Door(Room in_sideA, Room in_sideB)
    {
        sideA = in_sideA;
        sideB = in_sideB;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory == null) return;

            bool comingFromA = sideACollider.bounds.Contains(other.transform.position);
            bool comingFromB = sideBCollider.bounds.Contains(other.transform.position);

            if ((comingFromA && lockedFromA) || (comingFromB && lockedFromB))
            {
                Debug.Log("La puerta está bloqueada desde este lado.");
                return;
            }

            if (requiresKey && !inventory.HasKey(requiredKey))
            {
                Debug.Log("Se necesita la llave: " + requiredKey);
                return;
            }

            // ✅ Gasta la llave si es expendable
            inventory.UseKey(requiredKey);

            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        gameObject.SetActive(false); // O usa una animación de apertura
        Debug.Log("Puerta abierta");
    }
}
