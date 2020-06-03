using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking;
using UnityEngine.UI;

public class final : NetworkBehaviour
{
    #region variaveis
   
    public float deploymentHeight;
    public static final f;

    private RaycastHit pontoDeColisao1;
    private RaycastHit pontoDeColisao2;
    private RaycastHit pontoDeColisao3;
    private RaycastHit pontoDeColisao4;

    public LayerMask points;
    public LayerMask parede;

    private bool ismovingW;
    private bool ismovingS;
    private bool ismovingA;
    private bool ismovingD;


    public bool terminocenter;
    public bool var;


    public static bool disabilita;
    public GameObject bala;
    public GameObject spawnPoint;
    [SyncVar]
    private float fire_rate = 2f;
    [SyncVar]
    private float time_between_shoots;

   // public ParticleSystem part;
    public MeshRenderer mesh;
    public MeshRenderer cilindermesh;
   
    //private GameObject partgo;
    [SyncVar]
    private Color color;


    // private bool shield;
    // private bool speed;
    // private bool firerate;
    // private bool speeddown;
    private float velocity;
    // public GameObject shieldSphere;
    // public ParticleSystem speedpart;
    // public GameObject player2;
    // public GameObject spawnp2;
    // private bool morreu;  
    public static bool col_blue_base,col_red_base;
    private static bool blue_flag_captured, red_flag_captured;
    public string _ID;
    public bool isPlayer1;
    public  Transform spawnp1, spawnp2, spawn_blue_flag, spawn_red_flag;
    public GameObject blueflag, redflag;
    public static bool roundover;
    public static bool restartplayerpos;

   

   





    #endregion


    void Start()
    {
        ismovingA = false;
        ismovingD = false;
        ismovingS = false;
        ismovingW = false;
        terminocenter = true;
        var = true;
        disabilita = true;
        f = this;
        deploymentHeight = 45f;
        velocity = 30f;
     

        _ID = "" + GetComponent<NetworkIdentity>().netId;
        Debug.Log(_ID);

    }

    private void Update()
    {

        if (col_blue_base == false)
        {
            GameObject bandeira_azul = GameObject.Find("Blueflag");
            bandeira_azul.GetComponent<Rigidbody>().MovePosition(spawn_blue_flag.position);
        }

        if (col_red_base == false)
        {
            GameObject bandeira_vermelha = GameObject.Find("Redflag");
            bandeira_vermelha.GetComponent<Rigidbody>().MovePosition(spawn_red_flag.position);
        }

        if (isPlayer1 == true)
        {
            if (restartplayerpos == true)
            {
                gameObject.transform.position = spawnp1.position;
            }
        }
        else
        {
            if (restartplayerpos == true)
            {
                gameObject.transform.position = spawnp2.position;
            }
        }

      
      
       
        
        #region Update
        if (isLocalPlayer)
        {
            if (Input.GetKeyUp(KeyCode.E) && Time.time > time_between_shoots && roundover == false)
            {
                time_between_shoots = Time.time + fire_rate;
                CmdAtirar();

            }   
            
           

        }





       
            if (int.Parse(_ID) % 2 == 0)
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
                transform.gameObject.tag = "Player2";
            }
            else
            {
                GetComponent<MeshRenderer>().material.color = Color.blue;
                isPlayer1 = true;             
                transform.gameObject.tag = "Player";

            }








            if (col_blue_base == true && !isPlayer1 && roundover == false)
            {
                    GameObject bandeira_azul = GameObject.Find("Blueflag");
                    bandeira_azul.GetComponent<Rigidbody>().MovePosition(gameObject.transform.position);
                    blue_flag_captured = true;
                
            }

            if (col_red_base == true && isPlayer1 && roundover == false)
            {
                GameObject bandeira_vermelha = GameObject.Find("Redflag");
                bandeira_vermelha.GetComponent<Rigidbody>().MovePosition(gameObject.transform.position);
                red_flag_captured = true;
              
            }
        
        #endregion


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region INPUT e RAYCAST

