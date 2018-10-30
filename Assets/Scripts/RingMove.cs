using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ketika dilewati tengahnya oleh kapal selam, ring menghilang dan poin pemain bertambah 25
public class RingMove : MonoBehaviour {

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "pasopati")
        {
            other.GetComponent<apalah>().scoreInt = other.GetComponent<apalah>().scoreInt + 25;
            other.GetComponent<KendaliSub>().Point = other.GetComponent<KendaliSub>().Point + 25;
            Destroy(gameObject); //menghancurkan 
        }
    }
}
