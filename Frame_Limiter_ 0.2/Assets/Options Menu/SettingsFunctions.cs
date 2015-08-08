using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsFunctions : MonoBehaviour {
	
	private bool CamSwitch = false;

	public static bool CameraTop = false;
	public static bool CameraAngled = true;

	/*private static SettingsFunctions instance;

	public static SettingsFunctions Instance
	{
		get 
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<SettingsFunctions> ();

			return SettingsFunctions.instance;
		}

	}*/

	public void Return(Canvas canvas)
	{

		canvas.enabled = false;

		OpenSettings.Instance.settingsActive = false;

		if (!OpenSettings.Instance.paused) 
		{
			Time.timeScale = 1;
		}
			


	}
	public void Control()
	{
	
		
	}
	public void ChangeCamera()
	{
		if(CamSwitch)
		{
			
			CameraTop = false;
			CameraAngled = true;
			
			GameObject.Find("CameraChange").GetComponentInChildren<Text>().text = "Camera: Angled";
			
			CamSwitch=!CamSwitch; 
			
			
		}
		else
		{
			
			CameraTop = true;
			CameraAngled = false;
			
			GameObject.Find("CameraChange").GetComponentInChildren<Text>().text = "Camera: Top";
			CamSwitch=!CamSwitch; 
		
		}
	}

	void Awake()
	{
		
		
		
		  
		
		
		if (CameraAngled)
		{
			GameObject.Find ("CameraChange").GetComponentInChildren<Text> ().text = "Camera: Angled";
			
		} 
		else if(CameraTop)
		{
			GameObject.Find ("CameraChange").GetComponentInChildren<Text> ().text = "Camera: Top";
			CamSwitch=true; 
		}

	}

}
