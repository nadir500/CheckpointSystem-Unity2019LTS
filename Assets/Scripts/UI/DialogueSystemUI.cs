using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystemUI : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanelBody;
    [SerializeField] private Text _dialogueTextPanel;
    [SerializeField] private DialogueSystemManager _dialogueSystemManager;
    private string _messageText;

    public static DialogueSystemUI _instance;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        _messageText = "aa dd ss ff.... gg";

        //  StartCoroutine(_dialogueSystemManager.TypeText( _dialogueTextPanel,_messageText,0.1f));
        ShowDialgueUI();
    }

    private void ShowDialgueUI()
    {
        _dialoguePanelBody.SetActive(true);
        //fetch dialogue session data from database 

        //
        //split and show first dialogue 
        StartCoroutine(HandleTextCollectionWithInput(_messageText));
    }

    private void CloseDialogueUI()
    {
        _dialoguePanelBody.SetActive(false);
    }

    private IEnumerator HandleTextCollectionWithInput(string messageText)
    {
        //split the message text 
        string[] resultSplit = messageText.Split(' ');
        // yield return  new WaitUntil(() => Input.GetKeyDown(KeyCode.A));

         for (int i = 0; i < resultSplit.Length; i++)
        {
            Debug.Log("split strings " + i) ;
           StartCoroutine(_dialogueSystemManager.TypeText( _dialogueTextPanel,resultSplit[i],0.1f));
            // yield return 0;
       
            yield return  new WaitUntil(() => Input.GetKeyDown(KeyCode.A));
            yield return new WaitForSeconds(0.1f); //for syncing the indexes and not getting all missed up texts  TODO: needs thorough solution more than this 
            //   yield return new WaitWhile(() => Input.GetKeyUp(KeyCode.A));
        }
    }

    bool MouseClickEvent()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}