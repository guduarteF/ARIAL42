using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class counter : MonoBehaviour
{
    public static int number_local,number_server;
    public Text counter_text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter_update();
        if(number_local >= 5)
        {
            Application.Quit();
        }
        if(number_server >= 5)
        {
            Application.Quit();
        }
    }

    void counter_update()
    {
        counter_text.text = "BRANCO : "  + number_server + " X  AZUL : " + number_local;
    }
   
    
}
