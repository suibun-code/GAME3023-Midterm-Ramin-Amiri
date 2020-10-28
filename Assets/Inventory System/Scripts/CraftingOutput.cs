using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CraftingOutput : MonoBehaviour
{
    [Tooltip("Reference to the master item table")]
    [SerializeField]
    private ItemTable masterItemTable;

    public ItemSlot itemSlot;

    private Dictionary<Item, List<Item>> allRecipes;

    [SerializeField]
    private List<Item> waterRecipe;
    private List<Item> ironBallRecipe;
    private List<Item> yellowPotionRecipe;
    private List<Item> redPotionRecipe;
    private List<Item> grapesRecipe;

    // Start is called before the first frame update
    void Start()
    {
        RecipeInit();
    }

    // Update is called once per frame
    void Update()
    {
        GetOutput(Crafting.Instance.itemSlots);
    }

    //Initialize all recipes and put it into a dictionary containing the item to be outputted and the items needed to be in the crafting panel.
    public void RecipeInit()
    {   
        //The recipes.
        waterRecipe = new List<Item>();
        ironBallRecipe = new List<Item>();
        yellowPotionRecipe = new List<Item>();
        redPotionRecipe = new List<Item>();
        grapesRecipe = new List<Item>();

        //The dictionary containing the recipes.
        allRecipes = new Dictionary<Item, List<Item>>();
        
        //***ADD THE RECIPE REQUIREMENTS
        waterRecipe.Add(masterItemTable.GetItem(7));
        waterRecipe.Add(masterItemTable.GetItem(2));
        waterRecipe.Add(masterItemTable.GetItem(16));
        waterRecipe.Add(masterItemTable.GetItem(16));
        waterRecipe.Add(masterItemTable.GetItem(16));
        waterRecipe.Add(masterItemTable.GetItem(16));
        waterRecipe.Add(masterItemTable.GetItem(16));
        waterRecipe.Add(masterItemTable.GetItem(16));
        waterRecipe.Add(masterItemTable.GetItem(16));

        ironBallRecipe.Add(masterItemTable.GetItem(16));
        ironBallRecipe.Add(masterItemTable.GetItem(7));
        ironBallRecipe.Add(masterItemTable.GetItem(16));
        ironBallRecipe.Add(masterItemTable.GetItem(16));
        ironBallRecipe.Add(masterItemTable.GetItem(14));
        ironBallRecipe.Add(masterItemTable.GetItem(16));
        ironBallRecipe.Add(masterItemTable.GetItem(16));
        ironBallRecipe.Add(masterItemTable.GetItem(16));
        ironBallRecipe.Add(masterItemTable.GetItem(16));

        yellowPotionRecipe.Add(masterItemTable.GetItem(16));
        yellowPotionRecipe.Add(masterItemTable.GetItem(7));
        yellowPotionRecipe.Add(masterItemTable.GetItem(16));
        yellowPotionRecipe.Add(masterItemTable.GetItem(3));
        yellowPotionRecipe.Add(masterItemTable.GetItem(14));
        yellowPotionRecipe.Add(masterItemTable.GetItem(16));
        yellowPotionRecipe.Add(masterItemTable.GetItem(16));
        yellowPotionRecipe.Add(masterItemTable.GetItem(16));
        yellowPotionRecipe.Add(masterItemTable.GetItem(8));

        redPotionRecipe.Add(masterItemTable.GetItem(16));
        redPotionRecipe.Add(masterItemTable.GetItem(14));
        redPotionRecipe.Add(masterItemTable.GetItem(16));
        redPotionRecipe.Add(masterItemTable.GetItem(16));
        redPotionRecipe.Add(masterItemTable.GetItem(3));
        redPotionRecipe.Add(masterItemTable.GetItem(16));
        redPotionRecipe.Add(masterItemTable.GetItem(16));
        redPotionRecipe.Add(masterItemTable.GetItem(2));
        redPotionRecipe.Add(masterItemTable.GetItem(16));

        grapesRecipe.Add(masterItemTable.GetItem(3));
        grapesRecipe.Add(masterItemTable.GetItem(2));
        grapesRecipe.Add(masterItemTable.GetItem(16));
        grapesRecipe.Add(masterItemTable.GetItem(16));
        grapesRecipe.Add(masterItemTable.GetItem(16));
        grapesRecipe.Add(masterItemTable.GetItem(16));
        grapesRecipe.Add(masterItemTable.GetItem(16));
        grapesRecipe.Add(masterItemTable.GetItem(16));
        grapesRecipe.Add(masterItemTable.GetItem(16));

        //Add the recipe to the dictionary and define what the output item will be
        allRecipes.Add(masterItemTable.GetItem(14), waterRecipe);
        allRecipes.Add(masterItemTable.GetItem(8), ironBallRecipe);
        allRecipes.Add(masterItemTable.GetItem(15), yellowPotionRecipe);
        allRecipes.Add(masterItemTable.GetItem(13), redPotionRecipe);
        allRecipes.Add(masterItemTable.GetItem(5), grapesRecipe);
    }

    public void GetOutput(List<ItemSlot> panel)
    {
        //For each loop through the allRecipes dictionary for each key (recipe) then output the item into the output box and consume the items for the recipe.
        foreach (Item recipeItemType in allRecipes.Keys)
        {
            List<Item> recipe = allRecipes[recipeItemType]; //the current recipe to test and loop through.

            bool recipeFound = true;

            if (itemSlot.ItemInSlot == masterItemTable.GetItem(16))
            {
                for (int i = 0; i < 9; i++)
                {
                    if (panel[i].ItemInSlot != recipe[i])
                    {
                        Debug.Log("not same");
                        recipeFound = false;
                    }
                }

                if (recipeFound) //what to do if the recipe is found. call the consume function and then set the output box contents to the item that is outputted.
                {
                    ConsumeItems(panel, recipe);
                    itemSlot.SetContents(recipeItemType, 1);
                    Debug.Log("DONE");
                }
            }
        }
    }

    public void ConsumeItems(List<ItemSlot> panel, List<Item> recipe)
    {
        //loop through the items and consume if they are the same.
        for (int i = 0; i < 9; i++)
        {
            if (panel[i].ItemInSlot != recipe[i])
            {
                Debug.Log("Not the item.");
            }
            else
            {
                panel[i].SetItemCount(panel[i].ItemCount - 1);
                panel[i].b_needsUpdate = true;
                Debug.Log("removed 1");
            }
        }
    }
}
