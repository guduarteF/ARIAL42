using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class bandeirascript : MonoBehaviour
{
    //                                 xXX BANDEIRA VERMELHA XXx

    private bool isready;
    private Collider otherref;
    private bool _catch , red_flag_captured;

    void Start()
    {
        StartCoroutine(espera());

    }

    // Update is called once per frame
    void Update()
    {
        
        if (_catch == true)
        {
            GetComponent<Rigidbody>().MovePosition(otherref.gameObject.transform.position);
            red_flag_captured = true;
        }

    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(1f);
        isready = true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (isready == true)
        {
            if (other.gameObject.CompareTag("Player"))
            {

                otherref = other;
                _catch = true;
               



            }

            if (other.gameObject.CompareTag("bluebase") && red_flag_captured == true)
            {
              //  placar.p.playerpoints++;

            }

            



        }
    }

   
      
    

    
        

    


}
