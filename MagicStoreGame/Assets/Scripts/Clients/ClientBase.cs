using UnityEngine;

public abstract class ClientBase : MonoBehaviour
{
    public Dialog DialogStorage;
    public abstract void SetItemOrder(Item item);
    public abstract string SetDialogToSay();
}