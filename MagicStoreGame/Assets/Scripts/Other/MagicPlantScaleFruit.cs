using UnityEngine;
using System.Collections;

public class MagicPlantScaleFruit : MonoBehaviour
{
    public float maxScale = 2f; // Valor máximo de escala
    public float minScale = 0.5f; // Valor mínimo de escala
    public float scaleSpeed = 1f; // Velocidade de escala
    public float pauseTime = 2f; // Tempo de pausa em segundos entre as escalas

    private Vector3 initialScale; // Escala inicial do objeto
    private bool scalingUp = true; // Flag para indicar se está escalando para cima

    private void Start()
    {
        initialScale = transform.localScale;
        // Iniciar a rotina de escala
        StartScaling();
    }

    private void StartScaling()
    {
        // Iniciar a corrotina de escalonamento
        StartCoroutine(ScaleRoutine());
    }

    private IEnumerator ScaleRoutine()
    {
        while (true)
        {
            if (scalingUp)
            {
                // Escalando para o valor máximo
                while (transform.localScale.x < maxScale)
                {
                    transform.localScale += Vector3.one * Time.deltaTime * scaleSpeed;
                    yield return null;
                }

                // Aguardando o tempo de pausa
                yield return new WaitForSeconds(pauseTime);

                scalingUp = false; // Mudar para escalar para baixo
            }
            else
            {
                // Escalando para o valor mínimo
                while (transform.localScale.x > minScale)
                {
                    transform.localScale -= Vector3.one * Time.deltaTime * scaleSpeed;
                    yield return null;
                }

                // Aguardando o tempo de pausa
                yield return new WaitForSeconds(pauseTime);

                scalingUp = true; // Mudar para escalar para cima
            }
        }
    }
}
