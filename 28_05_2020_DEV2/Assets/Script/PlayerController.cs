using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            float h = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
            float v = Input.GetAxis("Vertical") * Time.deltaTime * 6;
            transform.Rotate(0, h, 0);
            transform.Translate(0, 0, v);
        }



     
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("coin"))
        {
            if(isLocalPlayer)
            {
                counter.number_local++;
                Destroy(collision.gameObject);
            }
            if(!isLocalPlayer)
            {
                counter.number_server++;
                Destroy(collision.gameObject);
            }
          
          

        }
       
      
        
          
          
       
            



        
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;

    }
  
    


}
