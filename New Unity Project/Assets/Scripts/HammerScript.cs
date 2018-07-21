
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HammerScript : MonoBehaviour {

	//variables
	public float damage = 10f;
	public float range = 100f;
	
	public int reserveMaterials = 120;

	//objects
	public Camera fpsCam;
	
	public GameObject previewRampObject;
	public GameObject buildRampObject;

	public GameObject previewFloorObject;
	public GameObject buildFloorObject;

	public TextMeshProUGUI materialsCurrentText;

	public TextMeshProUGUI noMaterialsText;
	public Material validMaterial;
	public Material invalidMaterial;
	
	private bool previewExists;
	

	// Use this for initialization
	void Start () {
		
		
		noMaterialsText.gameObject.SetActive(false);
		
		CreateRampPreview();
		CreateFloorPreview();
		previewRampObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		

		materialsCurrentText.text = reserveMaterials.ToString();

		if(reserveMaterials > 0)
		{
				noMaterialsText.gameObject.SetActive(false);
			
		
			if(Input.GetButton("Fire1"))
			{
					ControlPreview();
			}

			if(Input.GetButtonUp("Fire1"))
			{
				
				
				Build();
				
			}

		}
		else
		{
			noMaterialsText.gameObject.SetActive(true);
		}
				

	}

	void CreateRampPreview()
		{
			
			RaycastHit hit;
			//if we hit something
			 if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, 9))
			 {
				//create object with the correct material
				 BuildableObject target = hit.transform.GetComponent<BuildableObject>();
				 if(target != null && ValidPlacementScript.validPlacement== true)
				 {
					previewRampObject = Instantiate(previewRampObject, hit.point, Quaternion.LookRotation(Vector3.forward));
					previewRampObject.GetComponent<MeshRenderer>().material = validMaterial;
				 	
				 }
				 if(target != null && ValidPlacementScript.validPlacement== false)
				 {
					previewRampObject = Instantiate(previewRampObject, hit.point, Quaternion.LookRotation(Vector3.forward));
					previewRampObject.GetComponent<MeshRenderer>().material = invalidMaterial;
				 	
				 }
				 
				 

				
				 
			 }
		}

		void CreateFloorPreview()
		{
			
			RaycastHit hit;
			//if we hit something
			 if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, 9))
			 {
				 

				//create object with the correct material
				 BuildableObject target = hit.transform.GetComponent<BuildableObject>();
				 if(target != null && ValidPlacementScript.validPlacement== true)
				 {
					previewFloorObject = Instantiate(previewFloorObject, hit.point, Quaternion.LookRotation(Vector3.forward));
					previewFloorObject.GetComponent<MeshRenderer>().material = validMaterial;
				 	
				 }
				 if(target != null && ValidPlacementScript.validPlacement== false)
				 {
					previewFloorObject = Instantiate(previewFloorObject, hit.point, Quaternion.LookRotation(Vector3.forward));
					previewFloorObject.GetComponent<MeshRenderer>().material = invalidMaterial;
				 	
				 }
				 
				 

				
				 
			 }
		}
/*
 
    __                                  _   _               _   _             __ _             _         _     _           _   
   / _| ___  _ __    ___ _ __ ___  __ _| |_(_)_ __   __ _  | |_| |__   ___   / _(_)_ __   __ _| |   ___ | |__ (_) ___  ___| |_ 
  | |_ / _ \| '__|  / __| '__/ _ \/ _` | __| | '_ \ / _` | | __| '_ \ / _ \ | |_| | '_ \ / _` | |  / _ \| '_ \| |/ _ \/ __| __|
  |  _| (_) | |    | (__| | |  __/ (_| | |_| | | | | (_| | | |_| | | |  __/ |  _| | | | | (_| | | | (_) | |_) | |  __/ (__| |_ 
  |_|  \___/|_|     \___|_|  \___|\__,_|\__|_|_| |_|\__, |  \__|_| |_|\___| |_| |_|_| |_|\__,_|_|  \___/|_.__// |\___|\___|\__|
                                                    |___/                                                   |__/               
 
*/
		void Build()
		{
			previewRampObject.transform.Rotate(0, 0, 0);
			previewFloorObject.transform.Rotate(0, 0, 0);
			
			if(ValidPlacementScript.validPlacement == false)
			{
				ValidPlacementScript.validPlacement = true;
				previewRampObject.SetActive(false);
				previewFloorObject.SetActive(false);
				return;
			}


			previewRampObject.SetActive(false);
			previewFloorObject.SetActive(false);

			
			
			
			
					 if(ItemSelect.angleOfSelected > 180)
					 {
						reserveMaterials--;
						GameObject buildGameObject = Instantiate(buildRampObject, previewRampObject.transform.position, Quaternion.LookRotation(previewRampObject.transform.forward, previewRampObject.transform.up));
						
					 }
					 else if(ItemSelect.angleOfSelected < 180)
					 {
						reserveMaterials--;
						GameObject buildGameObject = Instantiate(buildFloorObject, previewFloorObject.transform.position, Quaternion.LookRotation(previewFloorObject.transform.forward, previewFloorObject.transform.up));
					 }
			 
		}


