using TMPro;
using UnityEngine;

public class ScalesPot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueText;
    private GameObject[] escamas; // Array para armazenar as escamas childrens dentro do pote
    public int currentScales = 0; // Número atual de escamas no pote

    void Start()
    {
        int childCount = transform.childCount;
        escamas = new GameObject[childCount];

        // Preenchendo o array com as escamas childrens
        for (int i = 0; i < childCount; i++)
        {
            escamas[i] = transform.GetChild(i).gameObject;
        }
        _valueText.text = currentScales.ToString("000");
        UpdateEscamasNoPote();
    }

    void Update()
    {
        UpdateEscamasNoPote();
    }

    // Atualizando visualmente as escamas no pote
    void UpdateEscamasNoPote()
    {
        for (int i = 0; i < escamas.Length; i++)
        {
            escamas[i].SetActive(i < currentScales);
        }
    }

    // Método para aumentar o número de escamas (Usar para quando vender itens para a bruxa)
    public void AdicionarEscamas(int quantidade)
    {
        currentScales += quantidade;
        _valueText.text = currentScales.ToString("000");
    }

    // Método para diminuir o número de escamas (Utilizar quando comprar upgrades para a loja)
    public void RemoverEscamas(int quantidade)
    {
        currentScales -= quantidade;
        currentScales = Mathf.Clamp(currentScales, 0, escamas.Length);
    }
}
