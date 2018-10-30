using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public int Timers;
    public Text Timertxt;
    private float DeltaTime;
    private int EndTime;
    public GameObject Ending;
    apalah apalahh;

	// Use this for initialization
	void Start () {
        Timers = 120;
        DeltaTime = 0;
        EndTime = 0;
        Timertxt.text = Timers.ToString();

        apalahh = GameObject.FindGameObjectWithTag("Player").GetComponent<apalah>();
	}
	
	// Update is called once per frame
	void Update () {
        DeltaTime += Time.deltaTime;
        if (DeltaTime > 1.0f)
        {
            Timers--;
            if (Timers <= 0)
            {
                apalahh.GameOvers();
                Timertxt.text = EndTime.ToString();
            }
            else
            {
                Timertxt.text = Timers.ToString();
            }
            DeltaTime = 0;
        }
	}
}
