using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private static  CheckpointController _instance; 
    [SerializeField] private static Dictionary<string, GameObject> _checkPointIds;
    [SerializeField] private GameObject _checkpointParent;
    private List<ICheckpointHandler> _checkpointsObservers;
    
    void Awake()
    {
        _checkpointsObservers = new List<ICheckpointHandler>();
        _checkPointIds = new Dictionary<string, GameObject>();
        _instance = this; 
        int _childCount = _checkpointParent.transform.childCount;
        string _childID = "";
        GameObject _checkpointTemp;
        for (int i = 0; i < _childCount; i++)
        {
            _checkpointTemp = _checkpointParent.transform.GetChild(i).gameObject;
            _childID = _checkpointTemp.GetComponent<CheckpointDataModel>().objID;
            _checkPointIds.Add(_childID, _checkpointTemp);
        }

        Debug.Log(_checkPointIds.Count);
         GetCheckPointID("08f2a7f4-dd1a-4720-b478-076e88081739");  //for testing 
    }

    public void GetCheckPointID(string ID)
    {
        GameObject tempo = _checkPointIds.FirstOrDefault(x => x.Key == ID).Value; //Linq assist
        Debug.Log(tempo.name);
        //----------------
        string _fromPlayerPrefs = PlayerPrefs.GetString("CPID");
        if (!string.IsNullOrEmpty(_fromPlayerPrefs))
        {
            Debug.Log("stored check point " + _fromPlayerPrefs );
        }
        //-------------------
    }

    public void SetLastCheckPointID(string ID)
    {
        // JsonCheckpoint checkpointJson = new JsonCheckpoint(); 
        // TextAsset _jsonText = Resources.Load("Json/checkpointJson") as TextAsset;
        // checkpointJson.checkpointID = ID;
        // string jsonSavePath = Application.persistentDataPath + "/Resources/Json/checkpointJson.json";
        // string jsonOutput = JsonUtility.ToJson(checkpointJson);
        // File.WriteAllText(jsonSavePath,jsonOutput);
        PlayerPrefs.SetString("CPID", ID);
        Debug.Log("recorded  in player prefs ");
    }

    #region Observer Pattern for Checkpoints

    public void AddObserver(ICheckpointHandler checkpointObserver)
    {
        _checkpointsObservers.Add(checkpointObserver);
    }

    public void RemoveObserver(ICheckpointHandler checkpointObserver)
    {
        _checkpointsObservers.Remove(checkpointObserver);
    }

    public void NotifyObserver(ICheckpointHandler item)
    {
        item.OnCheckpointChangeID();
    }

    #endregion
}
