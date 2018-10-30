using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePropellerR : MonoBehaviour {

    public GameObject PropellerRight;
	
	// Update is called once per frame
	void Update () {
        PropellerRight.transform.Rotate(0, 0, -20);
	}
}
