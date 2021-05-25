using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryManager : MonoBehaviour
{
    private int _columns;
    private int _rows;
    public static Dictionary<int, int> itemDictionary; //id, item count 
    public static InventoryManager _instance;

    public int columns
    {
        get => _columns;
        set => _columns = value;
    }

    public int rows
    {
        get => _rows;
        set => _rows = value;
    }

    private void Awake()
    {
        _instance = this;
        itemDictionary = new Dictionary<int, int>();
        Initialize();
    }


    private void Initialize()
    {
        //TODO: read from binary file 
            
        //create random item data 
        itemDictionary = GenerateRandomItems();
;    }

    private Dictionary<int,int> GenerateRandomItems()
    {
        Dictionary<int, int> dicTemp = new Dictionary<int, int>();
        for (int i = 0; i < 10; i++)
        {
            dicTemp.Add(Random.Range(i,100),Random.Range(1,4));  //id, item count 
        }
        Debug.Log("dic Temp " + dicTemp.Count);
        return dicTemp;
    }
    //add method 
    public void AddItem(ItemData itemData)
    {
        int _currentValue = 0;
        itemDictionary.TryGetValue(itemData.id, out _currentValue); //passing ID of the item 
        if (_currentValue != 0) //already exist in inventory  
        {
            Debug.Log("item exist number = " + _currentValue);
            _currentValue++;
            itemDictionary[itemData.id] = _currentValue;
        }
        else //didn't find the item in inventory 
        {
            itemDictionary.Add(itemData.id, _currentValue);
            //send UI request to update the UI 

            //
        }
    }

    //remove method 
    public void RemoveItem(ItemData itemData)
    {
        int _currentValue = 0;
        if (itemDictionary.TryGetValue(itemData.id, out _currentValue))
        {
            if (_currentValue == 1)
            {
                itemDictionary.Remove(itemData.id);
            }
            else
            {
                _currentValue--;
                itemDictionary[itemData.id] = _currentValue;
            }
        }
        //update UI 
        
        //
    }
 
}