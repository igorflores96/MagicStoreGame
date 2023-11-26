using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugNextScene : MonoBehaviour
{
    void Start()
    {
        AvancarParaProximaCena();
    }

    public void AvancarParaProximaCena()
    {
        // Carregar a próxima cena com base no índice da cena atual + 1
        int indiceProximaCena = SceneManager.GetActiveScene().buildIndex + 1;

        // Certificar-se de que o índice da próxima cena não ultrapasse o número de cenas disponíveis
        if (indiceProximaCena < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(indiceProximaCena);
        }
        else
        {
            Debug.LogWarning("Não há mais cenas disponíveis.");
            // Ou, se preferir, pode voltar para a primeira cena:
            // SceneManager.LoadScene(0);
        }
    }
}