        if (isLocalPlayer)
        {


            if (terminocenter == true && ismovingA == false && ismovingD == false && ismovingS == false && ismovingW == false && roundover == false)
            {

                if (Input.GetKey(KeyCode.W) && Physics.Raycast(transform.position, Vector3.forward, out pontoDeColisao1, deploymentHeight, points) && ismovingD == false && ismovingA == false && ismovingS == false && ismovingD == false && ismovingW == false)
                {
                    if (pontoDeColisao1.transform.name != "parede")
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        ismovingW = true;
                    }


                }



                if (Input.GetKey(KeyCode.D) && Physics.Raycast(transform.position, Vector3.right, out pontoDeColisao3, deploymentHeight, points) && ismovingW == false && ismovingA == false && ismovingS == false && ismovingD == false)
                {
                    if (pontoDeColisao3.transform.name != "parede")
                    {
                        transform.rotation = Quaternion.Euler(0, -90, 0);
                        ismovingD = true;
                    }

                }


                if (Input.GetKey(KeyCode.A) && Physics.Raycast(transform.position, -Vector3.right, out pontoDeColisao2, deploymentHeight, points) && ismovingW == false && ismovingS == false && ismovingD == false && ismovingA == false)
                {

                    if (pontoDeColisao2.transform.name != "parede")
                    {
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                        ismovingA = true;
                    }
                }


                if (Input.GetKey(KeyCode.S) && Physics.Raycast(transform.position, -Vector3.forward, out pontoDeColisao4, deploymentHeight, points) && ismovingW == false && ismovingA == false && ismovingD == false && ismovingS == false)
                {
                    if (pontoDeColisao4.transform.name != "parede")
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        ismovingS = true;
                    }
                }


            }
        }



        #endregion

        #region movement

        if (isLocalPlayer && roundover == false)
        {
            if (ismovingW == true)
            {
                if (gameObject.transform.position != pontoDeColisao1.transform.position)
                {

                    transform.position = Vector3.Lerp(gameObject.transform.position, pontoDeColisao1.transform.position, 0.5f * velocity * Time.deltaTime);
                    var = false;
                }
                else
                {
                    var = true;
                    ismovingW = false;
                    ismovingA = false;
                    ismovingS = false;
                    ismovingD = false;
                }


            }

            if (ismovingS == true)
            {
                if (gameObject.transform.position != pontoDeColisao4.transform.position)
                {
                    transform.position = Vector3.Lerp(gameObject.transform.position, pontoDeColisao4.transform.position, 0.5f * velocity * Time.deltaTime);
                    var = false;
                }
                else
                {
                    var = true;
                    ismovingW = false;
                    ismovingA = false;
                    ismovingS = false;
                    ismovingD = false;
                }



            }

            if (ismovingA == true)
            {
                if (gameObject.transform.position != pontoDeColisao2.transform.position)
                {
                    transform.position = Vector3.Lerp(gameObject.transform.position, pontoDeColisao2.transform.position, 0.5f * velocity * Time.deltaTime);
                    var = false;
                }
                else
                {
                    var = true;
                    ismovingW = false;
                    ismovingA = false;
                    ismovingS = false;
                    ismovingD = false;
                }


            }

            if (ismovingD == true)
            {
                if (gameObject.transform.position != pontoDeColisao3.transform.position)
                {
                    transform.position = Vector3.Lerp(gameObject.transform.position, pontoDeColisao3.transform.position, 0.5f * velocity * Time.deltaTime);
                    var = false;
                }
                else
                {
                    var = true;
                    ismovingW = false;
                    ismovingA = false;
                    ismovingS = false;
                    ismovingD = false;
                }


            }
        }
        //XXXXXXXXXXXXXXXXXXXX MOVING XXXXXXXXXXXXXXXXXXXXXX


        if (var == true)
        {
            disabilita = false;
        }
        if (var == false)
        {
            disabilita = true;
        }
        #endregion

        #region POWERUP 


        //if (speed == true)
        //{
        //    velocity = 60f;
        //    UI_TextManager.ui.textgo.SetActive(true);
        //    UI_TextManager.ui.textui.text = "Speed UP";
        //}


        //if (firerate == true)
        //{
        //    fire_rate = 0.5f;
        //    UI_TextManager.ui.textgo.SetActive(true);
        //    UI_TextManager.ui.textui.text = "Fire-rate Buff";
        //}
        //else
        //{
        //    fire_rate = 2;


        //}
        //if (speeddown == true)
        //{
        //    final.f.velocity = 0;
        //    UI_TextManager.ui.textgo.SetActive(true);
        //    UI_TextManager.ui.textui.text = "Stun Red Player";
        //}
        //if(morreu == false && speed == false)
        //{
        //    velocity = 30f;


        //}
        #endregion
        

    }

    #region TRIGGER
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("speed"))
        {
            velocity = velocity + 10f;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        if (other.gameObject.CompareTag("bluebase") && !isPlayer1)
        {
            col_blue_base = true;
        }

        if (other.gameObject.CompareTag("bluebase") && red_flag_captured == true && isPlayer1)
        {
           
            col_red_base = false;
            red_flag_captured = false;
            placar.p.playerpoints++;         
            StartCoroutine(restart_Game());
            Debug.Log("3");
        }

        if (other.gameObject.CompareTag("redbase") && isPlayer1)
        {
            col_red_base = true;
        }
        if (other.gameObject.CompareTag("redbase") && blue_flag_captured == true && !isPlayer1)
        {
            col_blue_base = false;
            blue_flag_captured = false;        
            placar.p.enemypoints++;
            StartCoroutine(restart_Game());
            Debug.Log("4");
        }

        if (other.gameObject.CompareTag("center"))
        {
            terminocenter = true;
            
        }

        if (other.gameObject.CompareTag("bala"))
        {

            
            if (!isPlayer1)
            {
                roundover = true;
                ismovingW = false;
                ismovingA = false;
                ismovingS = false;
                ismovingD = false;
                col_blue_base = false;
                blue_flag_captured = false;
                gameObject.transform.position = spawnp2.position;   
                StartCoroutine(delay());

            }
               

            

            if(isPlayer1)
            {

                roundover = true;
                ismovingW = false;
                ismovingA = false;
                ismovingS = false;
                ismovingD = false;            
                col_red_base = false;
                red_flag_captured = false;
                gameObject.transform.position = spawnp1.position;
                StartCoroutine(delay());

            }

          

            //}
            //else
            //{
            //if (other.GetComponent<MeshRenderer>().material.color == Color.blue)
            //{


            // }
            //}

            //if(other.gameObject.GetComponent<MeshRenderer>().material.color == Color.red)
            //{

            //}
            // if(other.gameObject.GetComponent<MeshRenderer>().material.color == Color.blue)
            // {

            // }





        }

       

        









        //if (other.gameObject.CompareTag("shieldPU"))
        //{
        //    shield = true;
        //    shieldSphere.SetActive(true);
        //    Debug.Log("shield");
        //    Destroy(other.gameObject);
        //    part.transform.position = gameObject.transform.position;
        //    part.Play();

        //}

        //if (other.gameObject.CompareTag("speedPU"))
        //{
        //    StartCoroutine(speedI());
        //    Debug.Log("SPEED");
        //    Destroy(other.gameObject);
        //    part.transform.position = gameObject.transform.position;
        //    part.Play();
        //}

        //if (other.gameObject.CompareTag("fireratePU"))
        //{
        //    StartCoroutine(firerateI());
        //    Debug.Log("FIRERATE");
        //    Destroy(other.gameObject);
        //    part.transform.position = gameObject.transform.position;
        //    part.Play();
        //}

        //if(other.gameObject.CompareTag("speedDownPU"))
        //{
        //    StartCoroutine(stun());
        //    Debug.Log("SPEED DOWN");
        //    Destroy(other.gameObject);
        //    part.transform.position = gameObject.transform.position;
        //    part.Play();
        //}
    }
    #endregion

    IEnumerator restart_Game()
    {
        GameObject bandeira_azul = GameObject.Find("Blueflag");
        GameObject bandeira_vermelha = GameObject.Find("Redflag");

        roundover = true;
        col_blue_base = false;
        col_red_base = false;
        ismovingW = false;
        ismovingA = false;
        ismovingS = false;
        ismovingD = false;
        yield return new WaitForSeconds(1f);

        restartplayerpos = true;

        bandeira_azul.GetComponent<Rigidbody>().MovePosition(spawn_blue_flag.position);
        bandeira_vermelha.GetComponent<Rigidbody>().MovePosition(spawn_red_flag.position);
       
        StartCoroutine(delay());
            Debug.Log("6");
        

    }


    [Command]
    void CmdAtirar()
    {
        GameObject clonebullet = (GameObject)Instantiate(bala, spawnPoint.transform.position, spawnPoint.transform.rotation);
        NetworkServer.Spawn(clonebullet);
     
    }
 

   // [ClientRpc]

   //public void RpcMorte()
   //{
   //     //     morreu = true;
   //     //     if (isServer)
   //     //     {
   //     //         placar.enemypoints++;
   //     //     }
   //   //  GameObject partclone = (GameObject)Instantiate(partgo, transform.position, Quaternion.identity);
   //     //part.transform.position = gameObject.transform.position;
   //     //NetworkServer.Spawn(partclone);
   //     //part.Play();
   //    //     velocity = 0;
   //    //     StartCoroutine(menuManager.RestartDelay());
   //  mesh.enabled = false;
   //  cilindermesh.enabled = false;
        
   //}

    #region IENUMERATOR
    //IEnumerator speedI()
    //{
    //    speed = true;
    //    speedpart.Play();
    //    yield return new WaitForSeconds(3);
    //    UI_TextManager.ui.textgo.SetActive(false);
    //    speedpart.Stop();
    //    speed = false;
    //    Debug.Log("speed false");
    //}
    //IEnumerator firerateI()
    //{
    //    firerate = true;
    //    yield return new WaitForSeconds(4);
    //    UI_TextManager.ui.textgo.SetActive(false);
    //    firerate = false;
    //    Debug.Log("firerate false");
    //}
    //IEnumerator stun()
    //{
    //    speeddown = true;
    //    yield return new WaitForSeconds(2);
    //    UI_TextManager.ui.textgo.SetActive(false);
    //    speeddown = false;
    //    Debug.Log("stun false");
    //}
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1.5f);
        roundover =false;
        restartplayerpos = false;
        Debug.Log("7");
    }
   


    #endregion
}
