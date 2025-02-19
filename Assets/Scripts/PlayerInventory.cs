using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    private HashSet<string> keys = new HashSet<string>(); // Lista de llaves recogidas

    public void AddKey(string keyName)
    {
        keys.Add(keyName);
        Debug.Log("Llave obtenida: " + keyName);
    }

    public bool HasKey(string keyName)
    {
        return keys.Contains(keyName);
    }

    public void UseKey(string keyName)
    {
        if (keys.Contains(keyName))
        {
            keys.Remove(keyName);
            Debug.Log("Llave usada y eliminada: " + keyName);
        }
    }
}