using UnityEngine;
using System.Collections;


public class SpeedUpPhysic : ObjectsSpawnScript {

	

	// Use this for initialization
	void Start () 
	{

		byte ballColor = (byte) Random.Range (1, 3);
	
		if (ballColor == (byte)1) 
		{
			gameObject.GetComponent<Renderer>().material.color = Color.red;
			gameObject.GetComponentInChildren<ParticleSystem> ().startColor = Color.red;
		} 
		else if (ballColor == (byte)2) 
		{
			gameObject.GetComponent<Renderer>().material.color = Color.cyan;
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
			if(gameObject.GetComponent<Renderer>().material.color == Color.red)
                BallPhysic.Instance.setSpeed(3f / 4f);
			else if(gameObject.GetComponent<Renderer>().material.color == Color.cyan)
                BallPhysic.Instance.setSpeed(4f / 3f);

            ObjectManager.Instance.SetSpawning("SpeedUp");
            BallPhysic.Instance.setVelocityBall();

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
