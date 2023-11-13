using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    private void OnEnable() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        
    }
}
