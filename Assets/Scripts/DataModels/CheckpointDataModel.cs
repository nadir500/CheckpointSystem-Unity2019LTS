using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CheckpointDataModel : MonoBehaviour ,ISerializationCallbackReceiver , ICheckpointHandler
{
    [SerializeField] private string _objID =null;
    [SerializeField] private CheckpointController _checkpointController;
 
    public string checkpointID;
    private void Start()
    {
        _checkpointController.AddObserver(this);
    }

    public string objID
    {
        get { return _objID; }
    }

    //
    public void OnAfterDeserialize()
    {
        if (_objID ==null)
        {
            RegisterIDValue();
        }
    }

    public void OnBeforeSerialize()
    {
        if (_objID ==null)
        {
            RegisterIDValue();
        }
    }

    private void RegisterIDValue()
    {
        _objID = Guid.NewGuid().ToString(); 
;    }

    private void UnregisterID()
    {
        _objID = null; 
    }
    private void OnDestroy()
    {
        UnregisterID();
    }

    public void OnCheckpointChangeID()
    {
        _checkpointController.GetCheckPointID(objID);
    }

    private void OnTriggerEnter(Collider other)
    {
         _checkpointController.NotifyObserver(this);
    }
}

[Serializable] //for json 
public class JsonCheckpoint
{
    public string checkpointID ="";
}
