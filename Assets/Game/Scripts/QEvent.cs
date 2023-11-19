using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class QEvent : MonoBehaviour
{
    [FormerlySerializedAs("text")] public TMP_Text textMesh;
    public int length = 10;
    public string keyList;
    private List<KeyCode> keyCodeList = new List<KeyCode>();
    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        textMesh.text = "";
        keyCodeList = Generate();
        UpdateUI();
    }

    private void UpdateUI()
    {
        var reversed = new List<KeyCode>(keyCodeList);
        reversed.Reverse();
        var first = reversed[0];
        
        
        textMesh.text = first + " + <color=grey>" + string.Join(" + ", reversed.GetRange(1, reversed.Count - 1));
    }

    private void Update()
    {
        if (textMesh.text.Length > 0)
        {
            var key = keyCodeList[^1];
            Debug.Log(key);
            if (Input.GetKeyDown(key))
            {
                keyCodeList.RemoveAt(keyCodeList.Count - 1);
                UpdateUI();
            }
        }
    }

    public List<KeyCode> Generate()
    {
        var res = new List<KeyCode>();
        for (int i = 0; i < length; i++)
        {
            var rnd = Random.Range(0, keyList.Length);
            var character = keyList[rnd];
            KeyCode keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), character.ToString().ToUpper(), true);
            res.Add(keyCode);
        }

        return res;
    }
}