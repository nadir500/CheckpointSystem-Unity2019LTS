using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GeneralSettingsUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _settingsBodyPanel;
    [SerializeField] private ResolutionSettingsUI _resolutionSettingsUI;

    [SerializeField] private Button _saveButton;
    // private SettingsParametersComponent[] _settingsParametersComponents;

    // Start is called before the first frame update
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _saveButton.onClick.AddListener(delegate { WriteGraphicsSettings(_resolutionSettingsUI.graphicsSettings); });
        // Debug.Log(Screen.currentResolution.ToString());
    }

    //read from player prefs
    private void LinkGraphicsSettings(SettingsParametersComponent[] graphicsSettings)
    {
        //read from player prefs and set everything on initialize 
        //load graphics 
       
        //
    }

    //Apply Graphics Settings Here!! 
    public void WriteGraphicsSettings(SettingsParametersComponent[] graphicsSettings)
    {
        string resultString;
        FullScreenMode screenMode;

        for (int i = 0; i < graphicsSettings.Length; i++)
        {
            switch (graphicsSettings[i].componentTag)
            {
                case "_res":
                    string[] result = graphicsSettings[i].parameterText.text.Split('x');
                    int width, height = 0;
                    Debug.Log("result string save " + result[0]);
                    width = int.Parse(result[0]);
                    height = int.Parse(result[1]);
                    Screen.SetResolution(width, height, FullScreenMode.TryParse(graphicsSettings[1].parameterText.text, out screenMode));
                    Debug.Log("result " + result[0]);
                    //save into player prefs
                    break;
                case "_wm":
                    resultString = graphicsSettings[i].parameterText.text;
                    bool IsParsed;

                    IsParsed = FullScreenMode.TryParse(resultString, out screenMode); //platform specific class 
                    if (IsParsed)
                    {
                        Debug.Log("screen mode " + screenMode);
                        Screen.fullScreenMode = screenMode;
                    }
                    break;
                case "_v_sync":
                    int v_syncIndex = Array.IndexOf(graphicsSettings[i].values, graphicsSettings[i].parameterText.text);
                    Debug.Log("index of v sync " + v_syncIndex);
                    QualitySettings.vSyncCount = v_syncIndex;  // 0 disable vsync, 1 enable v_sync 
                    break;
                case "_mquality":  //material Quality 
                    
                    break;
                case "_msaa":
                    //needs to be searched in settings 

                    break;
                case "_texquality":
                    //needs to be searched in settings 

                    break; 
                case "_shadows":
                    //needs to be searched in settings 

                    break;
            }
            PlayerPrefs.SetString(graphicsSettings[i].componentTag,graphicsSettings[i].parameterText.text);
        }
    }
}