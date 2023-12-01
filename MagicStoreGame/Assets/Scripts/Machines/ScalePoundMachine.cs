using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePoundMachine : MonoBehaviour
{   
    private int totalResult { get; set; }
    private int colliderCount;
    public Animator anim;
    public MovementPlayer player;
    void OnCollisionEnter(Collision collision)
    {
        if(this.colliderCount == 0 && player.isGrabbed == false)
        {
            Item item = collision.gameObject.GetComponent<Item>();
            GetPrice(item, collision.gameObject);
            colliderCount++;
        }
    }
    public void GetPrice(Item item, GameObject gameObj)
    {
       int value = item.ValueItem;
       this.totalResult = value;
       switch(value)
        {
            case 0:
                anim.SetTrigger("Bad");
                break;
            case 1:
                anim.SetTrigger("Default");
                break; 
            case 2:
                anim.SetTrigger("Good");
                break;
        }
        Destroy(gameObj);
        colliderCount = 0;
    }
}
