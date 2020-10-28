using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemSlot : EventTrigger
{
    // Event callbacks
    public UnityEvent<Item> onItemUse;

    // flag to tell ItemSlot it needs to update itself after being changed
    private bool b_needsUpdate = true;

    // Declared with auto-property
    public Item ItemInSlot { get; private set; }
    public int ItemCount { get; private set; }

    [Tooltip("The crafting table to use.")]
    [SerializeField]
    public GameObject craftingPanel;

    // scene references
    [SerializeField]
    private TMPro.TextMeshProUGUI itemCountText;

    [SerializeField]
    private Image itemIcon;

    private ItemSlot targetSlot;

    protected RectTransform rectTransform = null;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (b_needsUpdate)
        {
            UpdateSlot();
        }
    }

    /// <summary>
    /// Returns true if there is an item in the slot
    /// </summary>
    /// <returns></returns>
    public bool HasItem()
    {
        return ItemInSlot != null;
    }

    /// <summary>
    /// Removes everything in the item slot
    /// </summary>
    /// <returns></returns>
    public void ClearSlot()
    {
        ItemInSlot = null;
        b_needsUpdate = true;
    }

    /// <summary>
    /// Attempts to remove a number of items. Returns number removed
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public int TryRemoveItems(int count)
    {
        if (count > ItemCount)
        {
            int numRemoved = ItemCount;
            ItemCount -= numRemoved;
            b_needsUpdate = true;
            return numRemoved;
        }
        else
        {
            ItemCount -= count;
            b_needsUpdate = true;
            return count;
        }
    }

    /// <summary>
    /// Sets what is contained in this slot
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void SetContents(Item item, int count)
    {
        ItemInSlot = item;
        ItemCount = count;
        b_needsUpdate = true;
    }

    /// <summary>
    /// Activate the item currently held in the slot
    /// </summary>
    public void UseItem()
    {
        // if(ItemInSlot != null)
        // {
        //     if(ItemCount >= 1)
        //     {
        //         ItemInSlot.Use();
        //         onItemUse.Invoke(ItemInSlot);
        //         ItemCount--;
        //         b_needsUpdate = true;
        //     }
        // }
    }

    /// <summary>
    /// Update visuals of slot to match items contained
    /// </summary>
    private void UpdateSlot()
    {
        if (ItemCount == 0)
        {
            ItemInSlot = null;
        }

        if (ItemInSlot != null)
        {
            itemCountText.text = ItemCount.ToString();
            itemIcon.sprite = ItemInSlot.Icon;
            itemIcon.gameObject.SetActive(true);
        }
        else
        {
            itemIcon.gameObject.SetActive(false);
        }

        b_needsUpdate = false;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        itemIcon.GetComponent<Canvas>().sortingOrder = 2;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        itemIcon.transform.position += (Vector3)eventData.delta;

        foreach (ItemSlot slot in Crafting.Instance.itemSlots)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(slot.rectTransform, Input.mousePosition))
            {
                //if the mouse is within the space of a valid space, move to it then break out of the foreach loop.
                targetSlot = slot;
                break;
            }

            //if the mouse ISN'T within a valid space, do nothing.
            targetSlot = null;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        itemIcon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        itemIcon.GetComponent<Canvas>().sortingOrder = 1;

        if (targetSlot) //if no current space, set back to original space.
        {
            Move();
        }
    }

    public void Move()
    {
        if (targetSlot.HasItem() && targetSlot.ItemInSlot != ItemInSlot)
        {   
            return;
        }

        if (targetSlot.ItemInSlot = ItemInSlot)
        {
            targetSlot.ItemCount += ItemCount;
            this.ItemInSlot = null;
            this.ItemCount = 0;

            this.b_needsUpdate = true;
            targetSlot.b_needsUpdate = true;
        }
        else
        {
            targetSlot.ItemInSlot = ItemInSlot;
            targetSlot.ItemCount = ItemCount;
            this.ItemInSlot = null;
            this.ItemCount = 0;

            this.b_needsUpdate = true;
            targetSlot.b_needsUpdate = true;
        }

        targetSlot = null;
    }
}
