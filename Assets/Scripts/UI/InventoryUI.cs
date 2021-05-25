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
    private Image[] _itemImages;
    private Text[] _itemTexts;
    private Dictionary<ItemData, int> _itemDictionaryUI;
    
    private void Start()
    {
        _itemDictionaryUI = InventoryManager.itemDictionary; //pass by ref \
        Initialize();
    }

    private void Initialize()
    {
        // InitializeButtonsEvents();
        Draw();
    }

    private void Draw()  //fetch from inventory manager 
    {
        //_itemTexts = new Text[InventoryManager._instance.columns * InventoryManager._instance.rows]; 
        _itemTexts = _inventoryItemsBody.GetComponentsInChildren<Text>();
        
        Debug.Log("_temText " + _itemTexts.Length );
        // for (int i = 0; i < _itemDictionaryUI.Count; i++)
        // {
        //     
        // }
    }

    private void OnClickEvent()
    {
        
    }
}
