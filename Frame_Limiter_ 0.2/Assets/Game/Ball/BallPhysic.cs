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

	//public GameObject Score;
	public ScoreScript scoreScript;

	public float Speed = 100f;
	private float Timer;
	private int WhichBlock = 1; 

		
	float TerrainX, TerrainZ, BlockWidth;

	private static BallPhysic instance;
		
	public static BallPhysic Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<BallPhysic> ();
				
			return BallPhysic.instance;
		}
			
	}


	void Start () 
	{

		//	gameObject.renderer.material.color = Color.blue;
			
       	TerrainX = Terrain.collider.bounds.size.x;
		TerrainZ = Terrain.collider.bounds.size.z;
		BlockWidth = playerObject.transform.localScale.y;
	
		ballIsActive = false;
		OpenSettings.Instance.paused = false;
	//		rigidbody.velocity = new Vector3(0f, 0f, -Speed);
			
		ballPosition = gameObject.transform.position;
			
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
		Speed=100f;
	
	}



	public void setVelocityBall()
	{
		if (rigidbody.isKinematic == false) 
		{
			switch(WhichBlock)
			{
				case 1:
					rigidbody.velocity = new Vector3 (-getSpeed (), 0f, rigidbody.velocity.z);
					break;
				case 2:
					rigidbody.velocity = new Vector3 (getSpeed (), 0f, rigidbody.velocity.z);
					break;
				case 3:
					rigidbody.velocity = new Vector3 (rigidbody.velocity.x, 0f, -getSpeed ());
					break;
				case 4:
					rigidbody.velocity = new Vector3 (rigidbody.velocity.x, 0f, getSpeed ());
					break;

			}
				
		}
	}

	void OnCollisionEnter(Collision collision) 
	{
		audio.PlayOneShot(bounceSound);
        float z,x ;

		switch(collision.gameObject.name)
        {
            case "Block1":
                WhichBlock=1;                
                z = (transform.position.z - collision.transform.position.z) / collision.collider.gameObject.transform.localScale.x;
                dir = new Vector3(-1f, 0f, z*1.25f );              
                rigidbody.velocity = dir * Speed ;
                break;

            case "Block2":
                WhichBlock=2;
                z = (transform.position.z - collision.transform.position.z) / collision.collider.gameObject.transform.localScale.x;
                dir = new Vector3(1f, 0f , z*1.25f  );           
                rigidbody.velocity =dir * Speed ;
                break;

            case "Block3":
                WhichBlock=3;
                x = (transform.position.x - collision.transform.position.x) / collision.collider.gameObject.transform.localScale.x;
                dir = new Vector3(x*1.25f, 0f , -1f );                
                rigidbody.velocity = dir * Speed ;
                break;
            case "Block4":
                WhichBlock=4;
                x = (transform.position.x - collision.transform.position.x) / collision.collider.gameObject.transform.localScale.x;
                dir = new Vector3(x*1.25f, 0f , 1f );                
                rigidbody.velocity = dir * Speed ;  
                break;

        
        }		
		
	}
	void OnTriggerEnter(Collider collider)
	{
		switch (collider.gameObject.name)
        {
            case "Coin":
                audio.PlayOneShot(pickUpCoin);
                break;
            case "LifeUp":
                audio.PlayOneShot(pickUpLife);
                break;
            
            case "x2":
                audio.PlayOneShot(pickUpX2);
                break;
            
            case "SpeedUp":
                audio.PlayOneShot(pickUpSpeedUp);
                break;
        }
			
	}
	// Update is called once per frame
	void Update () 
	{
		
		
		
		if (Speed != 100f) 
		{
			Timer += Time.deltaTime;
			if(Timer >= 5.0f)
			{
				SetNormalSpeed();
				Timer = 0f;		
			}
		}
		
		if(Input.touchCount>0)
		{
			Touch t = Input.GetTouch(0);
			
			if(GameObject.Find("PlayPauseGame").GetComponent<GUITexture>().HitTest(t.position)&& (OpenSettings.Instance.settingsActive==false ))
			{	
				if (!ballIsActive)
				{
					rigidbody.isKinematic = false;
					// add a force
					rigidbody.velocity = new Vector3(100f , 0f , 0f) ;
					OpenSettings.Instance.paused = false;
					// set ball active
					ballIsActive = !ballIsActive;
					scoreScript.CanSpawnObjects = true;
					OpenSettings.Instance.PausePlay.texture = OpenSettings.Instance.PauseTexture;
					Time.timeScale = 1;
					//gameActive = false;
				}
			}
		}
		else if(Input.GetKey(KeyCode.P)&& (OpenSettings.Instance.settingsActive==false ))
		{
			
			if (!ballIsActive)
			{
				rigidbody.isKinematic = false;
				// add a force
				rigidbody.velocity = new Vector3(100f , 0f , 0f) ;
				OpenSettings.Instance.paused = false;
				// set ball active
				ballIsActive = !ballIsActive;
				scoreScript.CanSpawnObjects = true;
				OpenSettings.Instance.PausePlay.texture = OpenSettings.Instance.PauseTexture;
				Time.timeScale = 1;
				//gameActive = false;
			}
		}
		
		
		
		if (!ballIsActive && playerObject != null)
		{
			
			// get and use the player position
			ballPosition.z = playerObject.transform.position.z;
			// apply player X position to the ball
			transform.position = ballPosition;
			//	OpenSettings.Instance.paused = true;
			//gameActive = false;
			OpenSettings.Instance.PausePlay.texture = OpenSettings.Instance.PlayTexture;
			scoreScript.CanSpawnObjects = false;
			
		}
		
		
		// Check if ball falls
		if (ballIsActive &&(transform.position.z>(TerrainZ - (BlockWidth/2f) - (transform.localScale.x/2f) ))|| (transform.position.z < ( BlockWidth/2) - (transform.localScale.x/2f)  ) || (transform.position.x > (TerrainX - (BlockWidth/2) - (transform.localScale.x/2f) )) || (transform.position.x < (BlockWidth/2) - (transform.localScale.x/2f) ))
		{
			
			ballIsActive = !ballIsActive;
			ballPosition.z = playerObject.transform.position.z;
			ScoreScript.Instance.setLifes(-1);
			ballPosition.x = (playerObject.transform.localScale.y+(transform.localScale.z/2));
			transform.position = ballPosition;
			//gameActive = false;
			rigidbody.isKinematic = true;
			scoreScript.CanSpawnObjects = false;
			
		}
		
	}
}