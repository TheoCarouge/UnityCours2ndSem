using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    [SerializeField] GameObject entryPrefab;

    [SerializeField] float Radius = 300f;

    [SerializeField] List<Texture> Icons;

    [SerializeField] RawImage TargetIcon;

    List<RadialMenuEntry> Entries;


    private void Start()
    {
        Entries = new List<RadialMenuEntry>();
    }

    void AddEntry(string pLabel, Texture pIcon, RadialMenuEntry.RadialMenuEntryDelegate pCallback)
    {
        GameObject entry = Instantiate(entryPrefab, transform);

        RadialMenuEntry rme = GetComponent<RadialMenuEntry>();
        rme.SetLabel(pLabel);
        rme.SetIcon(pIcon);
        rme.SetCallback(pCallback);

        Entries.Add(rme);
    }

    public void Open()
    {
        for (int i = 0; i < 8; i++)
        {
            AddEntry(" Button" + i.ToString(), Icons[i], SetTargetIcon);
        }
        Rearrange();
    }

    public void Close()
    {
        for (int i = 0; i < 8; i++)
        {
            RectTransform rect = Entries[i].GetComponent<RectTransform>();
            GameObject entry = Entries[i].gameObject;
            rect.DOAnchorPos(Vector3.zero, .3f).SetEase(Ease.OutQuad).onComplete =
                delegate ()
                {
                    Destroy(entry);
                };
        }

        Entries.Clear();
    }

    public void Toggle()
    {
        if (Entries.Count == 0)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Rearrange()
    {
        float radianOfSeparation = (Mathf.PI * 2) / Entries.Count;
        for (int i = 0; i < Entries.Count; i++)
        {
            float x = Mathf.Sin(radianOfSeparation * i) * Radius;
            float y = Mathf.Cos(radianOfSeparation * i) * Radius;

            RectTransform rect = Entries[i].GetComponent<RectTransform>();

            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
            rect.DOAnchorPos(new Vector3(x, y, 0), .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
        }
    }

    void SetTargetIcon(RadialMenuEntry pEntry)
    {
        TargetIcon.texture = pEntry.GetIcon();
    }
}