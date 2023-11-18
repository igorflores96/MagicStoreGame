using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Microwave : MonoBehaviour
{
    public List<Recipe> RecipeLists;
    private Item RecipeItem1;
    private Item RecipeItem2;
    private Item RecipeItem3;
    public GameObject PoopObject;  
    public bool isUpgraded;
    private int countItem = 0;
    private List<GameObject> collidedObjects = new List<GameObject>();
    public GameObject spawnPoint;
    private bool poopSet = true;
    public float timerSeconds;

    void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;

        // Adiciona o objeto à lista se ainda não estiver presente
        if (!collidedObjects.Contains(collidedObject))
        {
            collidedObjects.Add(collidedObject);
            Debug.Log(collidedObjects);
        }
        Item item = collision.gameObject.GetComponent<Item>();
        AddItem(item, countItem, isUpgraded);
    }
    void OnCollisionExit(Collision collision)
    {
        countItem--;
    }
    public void AddItem(Item RecipeItem, int countItem,bool isUpgraded)
    {
        if(!isUpgraded) 
        {
            switch (this.countItem)
            {
                case 0:
                    RecipeItem1 = RecipeItem;
                    this.countItem++;
                    break;
                case 1:
                    RecipeItem2 = RecipeItem;
                    this.countItem++;
                    StartCoroutine(Timer(RecipeItem1, RecipeItem2,null));
                    this.countItem = 0;
                    break;
            }
        }
        else
        {
            switch (countItem)
            {
                case 0:
                    RecipeItem1 = RecipeItem;
                    this.countItem++;
                    break;
                case 1:
                    RecipeItem2 = RecipeItem;
                    this.countItem++;
                    break;
                case 2:
                    RecipeItem3 = RecipeItem;
                    this.countItem++;
                    break;
                case 3:
                    this.countItem++;
                    StartCoroutine(Timer(RecipeItem1,RecipeItem2,RecipeItem3));
                    this.countItem = 0;
                    break;
            }
        }
    }
    public void FusionItens(Item RecipeItem1, Item RecipeItem2, Item RecipeItem3)
    {
        
        Recipe recipeLast = RecipeLists.Last();

        foreach (Recipe recipe in RecipeLists)
        {
            if (RecipeItem3 == null)
           {
                if(ContainsItem(recipe,RecipeItem1) == true && ContainsItem(recipe, RecipeItem2) == true)
                {

                    poopSet = false;
                    Instantiate(recipe.RecipeItemPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    DeleteCollidedObjects();
                    
                }
                else if(poopSet && recipe.Equals(recipeLast))
                {
                    
                    Instantiate(PoopObject.gameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    DeleteCollidedObjects();
                }
           }
           else
           {
                if (ContainsItem(recipe, RecipeItem1) == true && ContainsItem(recipe, RecipeItem2) == true && ContainsItem(recipe, RecipeItem3) == true)
                {
                    poopSet = false;
                    Instantiate(recipe.RecipeItemPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    DeleteCollidedObjects();
                }
                else if (poopSet && recipe.Equals(recipeLast))
                {
                    Instantiate(PoopObject.gameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    DeleteCollidedObjects();
                }

            }
        }
    }
    public bool ContainsItem(Recipe recipe, Item RecipeItem)
    {
        try
        {
            return recipe.RecipeItems.All(x => recipe.RecipeItems.Any(y => y.name == RecipeItem.name));
        }
        catch (Exception)
        {
            return false;
        }
    }
    private void DeleteCollidedObjects()
    {
        foreach (GameObject obj in collidedObjects)
        {
            Destroy(obj);
        }
        collidedObjects.Clear();
    }
    IEnumerator Timer(Item RecipeItem1, Item RecipeItem2, Item RecipeItem3)
    {
        yield return new WaitForSeconds(timerSeconds);

        FusionItens(RecipeItem1, RecipeItem2, RecipeItem3);
    }
}
