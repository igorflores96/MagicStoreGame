using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantmentSpray : MonoBehaviour
{
    [SerializeField] private EnchantmentType _enchantmentType;
    [SerializeField] private EnchantmentMachine _machine;

    public EnchantmentType EnchantmentTypeSpray
    {
        get { return _enchantmentType;}
    }

    public void Use()
    {
        FindObjectOfType<AudioManager>().Play("SprayCan");

        _machine.IsEnchantig = true;
    }

    public void StopUse()
    {
        FindObjectOfType<AudioManager>().Stop("SprayCan");
        _machine.IsEnchantig = false;
    }
}
