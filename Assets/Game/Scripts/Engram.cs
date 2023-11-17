using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Engram : MonoBehaviour
{
    public TextMeshProUGUI engramText;
    private int engramNum;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enegram")
        {
            if (Input.GetKey(KeyCode.E))
            {
                engramNum++;
                engramText.text = "x" + engramNum.ToString();
                Destroy(collision.gameObject);
            }
           
        }
    }
}
