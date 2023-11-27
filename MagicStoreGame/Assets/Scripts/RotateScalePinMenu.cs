using UnityEngine;

public class RotateScalePinMenu : MonoBehaviour
{
    public float speed = 50f; // Velocidade de rotação do objeto

    // Atualização a cada frame
    void Update()
    {
        // Girar o objeto ao longo do eixo Y (pode ser ajustado para outros eixos conforme necessário)
        transform.Rotate(Vector3.left, speed * Time.deltaTime);
    }
}