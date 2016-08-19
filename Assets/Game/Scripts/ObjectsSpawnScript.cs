using UnityEngine;
using System.Collections;


public abstract class ObjectsSpawnScript : MonoBehaviour {
	
	protected float Timer;
	protected float TerrainScaleX, TerrainScaleZ;
	protected float BlockWidth;
	protected bool canSpawn;
	private positionsStruct temp;
	protected int repeatCount;
	
	public virtual void init()
	{

        Vector3 terrainBounds = GameObject.Find("Terrain").GetComponent<Collider>().bounds.size;
        BlockWidth = GameObject.Find ("Block1").transform.localScale.y;
		TerrainScaleX = terrainBounds.x;
		TerrainScaleZ = terrainBounds.z;
		
	}
	public virtual void checkCollisionPosition()
	{
		int i;
		bool cantSpawn;
		float rightBound ;
		float leftBound;
		float upBound;
		float downBound ;
		
		repeatCount = 10;

		do
		{
			cantSpawn = false;
			transform.position = new Vector3 (Random.Range (BlockWidth + transform.localScale.x, TerrainScaleX - (BlockWidth + transform.localScale.x)), 20f , Random.Range (BlockWidth + transform.localScale.x, TerrainScaleZ - (BlockWidth + transform.localScale.x)));
			
			leftBound = transform.position.x - transform.localScale.x / 2;
			rightBound = transform.position.x + transform.localScale.x / 2;
			downBound = transform.position.z - transform.localScale.x / 2;
			upBound = transform.position.z + transform.localScale.x / 2;
			
			repeatCount --;

			for(i=0; i< ObjectManager.Instance.positionsList.Count; i++)
			{
				float xEnd = ObjectManager.Instance.positionsList[i].xEnd;
				float xStart = ObjectManager.Instance.positionsList[i].xStart;
				float zStart = ObjectManager.Instance.positionsList[i].zStart;
				float zEnd = ObjectManager.Instance.positionsList[i].zEnd;

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
			
			
		} while(cantSpawn);

		
		
	}
	public virtual void boundsBlock(float positionYBound)
	{
        Vector3 pos = transform.position;

        if (pos.y <= positionYBound)
            transform.position = new Vector3(pos.x, positionYBound, pos.z);

	}
	public virtual void addStruct(Transform trans)
	{
        ObjectManager.Instance.positions.xStart = trans.position.x - trans.localScale.x / 2;
		ObjectManager.Instance.positions.xEnd = trans.position.x + trans.localScale.x / 2;
		ObjectManager.Instance.positions.zStart = trans.position.z - trans.localScale.x / 2;
		ObjectManager.Instance.positions.zEnd = trans.position.z + trans.localScale.x / 2;
		
		temp = ObjectManager.Instance.positions;
        ObjectManager.Instance.positionsList.Add(ObjectManager.Instance.positions);
	}
	public virtual void removeStruct()
	{
        ObjectManager.Instance.positionsList.Remove(temp);
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
					GetComponent<Renderer>().enabled = false;
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
					GetComponent<Renderer>().enabled = true;

				yield return new WaitForSeconds (time);
			}
			
		}
        ObjectManager.Instance.SetSpawning(whichObject);

		removeStruct();
		Destroy (gameObject);	
		
	}	
}
