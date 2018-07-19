using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ValidPlacementScript : MonoBehaviour {

public static bool validPlacement;

		

		void OnTriggerStay(Collider other)
			{
				Debug.Log("I am touching someting");
				if(other.tag == "NotPlaceable")
				{
					Debug.Log("not buildable entered");
					validPlacement = false;
				}
				if(other.gameObject.GetComponent(typeof(BuildableObject)))
				{
					Debug.Log("YEE");
					validPlacement = true;
				}
			}

			

			

			

		

			
}
