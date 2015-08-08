using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
	int durability = 2;

	// Use this for initialization
	void Start () {
		gameObject.GetComponentInParent<CubeScript>().gameObject.renderer.material.mainTexture = gameObject.GetComponentInParent<CubeScript>().spriteFirst;	
	}
	void OnCollisionEnter(Collision collision)
	{
		
		if(collision.gameObject.name =="Ball") 
		{
			
			
			--durability;
			
			if(durability == 1)
				gameObject.GetComponentInParent<CubeScript>().gameObject.renderer.material.mainTexture = gameObject.GetComponentInParent<CubeScript>().spriteSecond;	
			else if(durability == 0)	
			{
				gameObject.GetComponentInParent<CubeScript>().removeStruct();
//				var score = GameObject.Find("Score").GetComponent<ScoreScript>();
//				score.CanSpawnCube= true;
				Destroy(gameObject.GetComponentInParent<CubeScript>().gameObject);
				
			}	
			
			
		}
		
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
