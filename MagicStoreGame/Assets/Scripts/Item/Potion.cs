using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Potion : MonoBehaviour, IItem
{
    [SerializeField] private float _potionMaxMl;
    [SerializeField] private string _itemName;
    [SerializeField] private Transform _labelPosition;
    [SerializeField] private Transform _harmonyPositon;
    [SerializeField] private Transform _mindPosition;
    [SerializeField] private Transform _wisdomPosition;
    [SerializeField] private Transform _youthPosition;
    [SerializeField] private GameObject _whater;
    [SerializeField] private GameObject _dye;
    [SerializeField] private GameObject _stopper;
    private Color _potionColor;
    private int _colorQuantity;
    private int _itemValue;
    private EnchantmentType _currentElement;
    private float _currentMl;
    private int _velocityWater;
    private bool _isPouringWater;
    private bool _stickLabel;
    private bool _dyePotion;
    private Coroutine startFireCoroutine;

    void OnEnable()
    {
        _colorQuantity = 0;
        _stickLabel = false;
        _dyePotion = false;
       _currentMl = 0.0f; 
       _stopper.SetActive(false);
    }

    void Update()
    {
        if(_isPouringWater)
        {
            if(_currentMl < _potionMaxMl)
            {
                _currentMl += 1.0f * _velocityWater * Time.deltaTime;
                float scale;
                
                if(_potionMaxMl == 15.0f)
                    scale = 0.018f;
                else
                    scale = 1.6f;

                float newScaleY = Mathf.Clamp(_currentMl / _potionMaxMl, 0.0f, 1.0f) * scale;
                
                    
                _whater.SetActive(true);
                _whater.transform.localScale = new Vector3(_whater.transform.localScale.x, newScaleY, _whater.transform.localScale.z);
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
        _isPouringWater = status;
    }

    public void StickTheLabel(Transform labelToStick, Label label)
    {
        if(!_stickLabel)
        {
            Destroy(labelToStick.GetComponent<Collider>());
            Destroy(labelToStick.GetComponent<Rigidbody>());
            labelToStick.position = _labelPosition.position;
            labelToStick.parent = this.transform;
            _itemName = label.LabelName;
            _stickLabel = true;
        }
    }

    public void ChangeLabelName(Transform littleLabelToStick, LittleLabel label)
    {
        if(_stickLabel)
        {

            Destroy(littleLabelToStick.GetComponent<Collider>());
            Destroy(littleLabelToStick.GetComponent<Rigidbody>());
            littleLabelToStick.parent = this.transform;
            switch (_itemName)
            {
                case "Potion Of Wisdom": _itemName = "Potion Of " + label.LittleLabelName; littleLabelToStick.position = _wisdomPosition.position;
                break;
                case "Potion Of Eternal Youth": _itemName = "Potion Of Eternal " + label.LittleLabelName; littleLabelToStick.position = _youthPosition.position;
                break;
                case "Harmony Elixir": _itemName = label.LittleLabelName + " Elixir"; littleLabelToStick.position = _harmonyPositon.position;
                break;
                case "Elixir Of Mind Control": _itemName = "Elixir Of " + label.LittleLabelName + " Control"; littleLabelToStick.position = _mindPosition.position;
                break;
            }
        }
    }

    public void DyePotion(Color color)
    {
        if(!_dyePotion || _potionColor == color)
        {
            _dye.SetActive(true);
            _dye.GetComponentInChildren<MeshRenderer>().material.color = color;
            _potionColor = color;
            _colorQuantity++;
            _dyePotion = true;
        }
    }

    public int IncreaseValueItem(int multiplier)
    {
        _itemValue *= multiplier;

        return _itemValue;
    }

    public void StopCooking()
    {
        if (startFireCoroutine != null)
        {
            Debug.Log("Parou de cozinhar");
            StopCoroutine(startFireCoroutine);
            startFireCoroutine = null; 
        }
    }

    public void CookPotion(float timeToCook)
    {
        if(_currentMl >= _potionMaxMl && _dyePotion)
        {
            if (startFireCoroutine != null)
            {
                StopCoroutine(startFireCoroutine);
            }

            startFireCoroutine = StartCoroutine(StartFire(timeToCook));
        }

    }

    IEnumerator StartFire(float timeToCook)
    {
        yield return new WaitForSeconds(timeToCook);
        
        _stopper.SetActive(true);
        _dye.SetActive(false);
        _whater.GetComponentInChildren<MeshRenderer>().material.color = _potionColor;
        startFireCoroutine = null; 
    }



    public bool IsPouring
    {
        get {return _isPouringWater;}
        set {_isPouringWater = value;}
    }

    public int ValueItem
    {
        get {return _itemValue;}
        set { _itemValue = value;} 
    }

    public string NameItem
    {
        get {return _itemName;}
        set { _itemName = value;}
    }

    public EnchantmentType EnchantmentItem
    {
        get {return _currentElement;}
        set {_currentElement = value;}
    }

    public int PotionColorQuantity
    {
        get {return _colorQuantity;}
    }
}
