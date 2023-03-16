using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IDataPersistence
{
    [SerializeField] ItemData datas;
    [SerializeField] InventoryScriptable inventory;
    Action onItemPicked;
    private bool collected = false;

    [SerializeField] private string id;
    [ContextMenu("Generate guid for ID")]

    private void GenerateGuid()
    {
        id = Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        data.gunsCollected.TryGetValue(id, out collected);
        if (collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.gunsCollected.ContainsKey(id))
        {
            data.gunsCollected.Remove(id);
        }
        data.gunsCollected.Add(id, collected);
    }

    void CollectGun()
    {
        collected = true;
        inventory.ItemList.Add(datas);
        gameObject.SetActive(false);
        GameEventsManager.instance.GunCollected();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collected)
        {
            CollectGun();
        }
    }
}