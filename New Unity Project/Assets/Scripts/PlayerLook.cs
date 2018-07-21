using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

	public float _sensitivity;
	public Transform playerBody;

	float xAxisClamp = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!Input.GetKey(KeyCode.X))
		{
			RotateCamera();
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			
			Cursor.visible = true;
		}
		
	}

	void RotateCamera()
	{
		//lock the cursor for every frame
			Cursor.lockState = CursorLockMode.Locked;
		//get the mouse input
		float mouseX = Input.GetAxisRaw("Mouse X");
		float mouseY = Input.GetAxisRaw("Mouse Y");

		//multiply by sens
		float rotAmountX = mouseX * _sensitivity;
		float rotAmountY = mouseY * _sensitivity;

		//get the rotation we want to make as a vector
		Vector3 targetRotCam = transform.rotation.eulerAngles;
		Vector3 targetRotBody = playerBody.rotation.eulerAngles;

		//add to this what we have got as input
		targetRotCam.x -= rotAmountY;
		targetRotCam.z = 0;
		targetRotBody.y += rotAmountX;

		xAxisClamp -= rotAmountY;

		//restricting the camera from doing a cheeki flip
		if(xAxisClamp > 90)
		{
			xAxisClamp = targetRotCam.x = 90;
		}
		else if(xAxisClamp < -90)
		{
			xAxisClamp = -90;
			targetRotCam.x = 270;
		}

		transform.rotation = Quaternion.Euler(targetRotCam);
		playerBody.rotation = Quaternion.Euler(targetRotBody);
	}
}
