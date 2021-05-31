using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.HighDefinition;

public class ResolutionSettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject _resolutionSettingsBodyPanel;
    [SerializeField] private HDRenderPipelineAsset _hdRenderPipelineAsset;
    private SettingsParametersComponent[] _settingsParametersComponents;
    private Text[] _parametersValues;

    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
        SetGraphicsSettings();//on UI
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
        //load saved values from general settings manager

        //
    }

    //set values on UI
    private void SetGraphicsSettings() 
    {
        StringBuilder stringBuilder = new StringBuilder();
        FullScreenMode[] fullScreenOptions = {
            FullScreenMode.ExclusiveFullScreen,
            FullScreenMode.FullScreenWindow, //FullScreenWindow = BorderlessFullscreen. MaximizedWindow is a MacOS-only thing.
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