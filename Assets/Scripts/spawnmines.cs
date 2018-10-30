using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//bila terkena tembakan torpedo, maka akan meledak dan menghilang serta menambah poin pemain sebanyak 50
//bila terkena kapal selam, maka akan meledak dan mengurangi health poin sebanyak 25

public class spawnmines : MonoBehaviour {

    public GameObject nmines;
    public Transform minespawn;
    private float spawntime;

	// Use this for initialization
	void Start () {
        spawntime = 0;
	}
	
	// Update is called once per frame
	void Update () {
       // minespawn.position += minespawn.up * 1f * Time.deltaTime;
        spawntime += Time.deltaTime;
        if (spawntime>7.0f)
        {
            Spawn();
            spawntime = 0;
        }
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "pasopati")
        {
            other.GetComponent<PointScript>().health--;
            Destroy(gameObject);
        }
    }

    void Spawn()
    {
        GameObject mines = (GameObject)Instantiate(nmines, minespawn.position, minespawn.rotation);
        mines.GetComponent<Rigidbody>().velocity = mines.transform.up * -1.0f;
    }
}
