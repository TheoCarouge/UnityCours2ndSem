using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Inventory", fileName = "newInventory")]
public class InventoryScriptable : ScriptableObject
{
    [SerializeField] private bool clearListOnEnable;
    [SerializeField] List<ItemData> itemList;
    public List<ItemData> ItemList { get { return itemList; }} // Ne pas oublier de clear la liste sinon elle restera la m�me d'une partie � l'autre

    private void OnEnable()
    {
        if (clearListOnEnable)
        {
            ItemList.Clear();
        }
    }
}
