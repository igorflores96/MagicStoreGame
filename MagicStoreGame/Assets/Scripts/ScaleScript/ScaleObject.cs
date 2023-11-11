using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScaleObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSizeUp;
    public float maxSizeDown;
    public float scalePerFrame;
    public bool isScalingUp = false;
    public bool isScalingDown = false;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isScalingUp = !isScalingUp;
            isScalingDown = false;
            if (isScalingUp && transform.localScale.x < maxSizeUp)
            {
                Scale(scalePerFrame);
            }
        }
        if (Input.GetKey(KeyCode.M))
        {
            isScalingDown = !isScalingDown;
            isScalingUp = false;
            if (isScalingDown && transform.localScale.x > maxSizeDown)
            {
                Scale(-scalePerFrame);
            }
        }
    }
    private void Scale(float scalingVal)
    {
        // Calcula a nova escala usando a taxa de escalamento
        Vector3 novaEscala2 = transform.localScale + new Vector3(scalingVal, scalingVal, scalingVal);

        // Garante que a nova escala não ultrapasse o tamanho máximo usando Mathf.Clamp
        novaEscala2.x = Mathf.Clamp(novaEscala2.x, maxSizeDown, maxSizeUp);
        novaEscala2.y = Mathf.Clamp(novaEscala2.y, maxSizeDown, maxSizeUp);
        novaEscala2.z = Mathf.Clamp(novaEscala2.z, maxSizeDown, maxSizeUp);

        // Aplica a nova escala ao objeto
        transform.localScale = novaEscala2;
    }
}
