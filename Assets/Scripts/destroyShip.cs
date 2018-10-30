using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyShip : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        GameObject hhh = GameObject.FindGameObjectWithTag("Player");
        
        if (other.tag == "torpedo")
        {
            hhh.GetComponent<apalah>().scoreInt += 75;
            Destroy(gameObject); //menghancurkan 
            Destroy(other.gameObject);
            hhh.GetComponent<apalah>().GameOvers();
        }
    }
}
