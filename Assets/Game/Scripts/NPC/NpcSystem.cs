using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NpcSystem : MonoBehaviour
{
    public GameObject chatPanel;
    public TextMeshProUGUI who;
    public TextMeshProUGUI Chat;
    public string[] lines;
    public float textSpeed;
    private int index;
    private bool Playerisclose;
    public GameObject conButton;
    public void OnTriggerEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Playerisclose= true;
        }
    }
    public void OnTriggerExite(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Playerisclose = false;
            ZeroText();
        }
    }

    private void Update()
    {
        who.text = gameObject.name;
        if(Input.GetKeyUp(KeyCode.F))
        {
            if(chatPanel.activeInHierarchy)
            {
                ZeroText();
            }
            else
            {
                chatPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if(Chat.text == lines[index])
        {
            conButton.SetActive(true);
        }
    }

 
   
    IEnumerator Typing()
    {
        foreach(char c in lines[index])
        {
            Chat.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    public void ZeroText()
    {
        Chat.text = "";
        index = 0;
        chatPanel.SetActive(false);
    }
    public void NextLine()
    {
        conButton.SetActive(false) ;

        if(index < lines.Length - 1)
        {
            index++;
            Chat.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ZeroText() ;        
        }
    }
}



    
