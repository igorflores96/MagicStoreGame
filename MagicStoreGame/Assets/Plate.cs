using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    private List<GameObject> collidedObjects = new List<GameObject>();
    private int countItem;
    public bool isUpgraded;
    public Microwave microwave;
    void OnTriggerEnter(Collider collision)
    {
        GameObject collidedObject = collision.gameObject;
        // Adiciona o objeto à lista se ainda não estiver presente
        if (!collidedObjects.Contains(collidedObject) && collidedObject.tag != "Result")
        {
            collidedObjects.Add(collidedObject);
            Debug.Log(collidedObjects);
            Item item = collision.gameObject.GetComponent<Item>();
            microwave.AddItem(item, isUpgraded, collidedObjects);
        }
        Debug.Log(collidedObjects.Count);
    }
    void OnTriggerExit(Collider collision)
    {
        microwave.RemoveItemTrigger(collision.gameObject);
    }
}
