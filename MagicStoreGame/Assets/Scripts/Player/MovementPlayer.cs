using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class MovementPlayer : MonoBehaviour
{
    [Header("Character Controller")]
    [SerializeField] private CharacterController _characterController;
    
    [Header("Câmera and Grab Point Transforms")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _grabPointTransform;
    
    [Header("Variables to movement")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _mouseSensitivity;
    
    [Header("Item to Grab Mask and Distance to Grab")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _grabRayCastDistance;
    private PlayerMovement _playerMovement;
    private float _rotationX = 0;
    private Transform _currentItemGrabed;
    private PlayerInput _playerInput;
    void Awake()
    {
        _currentItemGrabed = null;
      _playerMovement = new PlayerMovement();
      _playerInput = GetComponent<PlayerInput>();
      _playerMovement.MovementPlayer.GrabItem.performed += GrabItem;
        
      _playerMovement.MovementPlayer.Enable();
        _playerInput.onActionTriggered += PressingMachine;
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
                    hit.transform.SetParent(this.transform);
                } 
            }
            else
            {
                _currentItemGrabed.parent = null;
                _currentItemGrabed = null;
            }

        }

    }
    private void PressingMachine(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RaycastHit hit;

            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, _grabRayCastDistance, 3))
            {
                Debug.Log("Scale Up BOTÃO");
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
    }

    private void LateUpdate() 
    {
        _grabPointTransform.position = _cameraTransform.position + (_cameraTransform.forward * 0.5f);

        if(_currentItemGrabed != null)
            _currentItemGrabed.position = _cameraTransform.position + (_cameraTransform.forward * 0.5f);
    }
}
