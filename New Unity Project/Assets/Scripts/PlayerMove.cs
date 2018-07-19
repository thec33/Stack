using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;

	private Rigidbody rb;
	public float walkSpeed;

	private bool isGrounded;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update ()
	{
		MovePlayer();
		
		if(isGrounded == true)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				rb.velocity = Vector3.up * 5;
			}
		}

		
	}

	void MovePlayer()
	{
		//getting the location of the object
		//the names are as perceived from top down
		float horiz = Input.GetAxisRaw("Horizontal");
		float vert = Input.GetAxisRaw("Vertical");

		Vector3 moveHoriz = transform.right * horiz * walkSpeed;
		Vector3 moveVert = transform.forward * vert * walkSpeed;

		rb.position += moveHoriz + moveVert;
		
	}

	void FixedUpdate()
	{
		if(rb.velocity.y < 0)
		{
			rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}

	
		
	
	}

	void OnCollisionEnter(Collision other)
	{
		
		isGrounded = true;
	}

	void OnCollisionExit(Collision other)
	{
		isGrounded = false;
	}

}
