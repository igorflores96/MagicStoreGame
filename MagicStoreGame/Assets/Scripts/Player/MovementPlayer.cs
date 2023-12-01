using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using UnityEngine.Events;
using Unity.VisualScripting;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour
{
    [Header("Character Controller")]
    [SerializeField] private CharacterController _characterController;
    
    [Header("Câmera and Grab Point Transforms")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _grabPointTransform;
    
    [Header("Variables to movement")]
    [SerializeField] private float _playerMoveSpeed;
    [SerializeField] private float _playerRunSpeed;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _maxLookUp;
    [SerializeField] private float _maxLookDown;

    
    [Header("Item to Grab Mask and Distance to Grab")]
    [SerializeField] private LayerMask _layerGrabItem;
    [SerializeField] private float _grabRayCastDistance;
    [Header("Layers of Action")]
    [SerializeField] private LayerMask _layerButton;
    [SerializeField] private LayerMask _layerMachineEnchant;
    [SerializeField] private LayerMask _layerStorage;
    [SerializeField] private LayerMask _layerClient;

    public Slider _sliderMouseSensitivity; 



    private PlayerMovement _playerMovement;
    private float _rotationX = 0;
    private Transform _currentItemGrabed;

    private Dictionary<string, Action<Buttons>> _buttonActions = new Dictionary<string, Action<Buttons>>();

    public static UnityEvent<EnchantmentType> OnSprayPickedUp = new UnityEvent<EnchantmentType>();
    public bool isGrabbed = false;

    void Awake()
    {
        _buttonActions["ButtonDown"] = SetMachineDown;
        _buttonActions["ButtonUp"] = SetMachineUp;
        _buttonActions["ButtonWater"] = TurnOnWater;
        _buttonActions["ButtonFire"] = TurnOnFire;
        _buttonActions["LeverMicro"] = FusionItem;
        _buttonActions["DoorSub"] = VerificarItem;


        _playerMovement = new PlayerMovement();
      
        _playerMovement.MovementPlayer.GrabItem.performed += GrabItem;
        _playerMovement.MovementPlayer.UseMachine.performed += UseButton;  //microondas, box collider = adicionar layer 10 de botao na alavanca
        _playerMovement.MovementPlayer.UseLabel.performed += UseLabel;
        _playerMovement.MovementPlayer.GrabItem.performed += TalkToClient;
        _playerMovement.MovementPlayer.Enable();     
         
    }

    private void Start()
    {
        if (_sliderMouseSensitivity != null)
        {
            _sliderMouseSensitivity.onValueChanged.AddListener(UpdateSliderMouseSensitivityValue);
        }
    }

    void Update()
    {
        MovePlayer();
        CameraLook();
        UseItem();
    }

    private void UpdateSliderMouseSensitivityValue(float value)
    {
        _mouseSensitivity = value;
    }


    private void GrabItem(InputAction.CallbackContext context)
    {
        
        if(context.performed)
        {
            if(_currentItemGrabed == null)
            {
                RaycastHit hit;
                Item tempItem;
                EnchantmentSpray spray;
                

                if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, _grabRayCastDistance, _layerGrabItem))
                {
                    _currentItemGrabed = hit.transform;
                    _currentItemGrabed.rotation = Quaternion.identity;
                        
                    if (_currentItemGrabed.TryGetComponent(out tempItem))
                    {
                        tempItem.IsScalingUp = false;
                        tempItem.IsScalingDown = false;
                    }
                    isGrabbed = true;
                    if (_currentItemGrabed.TryGetComponent(out spray))
                    {
                        OnSprayPickedUp.Invoke(spray.EnchantmentTypeSpray);
                    }
                }
                else if(Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, _grabRayCastDistance, _layerStorage))
                {

                    Storage tempStorage;
                    
                    if(hit.transform.TryGetComponent(out tempStorage))
                    {
                        _currentItemGrabed = tempStorage.GetItemFromStorage();
                    }
            
                    if (_currentItemGrabed.TryGetComponent(out tempItem))
                    {
                        tempItem.IsScalingUp = false;
                        tempItem.IsScalingDown = false;
                    }

                    if(_currentItemGrabed.TryGetComponent(out spray))
                    {
                        OnSprayPickedUp.Invoke(spray.EnchantmentTypeSpray);
                    }
                } 
            }
            else
            {
                Rigidbody temp;
                if(_currentItemGrabed.TryGetComponent(out temp))
                {
                    temp.constraints = RigidbodyConstraints.None;
                }
                _currentItemGrabed.rotation = Quaternion.identity;
                _currentItemGrabed = null;
                isGrabbed = false;
            }

        }

    }
    private void MovePlayer()
    {
        float inputVectorValueX = _playerMovement.MovementPlayer.Movement.ReadValue<Vector2>().x;
        float inputVectorValueY = _playerMovement.MovementPlayer.Movement.ReadValue<Vector2>().y;
        bool isRunning = _playerMovement.MovementPlayer.Run.ReadValue<float>() > 0.1f;
        
        Vector3 cameraForward = _cameraTransform.forward;
        Vector3 cameraRight = _cameraTransform.right;
        cameraForward.y = cameraRight.y = 0.0f;

        Vector3 moveDirection = (cameraForward.normalized * inputVectorValueY + cameraRight.normalized * inputVectorValueX).normalized;

        if(!isRunning)
            _characterController.Move(moveDirection * _playerMoveSpeed * Time.deltaTime);
        else
            _characterController.Move(moveDirection * _playerRunSpeed * Time.deltaTime);

    }

    private void CameraLook()
    {
       
        float mouseX = _playerMovement.MovementPlayer.MouseLook.ReadValue<Vector2>().x;
        float mouseY = _playerMovement.MovementPlayer.MouseLook.ReadValue<Vector2>().y;

        _rotationX -= mouseY * _mouseSensitivity;
        _rotationX = Mathf.Clamp(_rotationX, -_maxLookUp, _maxLookDown);

        _cameraTransform.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX * _mouseSensitivity, 0);
    }

    private void UseButton(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            RaycastHit hit;
            Buttons temp;

            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, _grabRayCastDistance, _layerButton))
            {      
                if (hit.transform.TryGetComponent(out temp))
                {
                    if(_buttonActions.ContainsKey(temp.ButtonName))
                    {
                        _buttonActions[temp.ButtonName]?.Invoke(temp);
                    }

                }
            }
        }
    }

    private void UseItem()
    {
        bool usingItem = _playerMovement.MovementPlayer.UseSpray.ReadValue<float>() > 0.1f;

        if(usingItem)
        {
            if(_currentItemGrabed != null)
            {
                if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, _grabRayCastDistance, _layerMachineEnchant))
                {   
                    EnchantmentSpray spray;
                    if(_currentItemGrabed.TryGetComponent(out spray))
                    {
                        spray.Use();
                    }

                }
            }
        }
        else
        {
            if(_currentItemGrabed != null)
            {
                EnchantmentSpray spray;

                if(_currentItemGrabed.TryGetComponent(out spray))
                {
                    spray.StopUse();
                }

            } 
        }
    }

    private void TalkToClient(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _grabRayCastDistance, _layerClient))
            {
                if(hit.transform.TryGetComponent(out ForgeClient forgeClient))
                {
                    forgeClient.SetDialogToSay();
                }
                
                if(hit.transform.TryGetComponent(out SellClient sellClient))
                {
                    sellClient.SetDialogToSay();
                }
            }
        }
    }

    private void UseLabel(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (_currentItemGrabed != null)
            {
                
                Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
                RaycastHit hit;

                if(_currentItemGrabed.TryGetComponent(out Label label) || _currentItemGrabed.TryGetComponent(out Dye dye) || _currentItemGrabed.TryGetComponent(out OilFlask oil))
                    _currentItemGrabed.GetComponent<Collider>().enabled = false;

                if (Physics.Raycast(ray, out hit, _grabRayCastDistance))
                {
                    if (hit.collider.CompareTag("Potion"))
                    {
                        Potion potionTemp;
                        if(hit.transform.TryGetComponent(out potionTemp))
                        { 
                            if(_currentItemGrabed.TryGetComponent(out label))
                            {
                                _currentItemGrabed.transform.rotation = Quaternion.identity;
                                potionTemp.StickTheLabel(_currentItemGrabed, label);
                                _currentItemGrabed = null;
                            }
                            else if(_currentItemGrabed.TryGetComponent(out dye))
                            {
                                potionTemp.DyePotion(dye);
                            }
                            else if(_currentItemGrabed.TryGetComponent(out LittleLabel littleLabel))
                            {
                                _currentItemGrabed.transform.rotation = Quaternion.Euler(0, 0, 0);
                                potionTemp.ChangeLabelName(_currentItemGrabed, littleLabel);
                                _currentItemGrabed = null;
                            }
                            else if(_currentItemGrabed.TryGetComponent(out oil))
                            {
                                oil.PuringOil(potionTemp);
                            }
                            
                        }
                    }
                }
            }
        }

    }

    private void SetMachineDown(Buttons button)
    {
        button.SetScaleMachineDown();
    }

    private void SetMachineUp(Buttons button)
    {
        button.SetScaleMachineUp();
    }

    private void TurnOnWater(Buttons button)
    {
       button.SetWaterOn(); 
    }
    private void TurnOnFire(Buttons button)
    {
       button.SetFireOn(); 
    }
    private void FusionItem(Buttons button)
    {
        button.FusionItem();
    }
    private void VerificarItem(Buttons button)
    {
        button.VerificarItem();
    }
    private void LateUpdate() 
    {
        _grabPointTransform.position = _cameraTransform.position + (_cameraTransform.forward * 2.0f);

        if(_currentItemGrabed != null)
        {
            _currentItemGrabed.position = _cameraTransform.position + (_cameraTransform.forward * 2.0f);
            _currentItemGrabed.LookAt(new Vector3(_cameraTransform.position.x, _cameraTransform.position.y, 0.0f));
        }
            
    }
}
