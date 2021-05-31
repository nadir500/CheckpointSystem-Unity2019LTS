using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GeneralSettingsUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _settingsBodyPanel;

    private SettingsParametersComponent[] _settingsParametersComponents;

    // Start is called before the first frame update
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        // //get all components 
        // int childCount = _settingsBodyPanel.transform.GetComponentsInChildren<SettingsParametersComponent>().Length;
        // _settingsParametersComponents = new SettingsParametersComponent[childCount];
        // _settingsParametersComponents =
        //     _settingsBodyPanel.transform.GetComponentsInChildren<SettingsParametersComponent>();
        // Debug.Log("_settingsParametersComponents " + _settingsParametersComponents.Length);
        //assign events 
        
    }

    private void LinkEvents()
    {
    
    }
    
    private void SaveGraphicsChanges(Text[] graphicsSettings) 
    {
        for (int i = 0; i < graphicsSettings.Length; i++)
        {
            switch (graphicsSettings[i].text)
            {
                case "_res":
                    
                    break; 
                case  "_wm":
                    break;
                case  "_v_sync":
                    break;
                case  "_mquality":
                    break;
                case  "_msaa":
                    break;
                case "_texquality":
                    break;
                case "_shadows" :
                    break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}