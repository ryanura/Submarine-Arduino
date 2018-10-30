using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gerak : MonoBehaviour {
	private float tmpZ;
	private float tmpY;
	private float tmpX;

	private Transform subTransform;
	private Vector3 tmpVect;

	private bool spinning = false;

	// Use this for initialization
	void Start () {
		subTransform = gameObject.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CheckSpinning(bool result){
		spinning = result;
		if (spinning == true) {
			moveSub ();
		}
	}

	void moveSub(){
		tmpX = .0f;
		tmpY = .0f;
		tmpZ = .01f;
		tmpVect.Set (tmpX, tmpY, tmpZ);
		subTransform.Translate (tmpVect);
	}
}
