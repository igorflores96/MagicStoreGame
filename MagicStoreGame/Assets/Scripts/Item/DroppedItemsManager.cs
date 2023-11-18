using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawn : MonoBehaviour
{
    private Vector3 originalPosition;
    private Rigidbody rb;

    [Header ("||| Choose only one boolean bellow |||")]
    public bool isIngredient;
    public bool isRecipe;

    void Start()
    {
        originalPosition = transform.position; // Salva coordenada inicial do item
        rb = GetComponent<Rigidbody>(); // Obtém o componente Rigidbody do item
    }

    void OnTriggerEnter(Collider other)
    {
        if (isIngredient) { ResetItemPosition();  }
        if (isRecipe)     { DestroyItemProcess(); }
    }

    void ResetItemPosition()
    {
        rb.isKinematic = true; // Desativa o movimento do item
        transform.position = originalPosition; // Redefine a posição para a posição inicial
        rb.isKinematic = false; // Liga novamente o movimento do item
    }

    void DestroyItemProcess()
    {
        //FINALIZAR LOGICA AQUI PARA ITEM PARAR DE SER DESTRUIDO QUANDO O ITEM FOR PEGO DO CHÃO (RESETAR O 'TIMER')
        //ADICIONAR JUICE DO IOTEM SENDO DESTRUIDO
        Destroy(gameObject, 10f); // Destruir o objeto após 10 segundos
    }
}