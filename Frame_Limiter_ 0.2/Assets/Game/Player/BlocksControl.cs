using UnityEngine;
using System.Collections;


public class BlocksControl : MonoBehaviour
{
	
	public float PlayerVelocity;
	public Vector3 PlayerPosition;
	public GameObject Terrain;
	public GameObject DPad;
	


	// Use this for initialization
	private static BlocksControl instance;
	
	
	public static BlocksControl Instance
	{
		get 
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<BlocksControl> ();
			
			return BlocksControl.instance;
		}
		
	}
	
	void Start () 
	{
		PlayerPosition = gameObject.transform.position;
		PlayerVelocity = 2f;
	
			
	}
	void BoundsBlockZ()
	{
		if (PlayerPosition.z > (Terrain.collider.bounds.size.z - transform.localScale.y - transform.localScale.x/2))
		{
			
			PlayerPosition.z = (Terrain.collider.bounds.size.z - transform.localScale.y - transform.localScale.x/2);
		}
		else if (PlayerPosition.z < transform.localScale.y + transform.localScale.x/2)
		{
			
			PlayerPosition.z = transform.localScale.y + transform.localScale.x/2;
		}
	}
	void BoundsBlockX()
	{
		if (PlayerPosition.x > (Terrain.collider.bounds.size.x - transform.localScale.y - transform.localScale.x / 2)) 
		{
			
			PlayerPosition.x = (Terrain.collider.bounds.size.x - transform.localScale.y - transform.localScale.x / 2);
		} 
		else if (PlayerPosition.x < transform.localScale.y + transform.localScale.x / 2) 
		{
			
			PlayerPosition.x = transform.localScale.y + transform.localScale.x / 2;
		}
	}
	// Update is called once per frame
	void Update () 
	{
		
		
		
			
			if(!OpenSettings.Instance.paused && !OpenSettings.Instance.settingsActive)
			{
				
				if(gameObject.name == "Block1" || gameObject.name == "Block2")
				{
					if(Input.GetKey(KeyCode.UpArrow))
					{
						PlayerPosition.z += 1* PlayerVelocity;
					
					}
				
					else if(Input.GetKey(KeyCode.DownArrow))
					{
						PlayerPosition.z -= 1*PlayerVelocity;
						
					}
					PlayerPosition.z += (DPad.GetComponentInChildren<ArrowControl>().direction.x * PlayerVelocity );
					BoundsBlockZ();
				}
				
					
				else if(gameObject.name == "Block3" || gameObject.name == "Block4")	
				{
					if(Input.GetKey(KeyCode.LeftArrow))
					{
						PlayerPosition.x -= 1* PlayerVelocity;
						
					}
					
					else if(Input.GetKey(KeyCode.RightArrow))
					{
						PlayerPosition.x += 1*PlayerVelocity;
					}	
					PlayerPosition.x += (DPad.GetComponentInChildren<ArrowControl>().direction.y * PlayerVelocity );
					BoundsBlockX(); 
				}
				
			}
		
		
		
		
		gameObject.transform.position = PlayerPosition;
		
		
		
	}
}

