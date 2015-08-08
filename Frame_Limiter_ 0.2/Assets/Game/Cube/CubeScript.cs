using UnityEngine;
using System.Collections;


public class CubeScript : ObjectsSpawnScript {
	
	
	bool freezeOnce = true;
	public UnityEngine.Texture spriteFirst, spriteSecond;
	
	
	// Use this for initialization
	void Start () 
	{
		this.init();
		
		this.checkCollisionPosition();
		
		if(this.repeatCount !=0)
			this.addStruct(transform);

		renderer.enabled = true;
		
	
		
	}
	
	
	
	
	
	
	
	// Update is called once per frame
	void Update () 
	{
		if (gameObject.transform.position.y < (transform.localScale.z / 2) && freezeOnce)
		{
			transform.position = new Vector3 (transform.position.x, transform.localScale.z / 2, transform.position.z);
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			freezeOnce = !freezeOnce;
		}
			
	}
	
	
}
