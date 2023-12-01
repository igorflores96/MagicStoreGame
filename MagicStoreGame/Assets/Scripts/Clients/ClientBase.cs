using UnityEngine;

public abstract class ClientBase : MonoBehaviour
{
    public Dialog DialogStorage;
    public string CurrentSentence;
    public abstract void SetItemOrder(Item item);
    public abstract void LocalToWalk(Transform position);
    public abstract void SetDialogToSay();
}