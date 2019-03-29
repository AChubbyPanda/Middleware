using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> list = new List<Item>();

    public GameObject Player;
    public GameObject InventoryPanel;

    void UpdatePanelSlots()
    {

        InventorySlotController[] slots = InventoryPanel.GetComponentsInChildren<InventorySlotController>();
        int index = 0;
        foreach (var slot in slots)
        {
            if (index < list.Count)
            {
                slot.item = list[index];
            }
            else
            {
                slot.item = null;
            }

            slot.UpdateInfo();
            index++;
        }
    }

    // Use this for initialization
    void Start()
    {
        UpdatePanelSlots();
    }

    public void Add(Item item)
    {
        if (list.Count < 6)
        {
            list.Add(item);
        }
        UpdatePanelSlots();
    }

    public void Remove(Item item)
    {
        list.Remove(item);
        UpdatePanelSlots();
    }

    // Update is called once per frame
    void Update ()
{
		
	}
}
