using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfollower : MonoBehaviour {

    public Transform[] path;
    public float speed = 5.0f;
    public float reachdist = 1.0f;
    public int titikawal = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       // Vector3 dir = path[titikawal].position - transform.position;
        float dist = Vector3.Distance(path[titikawal].position, transform.position);

        transform.position = Vector3.MoveTowards(transform.position, path[titikawal].position, Time.deltaTime * speed);

        if (dist <= reachdist)
        {
            titikawal++;
        }

        if (titikawal >= path.Length)
        {
            titikawal = 0;
        }
	}

    void OnDrawGizmoz()
    {
        if (path.Length > 0)
        for (int i = 0; i < path.Length; i++)
        {
            if (path[i] != null)
            {
                Gizmos.DrawSphere(path[i].position, reachdist);
            }
        }
    }
}
