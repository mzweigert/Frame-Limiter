using UnityEngine;
using System.Collections;

public class x2Physic : ObjectsSpawnScript {


	
		// Use this for initialization
		void Start () 
		{
			this.init();
		
			this.checkCollisionPosition();

			StartCoroutine(this.WaitAndDestroy(5f, "Multiplier"));
			
			if(this.repeatCount !=0)
				this.addStruct(transform);
		}
		void OnTriggerEnter(Collider collider)
		{
		
			if(collider.gameObject.name =="Ball") 
			{
				
				GameObject.Find("Score").GetComponent<ScoreScript>().setMultiplier(2);
				GameObject.Find("Score").GetComponent<ScoreScript>().CanSpawnMultiplier = true;
				this.removeStruct();
				Destroy(gameObject);
			}
		
		}
		
		// Update is called once per frame
		void Update () 
		{
			
			this.boundsBlock(7f);
		}
}	