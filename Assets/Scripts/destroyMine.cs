using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMine : MonoBehaviour {

    public float damage = 50;
    private void OnTriggerEnter(Collider other)
    {
        GameObject hhh = GameObject.FindGameObjectWithTag("Player");
        
        if (other.tag == "torpedo")
        {
            //other.GetComponent<apalah>().scoreInt = other.GetComponent<apalah>().scoreInt + 50;
            //other.GetComponent<KendaliSub>().Point = other.GetComponent<KendaliSub>().Point + 50;

            hhh.GetComponent<apalah>().scoreInt += 50;
            Destroy(gameObject); //menghancurkan
            Destroy(other.gameObject);
        }
        if (other.tag == "Player")
        {
            hhh.GetComponent<apalah>().nyawa -= 50;
        }
    }
}
