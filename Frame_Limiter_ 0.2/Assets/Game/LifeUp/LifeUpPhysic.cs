using UnityEngine;
using System.Collections;

public class LifeUpPhysic : ObjectsSpawnScript {
	
	// Use this for initialization
	void Start () 
	{
		this.init();
		this.checkCollisionPosition();
		StartCoroutine(this.WaitAndDestroy(5f, "LifeUp"));
		//transform.position = new Vector3 (transform.position.x, 100f, transform.position.z);
		if(this.repeatCount !=0)
			this.addStruct(transform);
		
	
	}
	void OnTriggerEnter(Collider collider)
	{
		
		if(collider.gameObject.name =="Ball") 
		{
			
			GameObject.Find("Score").GetComponent<ScoreScript>().setLifes(1);
			GameObject.Find("Score").GetComponent<ScoreScript>().CanSpawnLifeUp = true;
			this.removeStruct();
			Destroy(gameObject);
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		this.boundsBlock(3.5f);
	}
}	