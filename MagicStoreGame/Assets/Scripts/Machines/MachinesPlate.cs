using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinesPlate : MonoBehaviour
{
    private void OnTriggerStay(Collider other) 
    {

        Rigidbody tempRb;

        if(other.transform.TryGetComponent(out tempRb))
        {
            tempRb.constraints = RigidbodyConstraints.FreezeAll;
        }

        other.transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
        other.transform.rotation = Quaternion.identity;
    }
}