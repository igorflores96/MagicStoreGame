using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScalePoundMachine : MonoBehaviour
{   
    public List<GameObject> _itensAwaitingForSell = new List<GameObject>();
    public List<GameObject> _originalItens = new List<GameObject>();

    [SerializeField] private ClientManager _clientManager;
    [SerializeField] private ScalesPot _pot;
    [SerializeField] private Transform _chestPosition;
    private int itemPrice { get; set; }
    private int colliderCount;
    public Animator anim;
    public MovementPlayer player;
    void OnCollisionEnter(Collision collision)
    {
        if(this.colliderCount == 0 && player.isGrabbed == false)
        {
            IItem tempItem;
            if(collision.gameObject.TryGetComponent(out tempItem))
            {
                if(!tempItem.IsOriginal)
                {
                    colliderCount++;
                    GetPrice(collision.gameObject, tempItem);
                }
                else
                {
                    if(!_originalItens.Contains(collision.gameObject))
                        _originalItens.Add(collision.gameObject);
                    
                    collision.transform.position = _chestPosition.position;
                }
            }
        }
    }
    public void GetPrice(GameObject gameObj, IItem item)
    {
        foreach (GameObject client in _itensAwaitingForSell)
        {
            if(client.TryGetComponent(out ClientBase clientScript))
            {
                if(clientScript.CurrentItemOrder.TryGetComponent(out Item itemTemp))
                {
                    if(item.NameItem == itemTemp.NameItem)
                    {
                        itemPrice = item.SetItemPrice(item.ValueItem);

                        switch(itemPrice)
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
                        
                        _pot.AdicionarEscamas(itemPrice);
                        _itensAwaitingForSell.Remove(client);

                        _clientManager.RemoveClient(client);
                        Destroy(client);
                        Destroy(gameObj);
                        colliderCount = 0;
                        break;
                    }
                }
            }
        }

    }
}
