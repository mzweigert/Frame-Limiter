using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public struct positionsStruct
{
	public float xStart{ get; set; }
	public float xEnd{ get; set; }
	public float zStart{ get; set; }
	public float zEnd{ get; set; }
}
public class ObjectManager : MonoBehaviour 
{
    [SerializeField]
    private RectTransform lifesRT;
    [SerializeField]
    private RectTransform scoreRT;

    public int score { get; private set; }
	private int lifes;


	public List<positionsStruct> positionsList;
	public positionsStruct positions = new positionsStruct();

    [SerializeField]
	private Transform SpeedUpPrefab, x2Prefab, coinPrefab, lifeUpPrefab, cubePrefab;

	private  int multiplier = 1;
	float Timer = 0f;

	private int succesToSpawn;

    public bool CanSpawnObjects { get; set; }
    private bool CanSpawnCoin;
    private bool CanSpawnMultiplier;
    private bool CanSpawnSpeedUp;
    private bool CanSpawnLifeUp;


    private static ObjectManager instance;

	public static ObjectManager Instance
	{
		get 
		{
			if (instance == null)
				instance = FindObjectOfType<ObjectManager> ();
			
			return instance;
		}
		
	}

	public void addScore()
	{
		score += 20 * multiplier;
        scoreRT.GetComponent<Text>().text = "Score: " + score;
	}
	public void setMultiplier(int n)
	{
		multiplier *= n;
	}
	public void setLifes(int n)
	{
		lifes+=n;
        lifesRT.GetComponent<Text>().text = "Lifes: " + lifes;
    }
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
    public void SetSpawning(string whichObject)
    {
        switch (whichObject)
        {
            case "Coin":
                Instance.CanSpawnCoin = true;
                break;
            case "Multiplier":
                CanSpawnMultiplier = true;
                break;
            case "SpeedUp":
                CanSpawnSpeedUp = true;
                break;
            case "LifeUp":
                CanSpawnLifeUp = true;
                break;
        }
    }
    void SpawnObject()
	{

        if (!CanSpawnObjects)
            return;

        succesToSpawn = Random.Range(1, 100);

        if (succesToSpawn <= 15 && succesToSpawn > 10 && CanSpawnSpeedUp)
        {
            CreateNewSpeedUp();
            CanSpawnSpeedUp = false;
        }
        if (succesToSpawn <= 15 && succesToSpawn > 10 && CanSpawnMultiplier)
        {
            CreateNewx2();
            CanSpawnMultiplier = false;
        }
        if (succesToSpawn <= 85 && succesToSpawn > 80 && CanSpawnLifeUp)
        {
            CreateNewLifeUp();
            CanSpawnLifeUp = false;
        }
        if (CanSpawnCoin)
        {
            CanSpawnCoin = false;
            CreateNewCoin();
        }

       

    }
	void Start()
	{
        score = 0;
        lifes = 5;
        CanSpawnLifeUp = true;
        CanSpawnSpeedUp = true;
        CanSpawnCoin = true;
        CanSpawnMultiplier = true;
        CanSpawnObjects = false;

        positionsList = new List<positionsStruct>();
        InvokeRepeating("SpawnObject", 1, 1);

        DontDestroyOnLoad(gameObject);

    }

	void Update()
	{
        if (!SceneManager.GetActiveScene().name.Equals("Game"))
        {
            Destroy(gameObject);
            return;
        }
        

        if (lifes <= 0)
            SceneManager.LoadScene("GameOver");

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
