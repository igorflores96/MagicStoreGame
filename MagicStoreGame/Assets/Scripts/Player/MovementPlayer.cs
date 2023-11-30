using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using System.Security.Cryptography;
using UnityEngine.Events;

public class MovementPlayer : MonoBehaviour
{
    [Header("Character Controller")]
    [SerializeField] private CharacterController _characterController;
    
    [Header("Câmera and Grab Point Transforms")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _grabPointTransform;
    [SerializeField] private Transform _dot;
    
    [Header("Variables to movement")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _maxLookUp;
    [SerializeField] private float _maxLookDown;

    
    [Header("Item to Grab Mask and Distance to Grab")]
    [SerializeField] private LayerMask _layerGrabItem;
    [SerializeField] private float _grabRayCastDistance;
    [Header("Layers of Action")]
    [SerializeField] private LayerMask _layerButton;
    [SerializeField] private LayerMask _layerMachineEnchant;

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
      _playerMovement.MovementPlayer.UseMachine.performed += UsingMachine;  //microondas, box collider = adicionar layer 10 de botao na alavanca
      _playerMovement.MovementPlayer.UseItem.started += UsingSpray;
      _playerMovement.MovementPlayer.UseItem.canceled += StopUsingSpray;

      _playerMovement.MovementPlayer.Enable();      
    }

    void Update()
    {
        MovePlayer();
        CameraLook();
    }

    private void GrabItem(InputAction.CallbackContext context)
    {
        
        if(context.performed)
        {
            if(_currentItemGrabed == null)
            {
                RaycastHit hit;

                if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, _grabRayCastDistance, _layerGrabItem))
                {
                    _currentItemGrabed = hit.transform;
                    _currentItemGrabed.rotation = Quaternion.identity;
                    Item tempItem;
                    Rigidbody tempRb;
                    EnchantmentSpray spray;
                    isGrabbed = true;
                    if (_currentItemGrabed.TryGetComponent(out tempRb))
                    {
                        tempRb.freezeRotation = true;
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

                Rigidbody tempRb;

                if(_currentItemGrabed.TryGetComponent(out tempRb))
                {
                    tempRb.constraints = RigidbodyConstraints.None;
                }
                _currentItemGrabed = null;
                isGrabbed = false;
            }

        }

    }
    private void MovePlayer()
    {
        float inputVectorValueX = _playerMovement.MovementPlayer.Movement.ReadValue<Vector2>().x;
        float inputVectorValueY = _playerMovement.MovementPlayer.Movement.ReadValue<Vector2>().y;
        
        Vector3 cameraForward = _cameraTransform.forward;
        Vector3 cameraRight = _cameraTransform.right;
        cameraForward.y = cameraRight.y = 0.0f;

        Vector3 moveDirection = (cameraForward.normalized * inputVectorValueY + cameraRight.normalized * inputVectorValueX).normalized;

        _characterController.Move(moveDirection * _playerSpeed * Time.deltaTime);
    }

    private void CameraLook()
    {
       
        float mouseX = _playerMovement.MovementPlayer.MouseLook.ReadValue<Vector2>().x;
        float mouseY = _playerMovement.MovementPlayer.MouseLook.ReadValue<Vector2>().y;

        _rotationX -= mouseY * _mouseSensitivity;
        _rotationX = Mathf.Clamp(_rotationX, -_maxLookUp, _maxLookDown);

        _cameraTransform.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX * _mouseSensitivity, 0);

        _dot.transform.position = _cameraTransform.position + (_cameraTransform.forward * 0.5f);
    }

    private void UsingMachine(InputAction.CallbackContext context)
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

    private void UsingSpray(InputAction.CallbackContext context)
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

    private void StopUsingSpray(InputAction.CallbackContext context)
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
            _currentItemGrabed.position = _cameraTransform.position + (_cameraTransform.forward * 2.0f);
    }
}
