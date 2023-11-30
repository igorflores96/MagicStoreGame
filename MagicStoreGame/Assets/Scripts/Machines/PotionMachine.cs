using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMachine : MonoBehaviour
{
    [SerializeField] private int _multiplierVelocityWather;
    [SerializeField] private float _timeToCook;

    private Potion _currentPotion;

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.layer == 20)
        {
            other.TryGetComponent(out _currentPotion);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.layer == 20)
        {
            if(_currentPotion != null)
            {
                _currentPotion.IsPouring = false;
                _currentPotion.StopCooking();
            }

            _currentPotion = null;
        }
    }

    public void UseWather()
    {
        if(_currentPotion != null)
            _currentPotion.StartingPouringWater(true, _multiplierVelocityWather);
    }

    public void UseFire()
    {
        if(_currentPotion != null)
            _currentPotion.CookPotion(_timeToCook);
    }
}
