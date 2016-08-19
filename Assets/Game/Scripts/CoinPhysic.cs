using UnityEngine;
using System.Collections;

public class CoinPhysic : ObjectsSpawnScript {
	
	// Use this for initialization
	void Start () 
	{
		init();
		checkCollisionPosition();
		GetComponent<Renderer>().enabled = true;		
		addStruct(transform);
		StartCoroutine(WaitAndDestroy(5f, "Coin"));
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.name =="Ball") 
		{
            ObjectManager.Instance.addScore();
            ObjectManager.Instance.SetSpawning("Coin");
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
