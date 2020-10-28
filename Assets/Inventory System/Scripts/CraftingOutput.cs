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

    [SerializeField]
    private List<Item> potionRecipe;

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
        potionRecipe = new List<Item>();

        potionRecipe.Add(masterItemTable.GetItem(7));
        potionRecipe.Add(masterItemTable.GetItem(2));
        potionRecipe.Add(masterItemTable.GetItem(16));
        potionRecipe.Add(masterItemTable.GetItem(16));
        potionRecipe.Add(masterItemTable.GetItem(16));
        potionRecipe.Add(masterItemTable.GetItem(16));
        potionRecipe.Add(masterItemTable.GetItem(16));
        potionRecipe.Add(masterItemTable.GetItem(16));
        potionRecipe.Add(masterItemTable.GetItem(16));
    }

    public void GetOutput(List<ItemSlot> panel)
    {
        for (int i = 0; i < 9; i++)
        {
            if (panel[i].ItemInSlot != potionRecipe[i])
            {
                Debug.Log("not same");
                return;
            }
        }
        itemSlot.SetContents(masterItemTable.GetItem(14), 1);
        Debug.Log("DONE");
    }
}
