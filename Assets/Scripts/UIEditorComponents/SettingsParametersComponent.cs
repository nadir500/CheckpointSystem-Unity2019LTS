using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsParametersComponent : MonoBehaviour
{
    public string componentTag; 
    public string[] values; // All the avaliable values of each parameter
    public string unitOfParameterValue; // The Unit of the parameter (ex inch)
    public string parameterKey; // THe Parameter Key in playePrefs
    
    public bool
        isIterativeValues; // if the values ar Sequence of number with constant step this boolean is active a foor loop to create the values 

    public float startValue; // if isIterativeValues = true this will be the first value of values
    public int numberOfValues; // if isIterativeValues = true this will be the number of values
    public float IterationStepValue; // if isIterativeValues = true this will be the step between each two numbers

    public int _index = 0; // it is public because it will be set in the inspector to default value

    public int index
    {
        get { return _index; }
        set
        {
            _index = value;
            indexValidator();
        }
    }

    public Text parameterText; // Reference to the text component that holds the value of this parameter

    public string parameterValue
    {
        // Probrity to manage the parameter text value settings    (
        // Remove the unit before reading the value 
        get
        {
            return (string.IsNullOrEmpty(unitOfParameterValue))
                ? parameterText.text
                : parameterText.text.Replace(" " + unitOfParameterValue, "");
        }
        // Add the unit when I assign a new value to this parameter and show it in "parameterText" and if link to resources = true i read the texture with same value and assign it the child image
        set
        {
            parameterText.text =
                value + ((string.IsNullOrEmpty(unitOfParameterValue)) ? "" : " " + unitOfParameterValue);
        }
    }

    public Button
        plusButton; //  Reference to plus button in this paramater  | Assigne in the inspector to the child of this parameter parameterControlls > plusButton

    public Button
        minusButton; //  Reference to minus button in this paramater  | Assigne in the inspector to the child of this parameter parameterControlls > minusButton

    public delegate void OnVariableChangeDelegate();

    public event OnVariableChangeDelegate OnVariableChange;

    void Start()
    {
        if (isIterativeValues)
        {
            values = new string[numberOfValues];
            //  Debug.Log("count = " + values.Length);
            for (int i = 0; i < numberOfValues; i++)
                values[i] = System.Math.Round((startValue + IterationStepValue * i), 2).ToString();
        }

        parameterValue = values[index];
        Debug.Log("parameter values " +  parameterValue );
        plusButton.onClick.AddListener(increase);     // no need to add onclick event to the buttons because the click event with be handeled in LongClick class that atteched to the button itself
        minusButton.onClick.AddListener(decrease);    // no need to add onclick event to the buttons because the click event with be handeled in LongClick class that atteched to the button itself

        //Read the values from player prefs
        // if (!string.IsNullOrEmpty(parameterKey) && PlayerPrefs.HasKey(parameterKey) &&
        //     (!string.IsNullOrEmpty(PlayerPrefs.GetString(parameterKey))))
        // {
        //     parameterValue = PlayerPrefs.GetString(parameterKey);
        //     index = System.Array.IndexOf(values, parameterValue);
        // }

        indexValidator();
    }

    public void increase()
    {
        index++;
        parameterValue = values[index];
        if (OnVariableChange != null)
            OnVariableChange();
    }

    public void decrease()
    {
        index--;
        parameterValue = values[index];

        if (OnVariableChange != null)
            OnVariableChange();
    }

    private void indexValidator()
    {
        if (_index == values.Length - 1)
            plusButton.interactable = false;
        else
            plusButton.interactable = true;

        if (_index == 0)
            minusButton.interactable = false;
        else
            minusButton.interactable = true;
    }
}