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
    
    [Header("Item to Grab Mask and Distance to Grab")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _grabRayCastDistance;
    [Header("Layer Button Up e Down")]
    [SerializeField] private LayerMask _layerUp;
    [SerializeField] private LayerMask _layerDown;
    [SerializeField] private LayerMask _enchantLayer;

    private PlayerMovement _playerMovement;
    private float _rotationX = 0;
    private Transform _currentItemGrabed;

    public static UnityEvent<EnchantmentType> OnSprayPickedUp = new UnityEvent<EnchantmentType>();

    void Awake()
    {
      _playerMovement = new PlayerMovement();
      
      _playerMovement.MovementPlayer.GrabItem.performed += GrabItem;
      _playerMovement.MovementPlayer.UseMachine.performed += UsingMachine;
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

                if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, _grabRayCastDistance, _layerMask))
                {
                    _currentItemGrabed = hit.transform;
                    _currentItemGrabed.rotation = Quaternion.identity;
                    Item tempItem;
                    Rigidbody tempRb;
                    EnchantmentSpray spray;

                    if(_currentItemGrabed.TryGetComponent(out tempRb))
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
        _rotationX = Mathf.Clamp(_rotationX, -10, 30);

        _cameraTransform.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX * _mouseSensitivity, 0);

        _dot.transform.position = _cameraTransform.position + (_cameraTransform.forward * 0.5f);
    }

    private void UsingMachine(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            RaycastHit hit;
            ButtonsScale temp;

            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, _grabRayCastDistance, _layerDown))
            {      
                if (hit.transform.TryGetComponent(out temp))
                {
                    temp.SetMachineDown();
                }
            }
            
            if(Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, _grabRayCastDistance, _layerUp))
            {
                if (hit.transform.TryGetComponent(out temp))
                {
                    temp.SetMachineUp();
                }
            }
        }
    }

    private void UsingSpray(InputAction.CallbackContext context)
    {
        if(_currentItemGrabed != null)
        {
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, _grabRayCastDistance, _enchantLayer))
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
    private void LateUpdate() 
    {
        _grabPointTransform.position = _cameraTransform.position + (_cameraTransform.forward * 0.5f);

        if(_currentItemGrabed != null)
            _currentItemGrabed.position = _cameraTransform.position + (_cameraTransform.forward * 0.5f);
    }
}
