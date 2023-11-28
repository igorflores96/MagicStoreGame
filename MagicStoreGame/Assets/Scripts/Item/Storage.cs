using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private bool _storageDisappear;
    [SerializeField] private float _timeToDisappear;


    public Transform GetItemFromStorage()
    {
        GameObject temp = Instantiate(_itemPrefab, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);

        if(_storageDisappear)
            StartCoroutine(DisappearAndReappear());

        return temp.transform;
    }


    IEnumerator DisappearAndReappear()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        renderer.enabled = false;

        yield return new WaitForSeconds(_timeToDisappear);

        collider.enabled = true;
        renderer.enabled = true;



    }
}
