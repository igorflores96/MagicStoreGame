using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] private ScaleMachine _scaleMachine;
    [SerializeField] private PotionMachine _potionMachine;
    [SerializeField] private Microwave _microwave;
    [SerializeField] private SubWitch _subWitch;
    [SerializeField] private string _buttonName;

    public void SetScaleMachineUp()
    {
        _scaleMachine.ActiveScaleUp();
    }

    public void SetScaleMachineDown()
    {
        _scaleMachine.ActiveScaleDown();
    }

    public void SetWaterOn()
    {
        _potionMachine.UseWather();
    }

    public void SetFireOn()
    {
        Debug.Log("Ligou o Fogo");
    }
    public void FusionItem()
    {
       StartCoroutine(_microwave.Timer());
    }
    public void VerificarItem()
    {
        StartCoroutine(_subWitch.Timer());
    }
    public string ButtonName
    {
        get {return _buttonName;}
    }
}
