using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//menambah jumlah torpedo

public class SpinTorpedo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "pasopati")
        {
            other.GetComponent<PointScript>().torpedo = other.GetComponent<PointScript>().torpedo + 10;
            other.GetComponent<apalah>().amunisi = other.GetComponent<apalah>().amunisi + 10;
            other.GetComponent<KendaliSub>().AmunitionInt = other.GetComponent<KendaliSub>().AmunitionInt + 10;
            Destroy(gameObject); //menghancurkan 
        }
    }
}
