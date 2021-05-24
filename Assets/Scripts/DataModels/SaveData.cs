using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string objID { get; set; }
    public string player { get; set; }
    public string[] unlockedCheckpointIDs { get; set; }
    public int[] lastPosition { get; set; }
    public int hp { get; set; }
    public int mana { get; set; }
    public int anger { get; set; }

    public SaveData() //default constructor 
    {
        objID = "n/a";
        player = "n/a";
        unlockedCheckpointIDs = new string[0];
        lastPosition = new int[3];
        hp = 100;
        mana = 100;
        anger = 0;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(" save data info--> ");
        stringBuilder.Append(" objID--> ");
        stringBuilder.Append(objID);
        stringBuilder.Append(" player--> ");
        stringBuilder.Append(player);
        stringBuilder.Append(" unlockedCheckpoints array--> ");
        for (int i = 0; i < unlockedCheckpointIDs.Length; i++)
        {
            stringBuilder.Append(unlockedCheckpointIDs[i]);
            stringBuilder.Append(" ");
        }
        stringBuilder.Append(" last position--> ");
        for (int i = 0; i < lastPosition.Length; i++)
        {
            stringBuilder.Append(lastPosition[i]);
            stringBuilder.Append(" ");
        }
        stringBuilder.Append(" hp--> ");
        stringBuilder.Append(hp);
        stringBuilder.Append(" mana--> ");
        stringBuilder.Append(mana);
        stringBuilder.Append(" anger--> ");
        stringBuilder.Append(anger);
        return stringBuilder.ToString();
    }
}