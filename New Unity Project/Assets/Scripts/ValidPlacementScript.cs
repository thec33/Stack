using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ValidPlacementScript : MonoBehaviour {

public static bool validPlacement;

		
void Start()
{
	validPlacement = true;
}

		void OnTriggerStay(Collider other)
			{
				
				if(other.gameObject.GetComponent<Rigidbody>() && other.tag == "NotPlaceable") //DO NOT HECCING CHANGE doesn't work with any other format
				{
					Debug.Log("test1");
					validPlacement = false;
				}
				else
				{
					validPlacement = true;
				}
			}

			

			

			

		

			
}
