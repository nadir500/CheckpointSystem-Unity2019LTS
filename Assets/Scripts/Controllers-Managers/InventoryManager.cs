using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryManager : MonoBehaviour
{
    private int _columns;
    private int _rows;
    public static Dictionary<ItemData, int> itemDictionary; //id, item count 
    public static InventoryManager _instance;

    public int columns
    {
        get => _columns;
       
    }

    public int rows
    {
        get => _rows;
        
    }

    private void Awake()
    {
        _instance = this;
        itemDictionary = new Dictionary<ItemData, int>();
        Initialize();
    }


    private void Initialize()
    {
        //TODO: read from binary file 
            
        //create random item data 
        _columns = 8;
        _rows = 3;
        itemDictionary = GenerateRandomItems();
;    }

    private Dictionary<ItemData,int> GenerateRandomItems()
    {
        Dictionary<ItemData, int> dicTemp = new Dictionary<ItemData, int>();
        for (int i = 0; i < 10; i++)
        {
            dicTemp.Add(new ItemData( Random.Range(i,100),null),Random.Range(1,4));  //id, item count 
        }
        Debug.Log("dic Temp " + dicTemp.Count);
        return dicTemp;
    }

   
    //add method 
    public void AddItem(ItemData itemData)
    {
        int _currentValue = 0;
         //fill static dictionary we have 
         ItemData validItemData = FetchItemData(itemData);
        itemDictionary.TryGetValue(validItemData, out _currentValue); //passing ID of the item 
        if (_currentValue != 0) //already exist in inventory  
        {
            Debug.Log("item exist number = " + _currentValue);
            _currentValue++;
            itemDictionary[validItemData] = _currentValue;
        }
        else //didn't find the item in inventory 
        {
            itemDictionary.Add(validItemData, _currentValue);
            //send UI request to update the UI 

            //
        }
    }
    private ItemData FetchItemData(ItemData itemData)
    {
        foreach (KeyValuePair<ItemData, int> item  in itemDictionary)
        {
            bool IsValid = item.Key.Equals(itemData);
            if (IsValid)
            {
             //return key ref 
             return item.Key;
            }
        }
        return null;
    }
    //remove method 
    public void RemoveItem(ItemData itemData)
    {
        int _currentValue = 0;
        ItemData validItemData = FetchItemData(itemData);
        if (itemDictionary.TryGetValue(validItemData, out _currentValue))
        {
            if (_currentValue == 1)
            {
                itemDictionary.Remove(validItemData);
            }
            else
            {
                _currentValue--;
                itemDictionary[validItemData] = _currentValue;
            }
        }
        //update UI 
        
        //
    }
 
}