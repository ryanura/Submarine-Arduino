using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propeller : MonoBehaviour {

	public GameObject sub;
	public Transform propRight;
	public Transform propLeft;

	// Use this for initialization
	void Start () {
		sub = this.gameObject;
		propRight = sub.transform.GetChild (0).GetChild (0).GetChild (1).GetChild (0);
		propLeft = sub.transform.GetChild (0).GetChild (0).GetChild (0).GetChild (0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.R)) {
			SendMessage ("Checkspinning", true);
			propRight.RotateAround (propRight.position, propRight.up, Time.deltaTime * -500f);
			propLeft.RotateAround (propLeft.position, propLeft.up, Time.deltaTime * 500f);
		}
		if (Input.GetKey (KeyCode.F)) {
			SendMessage ("Checkspinning", true);
			propRight.RotateAround (propRight.position, propRight.up, Time.deltaTime * 90f);
			propLeft.RotateAround (propLeft.position, propLeft.up, Time.deltaTime * -90f);
		}
		SendMessage ("check spinning", false);
	}
}
