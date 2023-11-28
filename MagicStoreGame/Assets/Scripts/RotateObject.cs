using UnityEngine;

public class RotateObject : MonoBehaviour
{
    //Utilizando esse script apenas pra girar a parte interna da Crystal Orb
    public float speed = 50f; 

    void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}