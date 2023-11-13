using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleMachine : MonoBehaviour
{
    [SerializeField] Transform _objectPosition;
    public ScaleObject _objectToScale;


    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == 20)
        {
            other.transform.position = _objectPosition.position;
            _objectToScale = other.GetComponent<ScaleObject>();
            _objectToScale.IsScalingUp = false;
            _objectToScale.IsScalingDown = false;
        }
            

    }

    public void ActiveScaleUp()
    {
        _objectToScale.IsScalingDown = false;
        _objectToScale.IsScalingUp = true;
    }

    public void ActiveScaleDown()
    {
        _objectToScale.IsScalingUp = false;
        _objectToScale.IsScalingDown = true;
    }
}
