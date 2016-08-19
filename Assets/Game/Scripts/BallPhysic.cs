using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class BallPhysic : MonoBehaviour 
{
	public AudioClip bounceSound, pickUpCoin, pickUpLife, pickUpSpeedUp, pickUpX2;
	
   	public bool ballIsActive;
	//public Vector3 ballInitialForce;
	private Vector3 ballPosition;
	private static Vector3 dir;
	// GameObject
	public GameObject playerObject;
	public GameObject Terrain;

	private float Speed = 10f;
	private float Timer;
	private int WhichBlock = 1; 

		
	float TerrainX, TerrainZ, BlockWidth;

	private static BallPhysic instance;
		
	public static BallPhysic Instance
	{
		get
		{
			if (instance == null)
				instance = FindObjectOfType<BallPhysic>();
				
			return instance;
		}
			
	}


	void Start () 
	{
       	TerrainX = Terrain.GetComponent<Collider>().bounds.size.x;
		TerrainZ = Terrain.GetComponent<Collider>().bounds.size.z;
		BlockWidth = playerObject.transform.localScale.y;
	
		ballIsActive = false;
        GameButtons.Instance.play = false;
		ballPosition = gameObject.transform.position;
        Time.timeScale = 1f;
    }

	public float getSpeed ()
	{
		return Speed;
	}
	public void setSpeed(float n)
	{
		Speed*=n;

	}
	public void SetNormalSpeed()
	{
		Speed=10f;
	
	}


	public void setVelocityBall()
	{
		if (GetComponent<Rigidbody>().isKinematic == false) 
		{
			switch(WhichBlock)
			{
				case 1:
					GetComponent<Rigidbody>().velocity = new Vector3 (-getSpeed (), 0f, GetComponent<Rigidbody>().velocity.z);
					break;
				case 2:
					GetComponent<Rigidbody>().velocity = new Vector3 (getSpeed (), 0f, GetComponent<Rigidbody>().velocity.z);
					break;
				case 3:
					GetComponent<Rigidbody>().velocity = new Vector3 (GetComponent<Rigidbody>().velocity.x, 0f, -getSpeed ());
					break;
				case 4:
					GetComponent<Rigidbody>().velocity = new Vector3 (GetComponent<Rigidbody>().velocity.x, 0f, getSpeed ());
					break;
			}
				
		}
	}

	void OnCollisionEnter(Collision collision) 
	{
		GetComponent<AudioSource>().PlayOneShot(bounceSound);
        float z,x ;

		switch(collision.gameObject.name)
        {
            case "Block1":
                WhichBlock=1;                
                z = (transform.position.z - collision.transform.position.z) / collision.collider.gameObject.transform.localScale.x;
                dir = new Vector3(-1f, 0f, z*1.25f );              
                GetComponent<Rigidbody>().velocity = dir * Speed ;
                break;

            case "Block2":
                WhichBlock=2;
                z = (transform.position.z - collision.transform.position.z) / collision.collider.gameObject.transform.localScale.x;
                dir = new Vector3(1f, 0f , z*1.25f  );           
                GetComponent<Rigidbody>().velocity =dir * Speed ;
                break;

            case "Block3":
                WhichBlock=3;
                x = (transform.position.x - collision.transform.position.x) / collision.collider.gameObject.transform.localScale.x;
                dir = new Vector3(x*1.25f, 0f , -1f );                
                GetComponent<Rigidbody>().velocity = dir * Speed ;
                break;
            case "Block4":
                WhichBlock=4;
                x = (transform.position.x - collision.transform.position.x) / collision.collider.gameObject.transform.localScale.x;
                dir = new Vector3(x*1.25f, 0f , 1f );                
                GetComponent<Rigidbody>().velocity = dir * Speed ;  
                break;

        
        }		
		
	}
    public void PlayGame()
    {
        if (!ballIsActive)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce( new Vector3(Speed, 0f, 0f));
            GameButtons.Instance.play = true;
            ballIsActive = !ballIsActive;

            ObjectManager.Instance.CanSpawnObjects = true;
            Time.timeScale = 1f;
        }
    }
	void OnTriggerEnter(Collider collider)
	{
		switch (collider.gameObject.name)
        {
            case "Coin":
                GetComponent<AudioSource>().PlayOneShot(pickUpCoin);
                break;
            case "LifeUp":
                GetComponent<AudioSource>().PlayOneShot(pickUpLife);
                break;
            
            case "x2":
                GetComponent<AudioSource>().PlayOneShot(pickUpX2);
                break;
            
            case "SpeedUp":
                GetComponent<AudioSource>().PlayOneShot(pickUpSpeedUp);
                break;
        }
			
	}
	// Update is called once per frame
	void Update () 
	{
		
		if (Speed != 10f) 
		{
			Timer += Time.deltaTime;
			if(Timer >= 5.0f)
			{
				SetNormalSpeed();
				Timer = 0f;		
			}
		}
		
		if (!ballIsActive && playerObject != null)
		{
			ballPosition.z = playerObject.transform.position.z;
			transform.position = ballPosition;
            ObjectManager.Instance.CanSpawnObjects = false;
		}
		
		// Check if ball falls
		if (ballIsActive &&(transform.position.z>(TerrainZ - (BlockWidth/2f) - (transform.localScale.x/2f) ))|| (transform.position.z < ( BlockWidth/2) - (transform.localScale.x/2f)  ) || (transform.position.x > (TerrainX - (BlockWidth/2) - (transform.localScale.x/2f) )) || (transform.position.x < (BlockWidth/2) - (transform.localScale.x/2f) ))
		{
			ballIsActive = !ballIsActive;
			ballPosition.z = playerObject.transform.position.z;
            ObjectManager.Instance.setLifes(-1);
			ballPosition.x = (playerObject.transform.localScale.y+(transform.localScale.z/2));
			transform.position = ballPosition;
			//gameActive = false;
			GetComponent<Rigidbody>().isKinematic = true;
            ObjectManager.Instance.CanSpawnObjects = false;
            GameButtons.Instance.BallOutOfBounds();
			
		}
		
	}
}