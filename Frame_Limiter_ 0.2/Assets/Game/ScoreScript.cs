using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public struct positionsStruct
{
	public float xStart{ get; set; }
	public float xEnd{ get; set; }
	public float zStart{ get; set; }
	public float zEnd{ get; set; }
}
public class ScoreScript : MonoBehaviour 
{
	public GUIStyle style;
	private int score = 0;
	private int lifes = 10;
	public Texture btnTexture;

	public List<positionsStruct> positionsList;
	public positionsStruct positions = new positionsStruct();

	public Transform SpeedUpPrefab, x2Prefab, coinPrefab, lifeUpPrefab, cubePrefab;

	private  int multiplier = 1;
	float Timer = 0f;

	private int succesToSpawn;
	
	public bool CanSpawnObjects;
	public bool CanSpawnCoin;
	public bool CanSpawnMultiplier;
	public bool CanSpawnSpeedUp;
	public bool CanSpawnLifeUp;
	//public bool CanSpawnCube;
	
	private static ScoreScript instance;

	public static ScoreScript Instance
	{
		get 
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<ScoreScript> ();
			
			return ScoreScript.instance;
		}
		
	}

	public void addScore()
	{
		score += 20 * multiplier;
	
	}
	public void setMultiplier(int n)
	{
		multiplier *= n;
		
	}
	public void setLifes(int n)
	{
		lifes+=n;
	}

	//------------------------------------------------------------------------------------------------------//


	void CreateNewSpeedUp()
	{
		
		var SpeedUp = Instantiate (SpeedUpPrefab);
		SpeedUp.name = "SpeedUp";
	}
	void CreateNewx2()
	{
		
		var SpeedUp = Instantiate (x2Prefab);
		SpeedUp.name = "x2";
	}
	void CreateNewCoin()
	{
		var newCoin = Instantiate(coinPrefab); 
		newCoin.name = "Coin";
		
		
	}
	void CreateNewLifeUp()
	{
		var newLifeUp = Instantiate(lifeUpPrefab); 
		newLifeUp.name = "LifeUp";
	}
    //NOT FINISHED YET
	/*void CreateNewCube()
	{
		var newCube = Instantiate(cubePrefab); 
		newCube.name = "Cube";
	}*/
	
	
	//------------------------------------------------------------------------------------------------------//


	void SpawnObject()
	{
		//print (CanSpawnObjects); 

		if (CanSpawnObjects==true)
		{
			succesToSpawn = Random.Range (1, 100);
			
			if (succesToSpawn <= 15 && succesToSpawn > 10 && CanSpawnSpeedUp==true )
			{
				
				CreateNewSpeedUp ();
				CanSpawnSpeedUp = false;

			} 
			if (succesToSpawn <= 15 && succesToSpawn > 10 && CanSpawnMultiplier==true) 
			{
				
				CreateNewx2 ();
				CanSpawnMultiplier = false;
			}
			
			if (succesToSpawn <= 85 && succesToSpawn > 80 && CanSpawnLifeUp==true) 
			{
				
				CreateNewLifeUp ();
				CanSpawnLifeUp = false;
			}
            //NOT FINISHED YET
            /*if (succesToSpawn <= 90 && succesToSpawn > 1 && CanSpawnCube==true) 
			{
				
				CreateNewCube ();
				CanSpawnCube = false;
			}*/
			
			//SPAWN FOR COIN
			if (CanSpawnCoin == true ) 
			{
				CanSpawnCoin = false;
				CreateNewCoin ();

			}
			
			

		}

		
	}
	void Start()
	{
//		CanSpawnCube = true;
		CanSpawnLifeUp = true;
		CanSpawnSpeedUp = true;
		CanSpawnCoin = true; 
		CanSpawnMultiplier = true;
		CanSpawnObjects = false;
	
		positionsList = new List<positionsStruct>();
		style.fontSize = 25;
		style.normal.textColor = Color.black;
		InvokeRepeating ("SpawnObject", 1, 1);
	

	}

	void OnGUI()
	{

		
		GUI.Label(new Rect(Screen.width-650, Screen.height-(Screen.height-90) , 80, 20), "Score: " + score.ToString(), style);
		GUI.Label(new Rect(Screen.width-650, Screen.height-(Screen.height-140) , 80, 20), "Lifes: " + lifes.ToString(), style);
		
	
	}

	void FixedUpdate()
	{
		if(lifes==0)
			Application.LoadLevel("GameOver");
	}

	

	void Update()
	{
	
		/*if(Input.GetKey(KeyCode.S))
		{
			ClearLog();
			int i = 0;
			while(i < ScoreScript.Instance.positionsList.Count)
			{
				
				print(ScoreScript.Instance.positionsList[i].xStart + " " +ScoreScript.Instance.positionsList[i].xEnd + " " +ScoreScript.Instance.positionsList[i].zStart + " " +ScoreScript.Instance.positionsList[i].zEnd + " ");
				i++;
			}
			CreateNewSpeedUp();			
			
		}*/

		if (multiplier >=2) 
		{
			Timer += Time.deltaTime;
			if (Timer >= 10.0f) 
			{
				Timer = 0f;
				multiplier = 1;
			}
		}
	}
}
