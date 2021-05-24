using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class SaveDataManager : MonoBehaviour
{
    public Text text;

    private void Awake()
    {
        Save();
        Load();
    }

    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.player = "pain";
        string _saveFilePath = Application.persistentDataPath + "/savedata.data"; //for SaveData class
        WriteToBinary(saveData, _saveFilePath);
    }

    public void Load()
    {
        string _saveFilePath = Application.persistentDataPath + "/savedata.data"; //for SaveData class
        ReadFromBinaryPlayerData(_saveFilePath);
    }

    #region Read/Write Binary Files

    private void WriteToBinary(SaveData saveDataModel, string saveFilePath)
    {
        Debug.Log(saveFilePath);
        WriteToBinaryFile(saveFilePath, saveDataModel);
    }

    private void ReadFromBinaryPlayerData(string saveFilePath)
    {
        SaveData playerData = ReadFromBinaryFile<SaveData>(saveFilePath);
        // Debug.Log(playerData.ToString());
        text.text = playerData.ToString();
    }

    #endregion

    #region generic types functions

    public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
    {
        using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
        {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, objectToWrite);
        }
    }

    public static T ReadFromBinaryFile<T>(string filePath)
    {
        using (Stream stream = File.Open(filePath, FileMode.Open))
        {
            var binaryFormatter = new BinaryFormatter();
            return (T) binaryFormatter.Deserialize(stream);
        }
    }

    #endregion
}