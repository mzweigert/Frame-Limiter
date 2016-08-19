using UnityEngine;
using System.Collections;

public class LifeUpPhysic : ObjectsSpawnScript {
	
	// Use this for initialization
	void Start () 
	{
		this.init();
		this.checkCollisionPosition();
		StartCoroutine(this.WaitAndDestroy(5f, "LifeUp"));
		if(this.repeatCount !=0)
			this.addStruct(transform);
		
	
	}
	void OnTriggerEnter(Collider collider)
	{
		
		if(collider.gameObject.name =="Ball") 
		{
            ObjectManager.Instance.setLifes(1);
            ObjectManager.Instance.SetSpawning("LifeUp");
            this.removeStruct();
			Destroy(gameObject);
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		this.boundsBlock(1f);
	}
}	