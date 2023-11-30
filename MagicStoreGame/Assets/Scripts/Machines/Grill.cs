using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Grill : MonoBehaviour
{
    private List<GameObject> collidedObjects = new List<GameObject>();
    public SubWitch subWitch;
    public GameObject spawn;
    public MovementPlayer movementPlayer;
    private Vector3 positionNow;
    GameObject baseObj;
    void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        // Adiciona o objeto à lista se ainda não estiver presente
        if (!collidedObjects.Contains(collidedObject) && collidedObject.tag != "ResultSub")
        {
           
            Debug.Log(collidedObjects);
            Item item = collision.gameObject.GetComponent<Item>();
            collidedObjects.Add(collidedObject);
            subWitch.AddItem(item, collidedObjects);
            if (collidedObjects.Count == 1)
            {
                collidedObject.transform.position = this.spawn.transform.position;
                baseObj = collidedObject;
                collidedObject.gameObject.GetComponent<Collider>().enabled = false;
                collidedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                item.isStacking = true;
            }
            else if (collidedObjects.Count > 1 && movementPlayer.isGrabbed == false)
            {
               float baseHeight = baseObj.GetComponent<Renderer>().bounds.size.y;
                if(positionNow.x == 0 && positionNow.y == 0 && positionNow.z == 0)
                {
                    positionNow = this.spawn.transform.position;
                }
               positionNow.y = positionNow.y + baseHeight;
                collidedObject.gameObject.GetComponent<Collider>().enabled = false;
                collidedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
               collidedObject.transform.position = positionNow;
               item.isStacking = true;
               baseObj = collidedObject;
            }
            
        }
        Debug.Log(collidedObjects.Count);
    }
    void OnCollisionExit(Collision collision)
   {
        //botar flag no obj dizendo q ele ta stacked, q dai ele verifica e ve se ta stacked n deletear
        if (movementPlayer.isGrabbed == true)
        {
            Remove(collision.gameObject);
        }
    }
    public void Remove(GameObject collisionGame)
    {
        subWitch.RemoveItemTrigger(collisionGame);
        Debug.Log("REmoved");
    }

}
