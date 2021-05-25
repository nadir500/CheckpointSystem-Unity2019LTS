using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    private Button _slotButton;
    public ItemData itemData { get; set; }

    private void Start()
    {
        _slotButton.onClick.AddListener(()=> OnClickEvent());
    }

    private void OnClickEvent()
    {
        InventoryManager._instance.RemoveItem(itemData);
    }
}
