using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubWitch : MonoBehaviour
{
    public GameObject PoopObject;
    private int countItem = 0;
    public GameObject spawnPoint;
    private bool notTottaly = true;
    public bool isUpgraded;
    public float timerSeconds;
    private List<GameObject> collidedObjects = new List<GameObject>();
    public List<Recipe> RecipeLists;
    private Recipe recipe;
    public Recipe recipeAddtional1;
    public Recipe recipeAddtional2;
    public Animator animator;
    private Item itemValue;
    public bool isVerify;
    private void Awake()
    {
        recipe = new Recipe();
        recipe.RecipeItems = new List<Item> { new Item() };
    }
    public void AddItem(Item RecipeItem,List<GameObject> collidedObjects)
    {
        this.collidedObjects = collidedObjects;
        if (this.countItem < 6)
        {
            if (countItem + 1 == recipe.RecipeItems.Count)
            {
                recipe.RecipeItems.Insert(countItem, RecipeItem);
            }
            else if(countItem == 0 && recipe.RecipeItems.Count == 1)
            {
                recipe.RecipeItems.Insert(0,RecipeItem);
            }
            this.countItem++;
            Debug.Log("SubWitchCount" + this.countItem);
        }
        else if (this.countItem == 6)
        {
            this.countItem = 0;
        }
    }
    public IEnumerator Timer()
    {
        animator.SetTrigger("DoorUp");
        yield return new WaitForSeconds(timerSeconds);
        VerificarReceita();
    }
    private bool VerificarReceita()
    {
        bool result = true;
        if (isUpgraded)
        {
            RecipeLists.Add(recipeAddtional1);
            RecipeLists.Add(recipeAddtional2);
        }
        Recipe recipeLast = RecipeLists.Last();
            foreach (Recipe rec in RecipeLists)
            {
                
                var currentcount = recipe.RecipeItems.Count - 1;
                if (rec.RecipeItems.Count != currentcount && rec.Equals(recipeLast))
                {
                    Instantiate(PoopObject.gameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    DeleteCollidedObjects();
                    
                    result = false;
                }
                else
                {
                    result = VerificarItems(rec);
                    if (result)
                    {
                        this.notTottaly = false;
                        Instantiate(rec.RecipeItemPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                        this.itemValue = rec.RecipeItemPrefab.GetComponent<Item>();
                        itemValue.ChangeValueItem(2);
                        DeleteCollidedObjects();
                    }
                    else if (this.notTottaly && rec.Equals(recipeLast))
                    {
                        Instantiate(rec.RecipeItemPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                        DeleteCollidedObjects();
                        this.itemValue = rec.RecipeItemPrefab.GetComponent<Item>();
                        itemValue.ChangeValueItem(1);
                        Debug.Log("item nao totalmente correto");
                    }
                }
            }
        isVerify = false;
        animator.SetTrigger("DoorDown");
        return result;
        
    }
    private bool VerificarItems(Recipe rec)
    {
        for (int i = 0; i < recipe.RecipeItems.Count -1; i++)
        {
            if (rec.RecipeItems[i].NameItem != recipe.RecipeItems[i].NameItem)
            {
                return false;
            }
        }
        return true;
    }
    private void DeleteCollidedObjects()
    {
        foreach (GameObject obj in collidedObjects)
        {
            Destroy(obj);
        }
        collidedObjects.Clear();
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
                recipe.RecipeItems.RemoveAt(i);
            }
        }
    }
    public void IsVerifying()
    {
        if (!isVerify)
        {
            StartCoroutine(Timer());
            isVerify = true;
        }
    }
}
