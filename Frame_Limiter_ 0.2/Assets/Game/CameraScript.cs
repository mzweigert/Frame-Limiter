using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

	private static CameraScript instance;

	public static CameraScript Instance
	{
		get 
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<CameraScript> ();
			
			return CameraScript.instance;
		}
		
	}
	
	// Use this for initialization
	void Start () 
	{
		

	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
			if(SettingsFunctions.CameraTop)
			{
				gameObject.transform.position = new Vector3(50f, 415f , 100f);
				gameObject.transform.rotation = Quaternion.Euler(90f,0f,0f);
			}
			else
			{
				gameObject.transform.position = new Vector3(55f, 350f , -110f);
				gameObject.transform.rotation = Quaternion.Euler(60f, 0f , 0f);
			}
		
		
	}
}
