using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

	public float ketinggian;
	public float waktu;

	// Use this for initialization
	void Start () {
		iTween.MoveBy (this.gameObject, iTween.Hash ("y", ketinggian, "time", waktu, "looptype", "pingpong", "easetype", iTween.EaseType.easeInOutSine));
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
