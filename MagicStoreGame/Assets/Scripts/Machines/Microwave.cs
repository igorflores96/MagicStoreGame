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
    private int countItem = 0;
    public GameObject spawnPoint;
    private bool poopSet = true;
    public float timerSeconds;
    private List<GameObject> collidedObjects = new List<GameObject>();

    public void AddItem(Item RecipeItem,bool isUpgraded, List<GameObject> collidedObjects)
    {
        this.collidedObjects = collidedObjects;
        if (!isUpgraded) 
        {
            switch (this.countItem)
            {
                case 0:
                    RecipeItem1 = RecipeItem;
                    this.countItem++;
                    break;
                case 1:
                    RecipeItem2 = RecipeItem;
                    this.countItem = 0;
                    break;
            }
        }
        else if (isUpgraded) 
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
                    break;
                case 2:
                    RecipeItem3 = RecipeItem;
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
            if (RecipeItem3 == null && recipe.RecipeItems.Count == 2)
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
           else if( RecipeItem3 != null)
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
    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(timerSeconds);

        FusionItens(this.RecipeItem1, this.RecipeItem2, this.RecipeItem3);
    }
    public void RemoveItemTrigger(GameObject obj)
    {
        if (this.countItem > 0)
        {
            this.countItem--;
        }
        for (int i = 0; i < collidedObjects.Count; i++)
        {
            if (collidedObjects[i] == obj)
            {
                collidedObjects.RemoveAt(i);
            }
        }
    }
}
