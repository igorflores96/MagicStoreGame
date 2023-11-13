using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScaleObject : MonoBehaviour
{
    public float maxSizeUp;
    public float maxSizeDown;
    public float scalePerFrame;
    private bool isScalingUp;
    private bool isScalingDown;

    private void Awake() {
        isScalingUp = false;
        isScalingDown = false;
    }

    void Update()
    {

            if (isScalingUp && transform.localScale.x < maxSizeUp)
            {
                Scale(scalePerFrame);
            }

            if (isScalingDown && transform.localScale.x > maxSizeDown)
            {
                Scale(-scalePerFrame);
            }

    }
    private void Scale(float scalingVal)
    {
        // Calcula a nova escala usando a taxa de escalamento
        Vector3 novaEscala2 = transform.localScale + new Vector3(scalingVal, scalingVal, scalingVal);

        // Garante que a nova escala n�o ultrapasse o tamanho m�ximo usando Mathf.Clamp
        novaEscala2.x = Mathf.Clamp(novaEscala2.x, maxSizeDown, maxSizeUp);
        novaEscala2.y = Mathf.Clamp(novaEscala2.y, maxSizeDown, maxSizeUp);
        novaEscala2.z = Mathf.Clamp(novaEscala2.z, maxSizeDown, maxSizeUp);

        // Aplica a nova escala ao objeto
        transform.localScale = novaEscala2;
    }

    public bool IsScalingUp
    {
        get { return isScalingUp;}
        set { isScalingUp = value;} 
    }

    public bool IsScalingDown
    {
        get { return isScalingDown;}
        set { isScalingDown = value;} 
    }
}
