using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinesPlate : MonoBehaviour
{
    [SerializeField] private float _yOffSet;
    private bool _haveItem;
    private void OnTriggerStay(Collider other) 
    {
        if(!_haveItem)
        {
            Rigidbody tempRb;

            if(other.transform.TryGetComponent(out tempRb))
            {
                tempRb.constraints = RigidbodyConstraints.FreezeAll;
            }

            other.transform.position = new Vector3(transform.position.x, transform.position.y + _yOffSet, transform.position.z);
            other.transform.rotation = Quaternion.identity;
            _haveItem = true; 
        }
    }

    private void OnTriggerExit(Collider other) {
        _haveItem = false;
    }
}
