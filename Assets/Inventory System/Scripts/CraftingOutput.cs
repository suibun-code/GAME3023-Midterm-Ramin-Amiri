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

    public void RecipeInit()
    {
        waterRecipe = new List<Item>();
        ironBallRecipe = new List<Item>();
        allRecipes = new Dictionary<Item, List<Item>>();

        waterRecipe.Add(masterItemTable.GetItem(7));
        waterRecipe.Add(masterItemTable.GetItem(2));
        waterRecipe.Add(masterItemTable.GetItem(16));
        waterRecipe.Add(masterItemTable.GetItem(16));
        waterRecipe.Add(masterItemTable.GetItem(16));
        waterRecipe.Add(masterItemTable.GetItem(16));
        waterRecipe.Add(masterItemTable.GetItem(16));
        waterRecipe.Add(masterItemTable.GetItem(16));
        waterRecipe.Add(masterItemTable.GetItem(16));

        ironBallRecipe.Add(masterItemTable.GetItem(2));
        ironBallRecipe.Add(masterItemTable.GetItem(3));
        ironBallRecipe.Add(masterItemTable.GetItem(16));
        ironBallRecipe.Add(masterItemTable.GetItem(16));
        ironBallRecipe.Add(masterItemTable.GetItem(16));
        ironBallRecipe.Add(masterItemTable.GetItem(16));
        ironBallRecipe.Add(masterItemTable.GetItem(16));
        ironBallRecipe.Add(masterItemTable.GetItem(16));
        ironBallRecipe.Add(masterItemTable.GetItem(16));

        allRecipes.Add(masterItemTable.GetItem(14), waterRecipe);
        allRecipes.Add(masterItemTable.GetItem(8), ironBallRecipe);
    }

    public void GetOutput(List<ItemSlot> panel)
    {
        foreach (Item recipeItemType in allRecipes.Keys)
        {
            List<Item> recipe = allRecipes[recipeItemType];

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

                if (recipeFound)
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
        for (int i = 0; i < 9; i++)
        {
            if (panel[i].ItemInSlot != recipe[i])
            {

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
