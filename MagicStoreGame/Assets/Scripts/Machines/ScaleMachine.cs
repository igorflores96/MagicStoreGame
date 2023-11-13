using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleMachine : MonoBehaviour
{
    [SerializeField] Transform _objectPosition;
    private Item _objectToScale;


    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == 20)
        {
            _objectToScale = other.GetComponent<Item>();
            _objectToScale.IsScalingUp = false;
            _objectToScale.IsScalingDown = false;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(_objectToScale != null && other.gameObject.layer == 20)
        {
            _objectToScale = null;
        }
    }

    public void ActiveScaleUp()
    {
        if(_objectToScale != null)
        {
            _objectToScale.IsScalingDown = false;
            _objectToScale.IsScalingUp = true;
        }

    }

    public void ActiveScaleDown()
    {
        if(_objectToScale != null)
        {
            _objectToScale.IsScalingUp = false;
            _objectToScale.IsScalingDown = true;
        }

    }
}
