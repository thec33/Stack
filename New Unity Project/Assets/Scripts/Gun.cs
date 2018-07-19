
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour {

	//variables
	public float damage = 10f;
	public float range = 100f;
	public float impactForce = 1000f;
	public float fireRate = 15f;
	public float reloadSpeed = 2f;
	public int magSize = 30;
	private int bulletsLeft = 30;
	public int reserveAmmo = 120;

	public bool autoMatic = false;

	private bool reloading = false;

	//objects
	public Camera fpsCam;
	
	public ParticleSystem muzzleFlash;

	public GameObject impactEffect;

	private float nextTimeToFire = 0f;

	public TextMeshProUGUI ammoCurrentText;
	public TextMeshProUGUI ammoReserveText;
	public TextMeshProUGUI reloadText;
	
	

	// Use this for initialization
	void Start () {
		
		bulletsLeft = magSize;
		reloadText.gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {

		ammoCurrentText.text = bulletsLeft.ToString();
		ammoReserveText.text = reserveAmmo.ToString();
		

		if(bulletsLeft > 0)
		{

			if(bulletsLeft < magSize / 6)
			{
				reloadText.gameObject.SetActive(true);
			}
			
		if(autoMatic == true)
		{
			if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
			{
				nextTimeToFire = Time.time + 1f/fireRate;
				Shoot();
			}
		}
		else
		{
			if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
			{
				nextTimeToFire = Time.time + 1f/fireRate;
				Shoot();
			}
		}
			if(Input.GetKeyDown(KeyCode.R))
				{	
					Reload();
				}
		
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				Reload();
			}
		}
		
		
			
			
		ammoCurrentText.text = bulletsLeft.ToString();
		ammoReserveText.text = reserveAmmo.ToString();
			
		

	}

	void Shoot()
		{
			
			bulletsLeft--;
			muzzleFlash.Play();
			RaycastHit hit;
			//if we hit something
			 if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, 9))
			 {
				 Debug.Log(hit.transform.name);
				 

				 Target target = hit.transform.GetComponent<Target>();
				 if(target != null)
				 {
					 target.TakeDamage(damage);
				 }

				 if(hit.rigidbody != null)
				 {
					 hit.rigidbody.AddForce(-hit.normal * impactForce);
					 
				 }

				//deleting the particle effects
				 GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
				 Destroy(impactGO, 0.5f);
			 }
		}
		void Reload()
		{
			if(magSize != bulletsLeft)
			{
			if(reserveAmmo > magSize)
			{
				transform.position = Vector3.Lerp(transform.position, transform.position-(Vector3.up), reloadSpeed/2);
				reserveAmmo += bulletsLeft;
				bulletsLeft = magSize;
				reloadText.gameObject.SetActive(false);
				reserveAmmo -= magSize;
				transform.position = Vector3.Lerp(transform.position, transform.position+(Vector3.up), reloadSpeed/2);
			}
			else if(reserveAmmo < magSize && reserveAmmo > 0)
			{
				transform.position = Vector3.Lerp(transform.position, transform.position-(Vector3.up), reloadSpeed/2);
				bulletsLeft = reserveAmmo;
				reserveAmmo = 0;
				transform.position = Vector3.Lerp(transform.position, transform.position-(Vector3.up), reloadSpeed/2);

				if(bulletsLeft < magSize / 6)
				{
					reloadText.gameObject.SetActive(true);
				}
				else{
					reloadText.gameObject.SetActive(false);
				}
			}
		}
		}
}
