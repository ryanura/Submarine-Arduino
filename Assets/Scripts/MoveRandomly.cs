using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveRandomly : MonoBehaviour {

    NavMeshAgent NavMeshAgent;
    NavMeshPath path;
    public float timeFornewPath;
    bool inCourutine;
    Vector3 Target;
    bool validPath;
   // public int newtarget;
   // public float speed;
   

	// Use this for initialization
	void Start () {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
	}

    void Update()
    {
        if (!inCourutine)
            StartCoroutine(DoSomething());
    }

    Vector3 getNewRandomPos()
    {
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

    IEnumerator DoSomething()
    {
        inCourutine = true;
        yield return new WaitForSeconds(timeFornewPath);
        GetNewPath();
        validPath = NavMeshAgent.CalculatePath(Target, path);
        if (!validPath) Debug.Log("Found an invalid path");

        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = NavMeshAgent.CalculatePath(Target, path);
        }
        inCourutine = false;
    }

    void GetNewPath()
    {
        Target = getNewRandomPos();
        NavMeshAgent.SetDestination(Target);
    }
	// Update is called once per frame
    void newTarget()
    {
        float myx = gameObject.transform.position.x;
        float myz = gameObject.transform.position.z;

        float xpos = myx + Random.Range(myx - 100, myx + 100);
        float zpos = myz + Random.Range(myz - 100, myz + 100);

        Target = new Vector3(xpos, gameObject.transform.position.y, zpos);

        NavMeshAgent.SetDestination(Target);
    }
}
