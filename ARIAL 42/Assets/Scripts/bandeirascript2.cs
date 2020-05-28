using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class bandeirascript2 : MonoBehaviour
{
    //                                xXX BANDEIRA AZUL XXx
    private bool isready;
    private Collider otherref;
    private bool _catch, blue_flag_captured;
   
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
            blue_flag_captured = true;
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
            if (other.gameObject.CompareTag("Player2"))
            {

                otherref = other;
                _catch = true;
               



            }

            if(other.gameObject.CompareTag("redbase") && blue_flag_captured == true)
            {

               // placar.p.enemypoints++;
            }

        }
    }

   
       
    
   


}