/*
 
    __                              _             _ _ _               _   _            _           _ _     _                        _               
   / _| ___  _ __    ___ ___  _ __ | |_ _ __ ___ | | (_)_ __   __ _  | |_| |__   ___  | |__  _   _(_) | __| |  _ __  _ __ _____   _(_) _____      __
  | |_ / _ \| '__|  / __/ _ \| '_ \| __| '__/ _ \| | | | '_ \ / _` | | __| '_ \ / _ \ | '_ \| | | | | |/ _` | | '_ \| '__/ _ \ \ / / |/ _ \ \ /\ / /
  |  _| (_) | |    | (_| (_) | | | | |_| | | (_) | | | | | | | (_| | | |_| | | |  __/ | |_) | |_| | | | (_| | | |_) | | |  __/\ V /| |  __/\ V  V / 
  |_|  \___/|_|     \___\___/|_| |_|\__|_|  \___/|_|_|_|_| |_|\__, |  \__|_| |_|\___| |_.__/ \__,_|_|_|\__,_| | .__/|_|  \___| \_/ |_|\___| \_/\_/  
                                                              |___/                                           |_|                                   
 
*/
		void ControlPreview()
		{

			

			//this is to prevent having two previews when using the radial menu and holding the preview button
			previewFloorObject.SetActive(false);
			previewRampObject.SetActive(false);

			//if we choose a ramp
			if(ItemSelect.angleOfSelected > 180)
			{

				if(Input.GetKeyDown(KeyCode.R))
				 {
					previewRampObject.transform.Rotate(Vector3.down * -90);
					
				 }
				
			RaycastHit hit;
			if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, 9))
			 {
				 
				previewRampObject.SetActive(true);
				if(hit.collider.tag == "Floor")
				{
					Vector3 placementSpot = new Vector3(Mathf.RoundToInt(hit.collider.bounds.ClosestPoint(hit.point).x/4)*4,Mathf.RoundToInt((hit.collider.bounds.ClosestPoint(hit.point).y)/4)*4, Mathf.RoundToInt(hit.collider.bounds.ClosestPoint(hit.point).z/4)*4); //grid placement
					previewRampObject.transform.position = placementSpot;
				}
				else if(hit.collider.tag == "BuiltItem")
				{
					Vector3 placementSpot = new Vector3(Mathf.RoundToInt(hit.collider.bounds.ClosestPoint(hit.point).x/4)*4,(Mathf.RoundToInt((hit.collider.bounds.ClosestPoint(hit.point).y)/4)*4)+2, Mathf.RoundToInt(hit.collider.bounds.ClosestPoint(hit.point).z/4)*4); //grid placement
					previewRampObject.transform.position = placementSpot;
				}

				//changing material of preview
				if(ValidPlacementScript.validPlacement)
				{
					previewRampObject.GetComponent<MeshRenderer>().material = validMaterial;
				}
				else
				{
					previewRampObject.GetComponent<MeshRenderer>().material = invalidMaterial;
				}
			 }
			}

			//FLOOR
			else if(ItemSelect.angleOfSelected < 180)
			{

				if(Input.GetKeyDown(KeyCode.R))
				 {
					previewFloorObject.transform.Rotate(Vector3.down * -90);
					
				 }
				
			RaycastHit hit;
			if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, 9))
			 {
				previewFloorObject.SetActive(true);
				if(hit.collider.tag == "Floor")
				{
					Vector3 placementSpot = new Vector3(Mathf.RoundToInt(hit.collider.bounds.ClosestPoint(hit.point).x/4)*4,(Mathf.RoundToInt((hit.collider.bounds.ClosestPoint(hit.point).y)/4)*4)+0.2f, Mathf.RoundToInt(hit.collider.bounds.ClosestPoint(hit.point).z/4)*4); //grid placement
					previewFloorObject.transform.position = placementSpot;
				}
				if(hit.collider.tag == "BuiltItem")
				{
					Vector3 placementSpot = new Vector3(Mathf.RoundToInt(hit.collider.bounds.ClosestPoint(hit.point).x/4)*4,(Mathf.RoundToInt(hit.collider.bounds.ClosestPoint(hit.point).y/4)*4)+2, Mathf.RoundToInt(hit.collider.bounds.ClosestPoint(hit.point).z/4)*4); //grid placement
					previewFloorObject.transform.position = placementSpot;
					Debug.Log("test");
					
				}

				if(ValidPlacementScript.validPlacement)
				{
					previewFloorObject.GetComponent<MeshRenderer>().material = validMaterial;
				}
				else
				{
					previewFloorObject.GetComponent<MeshRenderer>().material = invalidMaterial;
				}
			 }
			}

			}


		}

		
		

