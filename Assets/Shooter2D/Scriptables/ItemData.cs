using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum selection type d'objet
enum ItemType
{
    Weapon,
    Ammo,
    Object
}

// menu creation scriptable + parametres
[CreateAssetMenu(menuName = "Scriptable/ItemData", fileName = "newItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
}