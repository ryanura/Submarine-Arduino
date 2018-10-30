using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {
	public Transform cameraTarget;
	public Vector3 distance = new Vector3 (0f,5f,-10f);

	public float positionDamping = 2.0f;
	public float rotationDamping   = 2.0f;

	private Transform selfTransform;  


	// Use this for initialization
	void Start () {
		selfTransform = transform; //cached self tranform
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!cameraTarget) 
		{
			return;
		}

		//position
		Vector3 wanted_position = cameraTarget.position + (cameraTarget.rotation * distance);
		Vector3 current_position = Vector3.Lerp (selfTransform.position, wanted_position, positionDamping * Time.deltaTime);
		selfTransform.position = current_position;

		//rotation


		Quaternion current_rotation = Quaternion.Lerp (selfTransform.rotation, cameraTarget.rotation, rotationDamping * Time.deltaTime);
		selfTransform.rotation = current_rotation;

	}
}
