using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    //[SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private GameObject _inventoryItemsBody;
    private Button[] _itemsButtons;
    private Dictionary<int, int> _itemDictionaryUI;
    
    private void Start()
    {
        _itemDictionaryUI = InventoryManager.itemDictionary; //pass by ref 
    }

    private void Initialize()
    {
        // InitializeButtonsEvents();
    }

    private void Draw()
    {
        
    }
}
