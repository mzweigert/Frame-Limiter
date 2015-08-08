using UnityEngine;
using System.Collections;


public abstract class ObjectsSpawnScript : MonoBehaviour {
	

	protected float Timer;
	protected Vector3 Velocity = new Vector3 (0f, -200f, 0f);
	protected float TerrainScaleX, TerrainScaleZ;
	protected float BlockWidth;
	protected bool canSpawn;
	private positionsStruct temp;
	protected int repeatCount;
	
	public virtual void init()
	{
	
		BlockWidth = GameObject.Find ("Block1").transform.localScale.y;
		
		TerrainScaleX = GameObject.Find ("Terrain").collider.bounds.size.x;
		
		TerrainScaleZ = GameObject.Find ("Terrain").collider.bounds.size.z;
		
		rigidbody.velocity = Velocity;
		
	}
	public virtual void checkCollisionPosition()
	{
		int i;
		bool cantSpawn;
		float rightBound ;
		float leftBound;
		float upBound;
		float downBound ;
		
			//TEST 
			
		repeatCount = 10;
		
		do
		{
			
			cantSpawn = false;
			transform.position = new Vector3 (Random.Range (BlockWidth + transform.localScale.x, TerrainScaleX - (BlockWidth + transform.localScale.x)), 200f , Random.Range (BlockWidth + transform.localScale.x, TerrainScaleZ - (BlockWidth + transform.localScale.x)));
			
			leftBound = transform.position.x - transform.localScale.x / 2;
			rightBound = transform.position.x + transform.localScale.x / 2;
			downBound = transform.position.z - transform.localScale.x / 2;
			upBound = transform.position.z + transform.localScale.x / 2;
			
			repeatCount --;

			for(i=0; i< ScoreScript.Instance.positionsList.Count; i++)
			{
				float xEnd = ScoreScript.Instance.positionsList[i].xEnd;
				float xStart = ScoreScript.Instance.positionsList[i].xStart;
				float zStart = ScoreScript.Instance.positionsList[i].zStart;
				float zEnd = ScoreScript.Instance.positionsList[i].zEnd;

				if((transform.localScale.x/2 + (xEnd - xStart)/2) > Vector2.Distance(new Vector2(xStart + (xEnd - xStart)/2 , zStart + (zEnd - zStart)/2 ), new Vector2(transform.position.x, transform.position.z)))
				{ 
					cantSpawn = true;			
					break;
				}
				else if(((leftBound >= xStart && leftBound <= xEnd ) || (rightBound >= xStart && rightBound <= xEnd )) && ((downBound >= zStart && downBound<=zEnd) ||(upBound >= zStart && upBound<=zEnd)))
				{
					cantSpawn = true;			
					break;
				}	
			}
			

			if(repeatCount == 0 )
			{
				Destroy(gameObject);
				break;
			}
			
			
		}while(cantSpawn);

		
		
	}
	public virtual void boundsBlock(float positionYBound)
	{
		if (gameObject.transform.position.y < positionYBound)
			transform.position = new Vector3 (transform.position.x, positionYBound, transform.position.z);
	}
	public virtual void addStruct(Transform trans)
	{
		ScoreScript.Instance.positions.xStart = trans.position.x - trans.localScale.x / 2;
		ScoreScript.Instance.positions.xEnd = trans.position.x + trans.localScale.x / 2;
		ScoreScript.Instance.positions.zStart = trans.position.z - trans.localScale.x / 2;
		ScoreScript.Instance.positions.zEnd = trans.position.z + trans.localScale.x / 2;
		
		temp = ScoreScript.Instance.positions;
		ScoreScript.Instance.positionsList.Add(ScoreScript.Instance.positions);
	}
	public virtual void removeStruct()
	{
		ScoreScript.Instance.positionsList.Remove(temp);
	}
	public virtual IEnumerator WaitAndDestroy(float time, string whichObject)
	{
		yield return new WaitForSeconds (time);
		
		time = 1f;
		
		for (int i=0; i<12; i++) 
		{
			time-=0.08f;
			if(i%2==0)
			{
				if(whichObject.Equals("Multiplier") || whichObject.Equals("LifeUp"))
				{
					Renderer[] rs = GetComponentsInChildren<Renderer>();
					foreach(Renderer r in rs)
					
						r.enabled = false;
				}
				else
					renderer.enabled = false;
				yield return new WaitForSeconds (time);
				
			}
			else
			{
				if(whichObject.Equals("Multiplier") || whichObject.Equals("LifeUp"))
				{
					Renderer[] rs = GetComponentsInChildren<Renderer>();
					foreach(Renderer r in rs)
						
						r.enabled = true;
				}
				else
					renderer.enabled = true;

				yield return new WaitForSeconds (time);
			}
			
		}

        switch(whichObject)
        {
            case "Coin":
                GameObject.Find("Score").GetComponent<ScoreScript>().CanSpawnCoin = true;
                break;
            case "Multiplier":
                GameObject.Find("Score").GetComponent<ScoreScript>().CanSpawnMultiplier = true;
                break;
            case "SpeedUp":
                GameObject.Find("Score").GetComponent<ScoreScript>().CanSpawnSpeedUp = true;
                break;
            case "LifeUp":
                GameObject.Find("Score").GetComponent<ScoreScript>().CanSpawnLifeUp = true;
                break;

        }

		removeStruct();
		Destroy (gameObject);	
		
	}


	
}
