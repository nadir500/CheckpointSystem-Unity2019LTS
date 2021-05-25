using System;
using System.Text;
using UnityEngine;

[Serializable]
public class ItemData
{
    public int id { get; set; }

    public Sprite icon { get; set; }

    public ItemData()
    {
        id = 0;
        icon = null;
    }
/// <summary>
/// item data model initialization 
/// </summary>
/// <param name="_id">item id int</param>
/// <param name="_icon">item icon sprite</param>
    public ItemData(int _id, Sprite _icon)
    {
        id = _id;
        icon = _icon;
    }

public override string ToString()
{
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(" item id--> ");
    stringBuilder.Append(id);
    stringBuilder.Append(" icon sprite name--> ");
    stringBuilder.Append(icon.name);
    return base.ToString();
}
}