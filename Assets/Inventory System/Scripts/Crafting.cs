using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Assertions;
using System.Linq; // For finding all gameObjects with name

public class Crafting : MonoBehaviour
{
    [Tooltip("The object which will hold Item Slots as its direct children")]
    [SerializeField]
    private GameObject craftingPanel;

    [Tooltip("List size determines how many slots there will be. Contents will replaced by copies of the first element")]
    [SerializeField]
    public List<ItemSlot> itemSlots;

    public static Crafting _instance;
    public static Crafting Instance { get { return _instance; } }

    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        InitItemSlots();
    }

    //Initialize the item slots and make sure that there is a prefab for itemSlots. Then instantiate all of the item slots.
    private void InitItemSlots()
    {
        Assert.IsTrue(itemSlots.Count > 0, "itemSlots was empty");
        Assert.IsNotNull(itemSlots[0], "Crafting is missing a prefab for itemSlots. Add it as the first element of its itemSlot list");

        // init item slots
        for (int i = 1; i < itemSlots.Count; i++)
        {
            GameObject newObject = Instantiate(itemSlots[0].gameObject, craftingPanel.transform);
            ItemSlot newSlot = newObject.GetComponent<ItemSlot>();
            itemSlots[i] = newSlot;
        }
    }
}
