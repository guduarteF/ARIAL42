﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class placar : NetworkBehaviour
{ 


    public static placar p;
    
   public Text placarText;

   
   public int enemypoints;
   
   public int playerpoints;




    void Start()
    {
       
        p = this;
    }

    // Update is called once per frame
    void Update()
    {
        PlacarText();
    }


   
    public void PlacarText()
    { 
        
        placarText.text = playerpoints + "x" + enemypoints;
        
       
    }

   


}
