using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] private ScaleMachine _scaleMachine;
    [SerializeField] private string _buttonName;

    public void SetScaleMachineUp()
    {
        _scaleMachine.ActiveScaleUp();
    }

    public void SetScaleMachineDown()
    {
        _scaleMachine.ActiveScaleDown();
    }

    public string ButtonName
    {
        get {return _buttonName;}
    }
}
