using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;
    [SerializeField] Transform _cameraTransform;
    [SerializeField] float _playerSpeed;
    [SerializeField] float _mouseSensitivity;

    private PlayerMovement _playerMovement;
    private float _rotationX = 0;
    void Awake()
    {
      _playerMovement = new PlayerMovement();  

      _playerMovement.MovementPlayer.GrabItem.performed += GrabItem;
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
            Debug.Log("Pegou o item");
        }

    }

    private void MovePlayer()
    {
        float inputVectorValueX = _playerMovement.MovementPlayer.Movement.ReadValue<Vector2>().x;
        float inputVectorValueY = _playerMovement.MovementPlayer.Movement.ReadValue<Vector2>().y;
        
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
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
}
