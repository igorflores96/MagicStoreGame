using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantmentMachine : MonoBehaviour
{
    [SerializeField] private List<Color> _colors;
    [SerializeField] private float _timeToEnchant;
    private EnchantmentType _currentEnchantment;
    private ParticleSystem _objectToEnchant;
    private bool _isEnchanting;
    private float _timer;
    

    private void OnEnable()
    {
        _isEnchanting = false;
        _timer = 0.0f;
        MovementPlayer.OnSprayPickedUp.AddListener(SetEnchantmentTypeFromPlayer);
    }

    private void OnDisable()
    {
        MovementPlayer.OnSprayPickedUp.RemoveListener(SetEnchantmentTypeFromPlayer);
    }
    
    
    void Update()
    {
        if(_isEnchanting)
        {
            _timer -= 1.0f * Time.deltaTime;
            if(_timer <= 0.0f)
            {
                _isEnchanting = false;
                ApplyEnchantment();
            }
        }
    }

    private void SetEnchantmentTypeFromPlayer(EnchantmentType enchantmentType)
    {
        if(_currentEnchantment != enchantmentType) //We doing this here because if is the same type we continue with the same progress
            _timer = _timeToEnchant;

        _currentEnchantment = enchantmentType;
    }

    private void ApplyEnchantment()
    {
        if(_objectToEnchant != null)
        {
            var mainModule = _objectToEnchant.main; //Only way to access the startcolor
            _objectToEnchant.Play();
            mainModule.startColor = _colors[(int)_currentEnchantment];
            _timer = _timeToEnchant;
        }
    }

    public bool IsEnchantig
    {
        get {return _isEnchanting;}
        set {_isEnchanting = value;} 
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.layer == 20)
        {
            _objectToEnchant = other.GetComponentInChildren<ParticleSystem>();
        }
    }
}
