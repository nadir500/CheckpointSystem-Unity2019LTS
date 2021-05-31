using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.HighDefinition;

public class ResolutionSettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject _resolutionSettingsBodyPanel;
    [SerializeField] private HDRenderPipelineAsset _hdRenderPipelineAsset;
    [SerializeField] private GeneralSettingsUIManager _generalSettingsUIManager;
    private SettingsParametersComponent[] _settingsParametersComponents;
    private Text[] _parametersValues;

    public SettingsParametersComponent[] graphicsSettings
    {
        get { return _settingsParametersComponents; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.DeleteAll();
        Initialize();
        SetGraphicsSettings(); //on UI to be assigned to each choice [Text], fill the UI
        //read settings and assign index to each choice 

        LinkGraphicsSettings(graphicsSettings);
        StartCoroutine(ApplyGraphicsSettings());
    }

    IEnumerator ApplyGraphicsSettings()
    {
        yield return new WaitForSeconds(0.0f);
        _generalSettingsUIManager.WriteGraphicsSettings(graphicsSettings);

    }

    private void Initialize()
    {
        // //get all components 
        int childCount = _resolutionSettingsBodyPanel.transform.GetComponentsInChildren<SettingsParametersComponent>()
            .Length;
        _settingsParametersComponents = new SettingsParametersComponent[childCount];
        _parametersValues = new Text[childCount];

        _settingsParametersComponents =
            _resolutionSettingsBodyPanel.transform.GetComponentsInChildren<SettingsParametersComponent>();
        Debug.Log("_settingsParametersComponents " + _settingsParametersComponents.Length);
        //assign events 
        for (int i = 0; i < _parametersValues.Length; i++)
        {
            _parametersValues[i] = _settingsParametersComponents[i].parameterText; // pass as reference 
        }
    }

    private void LinkGraphicsSettings(SettingsParametersComponent[] graphicsSettings)
    {
        //read from player prefs and set everything on initialize 
        //load graphics 
        string outputPlayerPrefs = "";
        for (int i = 0; i < graphicsSettings.Length; i++)
        {
            outputPlayerPrefs =
                PlayerPrefs.GetString(graphicsSettings[i].componentTag);
            Debug.Log("outputPlayerPrefs " + outputPlayerPrefs);
            if (string.IsNullOrEmpty(outputPlayerPrefs))
            {
                //make initial values 
                Debug.Log(
                    "string.IsNullOrEmpty(outputPlayerPrefs) && !PlayerPrefs.HasKey(graphicsSettings[i].componentTag))");
                SetDefaultPlayerPrefs(graphicsSettings[i]);
            }

            if (PlayerPrefs.HasKey(graphicsSettings[i].componentTag))
            {
                outputPlayerPrefs =
                    PlayerPrefs.GetString(graphicsSettings[i].componentTag);
                //set the index for each setting 
                LinkPlayerPrefsToGraphicsSettings(outputPlayerPrefs, graphicsSettings[i]);
            }
            
        }
        
    }

    private void LinkPlayerPrefsToGraphicsSettings(string outputPlayerPrefs,
        SettingsParametersComponent graphicsSetting)
    {
        int index = Array.IndexOf(graphicsSetting.values, outputPlayerPrefs);
        Debug.Log("LinkPlayerPrefsToGraphicsSettings index " + index);
        graphicsSetting.index = index;
       
    }

    private void SetDefaultPlayerPrefs(SettingsParametersComponent graphicsSetting) //set strings to player prefs 
    {
        StringBuilder stringBuilder = new StringBuilder();
        switch (graphicsSetting.componentTag)
        {
            case "_res":
                stringBuilder.Append(Screen.currentResolution.width + "x" + Screen.currentResolution.height);
                PlayerPrefs.SetString(graphicsSetting.componentTag, stringBuilder.ToString());
                // Screen.SetResolution(1280, 720, FullScreenMode.ExclusiveFullScreen);
                stringBuilder.Clear();
                break;
            case "_wm":
                stringBuilder.Append(FullScreenMode.ExclusiveFullScreen.ToString());
                // Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                PlayerPrefs.SetString(graphicsSetting.componentTag, stringBuilder.ToString());
                stringBuilder.Clear();
                break;
            case "_v_sync":
                // QualitySettings.vSyncCount = 1;
                PlayerPrefs.SetString(graphicsSetting.componentTag, "ON");
                break;
            case "_mquality":

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
    }


    //fill  values on UI [nothing more]
    private void SetGraphicsSettings()
    {
        StringBuilder stringBuilder = new StringBuilder();
        FullScreenMode[] fullScreenOptions =
        {
            FullScreenMode.ExclusiveFullScreen,
            FullScreenMode
                .FullScreenWindow, //FullScreenWindow = BorderlessFullscreen. MaximizedWindow is a MacOS-only thing.
            FullScreenMode.Windowed
        };
        for (int i = 0; i < _settingsParametersComponents.Length; i++)
        {
            switch (_settingsParametersComponents[i].componentTag)
            {
                case "_res":
                    _settingsParametersComponents[i].values = new string[Screen.resolutions.Length];
                    
                    for (int j = 0; j < _settingsParametersComponents[i].values.Length; j++)
                    {
                        stringBuilder.Append(Screen.resolutions[j].width);
                        stringBuilder.Append("x");
                        stringBuilder.Append(Screen.resolutions[j].height);
                        _settingsParametersComponents[i].values[j] = stringBuilder.ToString();
                        stringBuilder.Clear();
                    }

                    string[] distinctValues = _settingsParametersComponents[i].values.Distinct().ToArray();
;
                    _settingsParametersComponents[i].values = distinctValues;

                    break;
                case "_wm":
                    _settingsParametersComponents[i].values = new string[fullScreenOptions.Length];
                    for (int j = 0; j < fullScreenOptions.Length; j++)
                    {
                        stringBuilder.Append(fullScreenOptions[j].ToString());
                        _settingsParametersComponents[i].values[j] = stringBuilder.ToString();
                        stringBuilder.Clear();
                    }

                    break;
                case "_v_sync":
                    _settingsParametersComponents[i].values = new string[2];
                    _settingsParametersComponents[i].values[0] = "OFF";
                    _settingsParametersComponents[i].values[1] = "ON";
                    break;
                case "_mquality":
                    _settingsParametersComponents[i].values = new string[3];
                    _settingsParametersComponents[i].values[0] = "Low";
                    _settingsParametersComponents[i].values[1] = "Medium";
                    _settingsParametersComponents[i].values[2] = "High";
                    break;
                case "_msaa":
                    //needs to be searched in settings 
                    _settingsParametersComponents[i].values = new string[3];
                    _settingsParametersComponents[i].values[0] = "2";
                    _settingsParametersComponents[i].values[1] = "4";
                    _settingsParametersComponents[i].values[2] = "8";
                    break;
                case "_texquality":
                    //needs to be searched in settings 
                    _settingsParametersComponents[i].values = new string[3];
                    _settingsParametersComponents[i].values[0] = "Low";
                    _settingsParametersComponents[i].values[1] = "Medium";
                    _settingsParametersComponents[i].values[2] = "High";
                    break;
                case "_shadows":
                    //needs to be searched in settings 
                    _settingsParametersComponents[i].values = new string[3];
                    _settingsParametersComponents[i].values[0] = "Low";
                    _settingsParametersComponents[i].values[1] = "Medium";
                    _settingsParametersComponents[i].values[2] = "High";
                    break;
            }
        }
    }
}