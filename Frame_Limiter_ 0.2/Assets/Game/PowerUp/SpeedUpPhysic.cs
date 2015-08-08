using UnityEngine;
using System.Collections;


public class SpeedUpPhysic : ObjectsSpawnScript {

	

	// Use this for initialization
	void Start () 
	{


		byte ballColor = (byte) Random.Range (1, 3);
	
		if (ballColor == (byte)1) 
		{

			gameObject.renderer.material.color = Color.red;
			gameObject.GetComponentInChildren<ParticleSystem> ().startColor = Color.red;
		} 
		else if (ballColor == (byte)2) 
		{
			gameObject.renderer.material.color = Color.cyan;
			gameObject.GetComponentInChildren<ParticleSystem> ().startColor = Color.cyan;
		}

		this.init();
		this.checkCollisionPosition();
		
		this.addStruct(transform);

		StartCoroutine(this.WaitAndDestroy(5f, "SpeedUp"));

	}

	void OnTriggerEnter(Collider collider)
	{
		
		if(collider.gameObject.name =="Ball") 
		{
			if(gameObject.renderer.material.color == Color.red)
				GameObject.Find("Ball").GetComponent<BallPhysic>().setSpeed(3f/4f);
			else if(gameObject.renderer.material.color == Color.cyan)
				GameObject.Find("Ball").GetComponent<BallPhysic>().setSpeed(4f/3f);

			GameObject.Find("Score").GetComponent<ScoreScript>().CanSpawnSpeedUp = true;
			GameObject.Find("Ball").GetComponent<BallPhysic>().setVelocityBall();
			
			this.removeStruct();
			Destroy(gameObject);
		}
		
	}
	// Update is called once per frame
	void Update () 
	{
		

		this.boundsBlock((transform.localScale.z / 2));
	}


}
