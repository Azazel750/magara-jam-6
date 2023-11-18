using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapePanel : MonoBehaviour
{
    public Image image;
    public float speed = 3;

    private float fillAmount;

    private bool isOpened = false;

    private void Update()
    {
        image.fillAmount = Mathf.MoveTowards(image.fillAmount, fillAmount, Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpened) Close();
            else Open();
        }
    }

    public void Open()
    {
        fillAmount = 1;
        isOpened = true;
    }
    
    public void Close()
    {
        fillAmount = 0;
        isOpened = false;
    }

}
