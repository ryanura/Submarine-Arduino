using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePropellerL : MonoBehaviour
{

    public GameObject PropellerLeft;

    // Update is called once per frame
    void Update()
    {
        PropellerLeft.transform.Rotate(0, 0, 20);

    }
}
