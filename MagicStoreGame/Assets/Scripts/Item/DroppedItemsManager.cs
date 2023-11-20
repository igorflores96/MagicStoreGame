using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemsManager : MonoBehaviour
{
    private Vector3 originalPosition;
    private Rigidbody rb;

    void Start()
    {
        originalPosition = transform.position; // Salva coordenada inicial do item
        rb = GetComponent<Rigidbody>(); // Obtém o componente Rigidbody do item
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("FloorResetItem"))
            ResetItemPosition();
    }

    void ResetItemPosition()
    {
        rb.isKinematic = true; // Desativa o movimento do item
        transform.position = originalPosition; // Redefine a posição para a posição inicial
        rb.isKinematic = false; // Liga novamente o movimento do item
    }
}