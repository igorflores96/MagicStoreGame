using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform objetoParaEscalar;
    public Vector3 fatorDeEscala = new Vector3(1.0f, 2.0f, 1.0f);

    void Update()
    {
        // Verifica se o objeto para escalar foi atribuído
        if (objetoParaEscalar != null)
        {
            // Obtém a posição do pivô do objeto pai
            Vector3 pivotPosition = objetoParaEscalar.position;

            // Move o objeto para escalar para o pivô do objeto pai
            objetoParaEscalar.position = Vector3.zero;

            // Aplica a escala ao objeto
            Vector3 novaEscala = objetoParaEscalar.localScale + new Vector3(0.01f, 0.01f, 0.01f);

            // Garante que a nova escala não ultrapasse o tamanho máximo usando Mathf.Clamp
            novaEscala.x = Mathf.Clamp(novaEscala.x, 1, 1);
            novaEscala.y = Mathf.Clamp(novaEscala.y, 1, 20);
            novaEscala.z = Mathf.Clamp(novaEscala.z, 1, 1);

            // Aplica a nova escala ao objeto
            objetoParaEscalar.localScale = novaEscala;

            // Move o objeto de volta para a posição original do pivô
            objetoParaEscalar.position = pivotPosition;
        }
    }
}
