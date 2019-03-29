using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        Text displayText = transform.Find("Text").GetComponent<Text>();
        Image displayImage = transform.Find("Image").GetComponent<Image>();

        if (item)
        {
            displayText.text = item.itemName;
            displayImage.sprite = item.icon;
            displayImage.color = Color.white;
        }
        else
        {
            displayText.text = " ";
            displayImage.sprite = null;
            displayImage.color = Color.clear;
        }
    }
    // Use this for initialization


    // Use this for initialization
    public void Use ()
    {
        if (item)
        {
            Debug.Log("You Clicked: " + item.itemName);
        }
	}
	
	
}
