using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystemManager : MonoBehaviour
{
    public IEnumerator TypeText(Text dialogueContainer, string messageText, float letterPauseRate)
    {
        dialogueContainer.text = "";
        dialogueContainer.text += messageText;
        // foreach (char letter in messageText)
        // {
        //     //Add 1 letter each tm
        //    // string res = ArabicFixer.Fix(letter.ToString(), true, false);
        //    dialogueContainer.text += letter.ToString();
        yield return 0;            // Waits for seconds assignedn letterPauseRate (eg. 0.5f)
        //     //yield return new WaitForSeconds(letterPauseRate * Time.deltaTime);
        //   // yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.A));
        // }
        //make next button available 

        //confirmDialogueButton.interactable = true;
    }

    public void TypeTextString(Text dialogueContainer, string messageText, float letterPauseRate)
    {
        dialogueContainer.text = "";
        // foreach (char letter in messageText.ToCharArray())
        // {
        //Add 1 letter each tm
        // string res = ArabicFixer.Fix(letter.ToString(), true, false);
        dialogueContainer.text += messageText.ToString();
        // yield return 0;            // Waits for seconds assignedn letterPauseRate (eg. 0.5f)
        // yield return new WaitForSeconds(letterPauseRate * Time.deltaTime);
        // }
        //make next button available 

        //confirmDialogueButton.interactable = true;
    }
}