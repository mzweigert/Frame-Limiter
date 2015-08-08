using UnityEngine;
using System.Collections;

public class OpenSettings : MonoBehaviour {

	public Canvas canvasSettings;
	public GUITexture Settings, PausePlay;
	public bool settingsActive, paused;
	public Texture PlayTexture, PauseTexture;

	private static OpenSettings instance;
	
	public static OpenSettings Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<OpenSettings> ();
			
			return OpenSettings.instance;
		}
		
	}

	

	public void Open()
	{
		

		if (Input.touchCount >0) 
		{
			Touch t = Input.GetTouch (0);
			
			if (Settings.HitTest (t.position, Camera.main) && settingsActive==false) 
			{
				canvasSettings.enabled = true;
				settingsActive = true;
				Time.timeScale = 0;
			
			}
			else if(PausePlay.HitTest (t.position, Camera.main)&& (settingsActive==false )&& (t.phase == TouchPhase.Began))
			{
				
				if(BallPhysic.Instance.ballIsActive)
				{
						
						if(!paused)
						{
							Time.timeScale = 0;
							paused = true;
							PausePlay.texture = PlayTexture;
					
						}
						else 
						{
							Time.timeScale = 1;
							paused = false;
							PausePlay.texture = PauseTexture;

						}

				}
			}
		
		}
		if(Input.GetKey(KeyCode.P)&& (settingsActive==false ))
		{
		
			if(BallPhysic.Instance.ballIsActive)
			{
				
				if(!paused)
				{
					Time.timeScale = 0;
					paused = true;
					PausePlay.texture = PlayTexture;
					
				}
				else 
				{
					Time.timeScale = 1;
					paused = false;
					PausePlay.texture = PauseTexture;
					
				}
				
			}
		}
			

	}
	void Start()
	{
		settingsActive = false;
	}
	void Update()
	{
		Open();
	}
}
