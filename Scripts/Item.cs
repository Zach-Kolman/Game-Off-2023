using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite itemImage;
    public Transform prefabModel;

    public bool inspectable;
    public bool droppable;
    public bool usable;
}

