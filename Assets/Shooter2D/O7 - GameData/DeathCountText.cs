using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeathCountText : MonoBehaviour, IDataPersistence
{
    private int deathCount = 0;

    private TextMeshProUGUI deathCountText;

    private void Awake()
    {
        deathCountText = this.GetComponent<TextMeshProUGUI>();
    }

    public void LoadData(GameData data)
    {
        this.deathCount = data.deathCount;
    }

    public void SaveData(ref GameData data)
    {
        data.deathCount = this.deathCount;
    }

    private void Start()
    {
        // sub to events
        GameEventsManager.instance.onPlayerDeath += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        // unsub to events
        GameEventsManager.instance.onPlayerDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        deathCount++;
    }

    private void Update()
    {
        deathCountText.text = "" + deathCount;
    }
}
