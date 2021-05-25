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
    private ItemData[] _itemDataArray;
    private Dictionary<ItemData, int> _itemDictionaryUI;

    private void Start()
    {
        _itemDictionaryUI = InventoryManager.itemDictionary; //pass by ref \
        Initialize();
    }

    private void Initialize()
    {
        // InitializeButtonsEvents();
        //from UI parent 
        _itemTexts = _inventoryItemsBody.GetComponentsInChildren<Text>();
        _itemsButtons = _inventoryItemsBody.GetComponentsInChildren<Button>();
        //
        Draw();
    }

    private void Draw() //fetch from inventory manager 
    {
        //_itemTexts = new Text[InventoryManager._instance.columns * InventoryManager._instance.rows]; 
        List<ItemData> itemDataList = new List<ItemData>();
        List<int> textsList = new List<int>();
        _itemDataArray = new ItemData[_itemDictionaryUI.Count];
        foreach (KeyValuePair<ItemData, int> item in _itemDictionaryUI)
        {
            itemDataList.Add(item.Key);
            textsList.Add(item.Value);
        }

        for (int i = 0; i < _itemTexts.Length; i++)
        {
            _itemTexts[i].text = "";
        }
        for (int i = 0; i < textsList.Count; i++)
        {
            // _itemTexts[i].text = textsList[i].ToString();
            ItemCountText(textsList[i],i);
        }

        _itemDataArray = itemDataList.ToArray(); //copy from dictionary 
        for (int i = 0; i < _itemsButtons.Length; i++)
        {
            _itemsButtons[i].onClick.RemoveAllListeners();
        }
        for (int i = 0; i < _itemDataArray.Length; i++)
        {
            int g = i;
            Debug.Log("_itemDataArray " + _itemDataArray.Length);
             // Debug.Log(_itemsButtons[i]);
            // Debug.Log(_itemDataArray[i].id);
            // Debug.Log("bound array " + _itemDictionaryUI.Count);
            _itemsButtons[i].onClick.AddListener(delegate { OnClickEvent(_itemDataArray[g]); });
            // _itemsButtons[i].onClick.AddListener(delegate { countShit(g); });

        }

        // Debug.Log("_temText " + _itemTexts.Length);
        // Debug.Log("_itemDataArray " + _itemDataArray.Length);
    }

    private void countShit(int index )
    {
        int g = index; 
        Debug.Log("index " + g);
    }
    private void OnClickEvent(ItemData itemData)
    {
        InventoryManager._instance.RemoveItem(itemData);
         Draw();
    }

    private void ItemCountText(int itemCount , int index )
    {
         
            if (itemCount <= 1)
            {
                _itemTexts[index].text = "";
            }
            else
            {
                _itemTexts[index].text = itemCount.ToString();
            }
        
    }
}