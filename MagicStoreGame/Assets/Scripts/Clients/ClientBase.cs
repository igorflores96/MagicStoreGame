using UnityEngine;

public abstract class ClientBase : MonoBehaviour
{
    public Dialog DialogStorage;
    public string CurrentSentence;
    public GameObject CurrentItemOrder;
    public abstract void SetItemOrder(GameObject item);
    public abstract void LocalToWalk(Vector3 position);
    public abstract void SetDialogToSay();
}