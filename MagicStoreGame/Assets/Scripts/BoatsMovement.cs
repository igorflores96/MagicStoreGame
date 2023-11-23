using UnityEngine;

public class BoatsMovement : MonoBehaviour
{
    public float velocidadeX = 1.0f; // Velocidade de movimento no eixo X
    public float velocidadeY = 1.0f; // Velocidade de movimento no eixo Z

    void Update()
    {
        // Movimento automático no eixo X e Z
        float movimentoX = velocidadeX * Time.deltaTime;
        float movimentoY = velocidadeY * Time.deltaTime;

        // Movimenta o objeto
        transform.Translate(new Vector3(movimentoX, movimentoY, 0.0f));
    }
}
