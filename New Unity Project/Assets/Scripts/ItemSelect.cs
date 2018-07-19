using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour {

	public List<MenuButton> buttons = new List<MenuButton>();
	private Vector2 Mouseposition;
	private Vector2 fromVector2M = new Vector2(0.5f,1.0f);
	private Vector2 centerCircle = new Vector2(0.5f,0.5f);
	private Vector2 toVector2M;

	public float angle;
	public static float angleOfSelected;
	private GameObject firstOption;
	private GameObject secondOption;
	
	// Use this for initialization
	void Start ()
	{
			angleOfSelected = 0;
			firstOption = GameObject.Find("selectRamp");
			secondOption = GameObject.Find("selectFloor");
			firstOption.SetActive(false);
			secondOption.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	 {
		if(Input.GetKey(KeyCode.X))
		{
			GetCurrentMenuItem();
		}
		else
		{
			firstOption.SetActive(false);
			secondOption.SetActive(false);
		}
		
		if(Input.GetKeyUp(KeyCode.X))
		{
			SetCurrentMenuItem();
		}
		
		
	}

	public void GetCurrentMenuItem()
	{
		firstOption.SetActive(true);
		secondOption.SetActive(true);
		Mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		toVector2M = new Vector2(Mouseposition.x/Screen.width, Mouseposition.y/Screen.height);

		angle = (Mathf.Atan2(fromVector2M.y-centerCircle.y, fromVector2M.x-centerCircle.x)) - Mathf.Atan2(toVector2M.y-centerCircle.y, toVector2M.x-centerCircle.x) * Mathf.Rad2Deg;

		if(angle < 0)
		{
			angle += 360;
		}
		//angle divided by number of degress per button

		if(angle > 180)
		{
			firstOption.GetComponent<Image>().color = new Color32(0, 184, 184, 255);
			secondOption.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
		}
		else if(angle < 180)
		{
			firstOption.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
			secondOption.GetComponent<Image>().color = new Color32(0, 184, 184, 255);
		}
		

	}

	public void SetCurrentMenuItem()
	{
		angleOfSelected = angle;
		
	}

}

[System.Serializable]
public class MenuButton
{
	public string name;
	public Image buttonImage;
	public Color NormalColor = Color.white;
	public Color HighlightedColor = Color.grey;
}