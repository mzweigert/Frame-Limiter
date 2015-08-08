using UnityEngine;
using System.Collections;


public class CoinPhysic : ObjectsSpawnScript {
	

	
	
	// Use this for initialization
	void Start () 
	{
		this.init();
		
		this.checkCollisionPosition();
		renderer.enabled = true;		
		this.addStruct(transform);

		StartCoroutine(this.WaitAndDestroy(5f, "Coin"));

	}

	void OnTriggerEnter(Collider collider)
	{
		
		if(collider.gameObject.name =="Ball") 
		{
			
			var score = GameObject.Find("Score").GetComponent<ScoreScript>();
			score.addScore();
			score.CanSpawnCoin= true;
			//GameObject.Find("Score").GetComponent<ScoreScript>().CanSpawnCoin = true;
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
