//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Stacking : MonoBehaviour
//{
//    public Grill grill;
//    public bool isStacked;
//    void OnCollisionEnter(Collision collision)
//    {
//        Debug.Log(this.gameObject.name);
//        Debug.Log(this.grill);
//        Item item = collision.gameObject.GetComponent<Item>();
//        if (collision.gameObject.tag == "FoodRecipe" && item.isStacking == false)
//        {
//            grill.IsStacking(collision.gameObject);
//        }
//    }
//    void Start()
//    {
//        // Atribuir o objeto Grill manualmente no editor do Unity ou programaticamente, se necessário
//        grill = FindObjectOfType<Grill>();
//        if (grill == null)
//        {
//            Debug.LogError("O objeto Grill não foi encontrado.");
//        }
//    }
//    //void OnCollisionExit(Collision collision)
//    //{
//    //   grill.Remove(collision.gameObject);
//    //}
//}
