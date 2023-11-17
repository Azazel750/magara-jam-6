using System;
using System.Collections.Generic;
using Karayel;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public static SlotView CurrentSlot => Instance.itemViewList[itemIndex];
    
    public SlotView[] itemViewList = new SlotView[itemCount];
    public static int itemIndex { get; private set; }
    
    private const int itemCount = 4;

    private const float scrollFloatingValue = 0.1f;
    
    public float switchSpeed = 0.5f;
    private float lastSwitchTime;
    private void Start()
    {
        UpdateAllSlots();
    }

    private void Update()
    {
        //çalışmıyor
        
        /*float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Check if there is a scroll input and if enough time has passed since the last switch
        if (scrollInput != 0 && Time.time - lastSwitchTime > switchSpeed)
        {
            // Update the last switch time
            lastSwitchTime = Time.time;

            // Determine the direction of the scroll (positive for up, negative for down)
            int direction = Mathf.RoundToInt(Mathf.Sign(scrollInput));
            
            Switch(itemIndex + scrollInput > 0 ? 1 : -1);
        }*/
        if (Input.GetKeyDown(KeyCode.Alpha1)) Switch(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Switch(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Switch(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) Switch(3);
    }

    public void SwitchNext()
    {
        Switch(itemIndex + 1);
    }

    public void SwitchPrevious()
    {
        Switch(itemIndex - 1);
    }

    public void Switch(int newIndex)
    {
        if (newIndex >= itemViewList.Length) newIndex = 0;
        else if (newIndex < 0) newIndex = itemViewList.Length - 1;
        itemIndex = newIndex;

        UpdateAllSlots();
    }
    
    private void UpdateAllSlots()
    {
        foreach (var slotView in itemViewList)
        {
            slotView.isSelected = false;
        }
        
        itemViewList[itemIndex].isSelected = true;
        
        for (int i = 0; i < itemCount; i++)
        {
            UpdateSlotView(itemViewList[i]);
        }
    }

    private void UpdateSlotView(SlotView itemView)
    {
        itemView.UpdateView();
    }
}