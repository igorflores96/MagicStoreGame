using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private float _potionMaxMl;
    private float _currentMl;
    private int _velocityWater;
    private bool _isPouring;
    void Start()
    {
       _currentMl = 0.0f; 
    }

    void Update()
    {
        if(_isPouring)
        {
            if(_currentMl <= _potionMaxMl)
            {
                _currentMl += 1.0f * _velocityWater * Time.deltaTime;
            }
            else
            {
                _currentMl = _potionMaxMl;
            }
        }
    }

    public void StartingPouringWater(bool status, int velocity)
    {
        _velocityWater = velocity;
        _isPouring = status;
    }

    public bool IsPouring
    {
        get {return _isPouring;}
        set {_isPouring = value;}
    }
}
