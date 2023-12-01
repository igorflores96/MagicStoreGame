using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleMachine : MonoBehaviour
{
    private Item _objectToScale;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == 20)
        {
            if(other.GetComponent<Item>() == true)
            {
                _objectToScale = other.GetComponent<Item>();
                _objectToScale.IsScalingUp = false;
                _objectToScale.IsScalingDown = false;
            }

        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(_objectToScale != null && other.gameObject.layer == 20)
        {
            _objectToScale = null;
            FindObjectOfType<AudioManager>().Stop("ScaleWavesDown");
            FindObjectOfType<AudioManager>().Stop("ScaleWavesUp");
        }
    }

    public void ActiveScaleUp()
    {
        
        if(_objectToScale != null)
        {
            FindObjectOfType<AudioManager>().Play("ScaleWavesUp");
            _objectToScale.IsScalingDown = false;
            _objectToScale.IsScalingUp = true;
        }

    }

    public void ActiveScaleDown()
    {
        if(_objectToScale != null)
        {
            FindObjectOfType<AudioManager>().Play("ScaleWavesDown");
            _objectToScale.IsScalingUp = false;
            _objectToScale.IsScalingDown = true;
        }

    }
}
