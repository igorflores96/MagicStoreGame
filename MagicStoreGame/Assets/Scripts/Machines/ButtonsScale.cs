using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScale : MonoBehaviour
{
    [SerializeField] private ScaleMachine _machine;

    public void SetMachineUp()
    {
        _machine.ActiveScaleUp();
    }

    public void SetMachineDown()
    {
        _machine.ActiveScaleDown();
    }
}
