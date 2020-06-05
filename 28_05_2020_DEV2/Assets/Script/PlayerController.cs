using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    [SyncVar]
    int vida = 5;
    public GameObject bullet;
    public Transform bullet_pos;
   

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

        if(isLocalPlayer)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                CmdFire();
            }
        }
       
        if(Input.GetKeyUp(KeyCode.R))
        {
            ChangeScene();
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

        if(collision.gameObject.CompareTag("bala"))
        {
            vida--;
            if(vida==0)
            {
                gameObject.SetActive(false);
                gameover.g.canvas.SetActive(true);
            }
        }
       
      
        
          
          
       
            



        
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;

    }
  
    [Command]
    void CmdFire()
    {
        GameObject bala = (GameObject)Instantiate(bullet, bullet_pos.position, bullet_pos.rotation);

        bala.GetComponent<Rigidbody>().velocity = bala.transform.forward * 10;
        NetworkServer.Spawn(bala);
        Destroy(bala, 2);
    }

    void ChangeScene()
    {
        NetworkManager.singleton.ServerChangeScene("teste");
    }
}
