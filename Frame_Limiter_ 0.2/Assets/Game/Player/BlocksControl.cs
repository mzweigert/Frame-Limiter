using UnityEngine;
using System.Collections;


public class BlocksControl : MonoBehaviour
{
	
	public float PlayerVelocity;
	public Vector3 PlayerPosition;
	public GameObject Terrain;
	public GameObject DPad;
    public Ray ray;
    public bool DpadOnOff;


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
		PlayerVelocity = 2.5f;
	
			
	}
	void BoundsBlockZ(Transform objTransform)
	{
        if (objTransform.position.z > (Terrain.collider.bounds.size.z - objTransform.localScale.y - objTransform.localScale.x/2))
		{
            PlayerPosition.z = (Terrain.collider.bounds.size.z - objTransform.localScale.y - objTransform.localScale.x/2);
            objTransform.transform.position = PlayerPosition;
		}
		else if (PlayerPosition.z < transform.localScale.y + transform.localScale.x/2)
		{
			
            PlayerPosition.z = objTransform.localScale.y + objTransform.localScale.x/2;
            objTransform.transform.position = PlayerPosition;
		}
       
	}
    void BoundsBlockX(Transform objTransform)
	{


        if (objTransform.position.x > (Terrain.collider.bounds.size.x - objTransform.localScale.y - objTransform.localScale.x / 2)) 
		{
            PlayerPosition.x = (Terrain.collider.bounds.size.x - objTransform.localScale.y - objTransform.localScale.x / 2);
            objTransform.transform.position = PlayerPosition;
        } 
        else if (objTransform.position.x < objTransform.localScale.y + objTransform.localScale.x / 2) 
		{
            PlayerPosition.x = objTransform.localScale.y + objTransform.localScale.x / 2;
            objTransform.transform.position = PlayerPosition;
		}
       


	}
	// Update is called once per frame
	void Update () 
	{

        if(SettingsFunctions.DpadControl)
        {
            if(DpadOnOff)
            {
                DPad.SetActive(true);    
                DpadOnOff=false;
            }
            
            if(!OpenSettings.Instance.paused && !OpenSettings.Instance.settingsActive)
            {
               
                if(gameObject.name == "Block1" || gameObject.name == "Block2")
                {
                 
                    if(Input.GetKey(KeyCode.UpArrow) || DPad.GetComponentInChildren<ArrowControl>().moveUp)
                    {
            
                        PlayerPosition = gameObject.transform.position;

                        PlayerPosition.z += (DPad.GetComponentInChildren<ArrowControl>().direction.x * PlayerVelocity );
                       
                        gameObject.transform.position = PlayerPosition;

                        BoundsBlockZ(gameObject.transform);
                    }
                    
                    else if(Input.GetKey(KeyCode.DownArrow) || DPad.GetComponentInChildren<ArrowControl>().moveDown)
                    {
                        PlayerPosition = gameObject.transform.position;

                        PlayerPosition.z += (DPad.GetComponentInChildren<ArrowControl>().direction.x * PlayerVelocity );
                       
                        gameObject.transform.position = PlayerPosition;
                        BoundsBlockZ(gameObject.transform);

                    }
                    
                }
                else if(gameObject.name == "Block3" || gameObject.name == "Block4") 
                {

                    if(Input.GetKey(KeyCode.LeftArrow) || DPad.GetComponentInChildren<ArrowControl>().moveLeft)
                    {
                        PlayerPosition = gameObject.transform.position;

                        PlayerPosition.x += (DPad.GetComponentInChildren<ArrowControl>().direction.y * PlayerVelocity );

                        gameObject.transform.position = PlayerPosition;

                        BoundsBlockX(gameObject.transform); 

                    }
                    
                    else if(Input.GetKey(KeyCode.RightArrow) || DPad.GetComponentInChildren<ArrowControl>().moveRight)
                    {
                        PlayerPosition = gameObject.transform.position;

                        PlayerPosition.x += (DPad.GetComponentInChildren<ArrowControl>().direction.y * PlayerVelocity );
                      
                        gameObject.transform.position = PlayerPosition;

                        BoundsBlockX(gameObject.transform);

                    }   
                    
                }
            }
            
        }
        else if(SettingsFunctions.TouchControl)
        {
            if(!DpadOnOff)
            {
                PlayerVelocity = 2f;
                DPad.SetActive(false);                 
                DpadOnOff=true;
                
                
            }
            if(!OpenSettings.Instance.paused && !OpenSettings.Instance.settingsActive)
            {
                foreach(Touch t in Input.touches)
                {
               
                    ray = Camera.main.ScreenPointToRay(t.position);
                    RaycastHit hit;
                    
                    if (Physics.Raycast(ray, out hit))
                    {

                        if(hit.collider.name == "Block1TouchCollider" || hit.collider.name == "Block2TouchCollider")
                        {
                            PlayerPosition = GameObject.Find("Block1").transform.position;
                            GameObject.Find("Block1").transform.position = new Vector3(PlayerPosition.x, PlayerPosition.y , PlayerPosition.z + t.deltaPosition.y*0.2f);
                            BoundsBlockZ(GameObject.Find("Block1").transform);

                            PlayerPosition = GameObject.Find("Block2").transform.position;
                            GameObject.Find("Block2").transform.position = new Vector3(PlayerPosition.x, PlayerPosition.y , PlayerPosition.z + t.deltaPosition.y*0.2f);
                            BoundsBlockZ(GameObject.Find("Block2").transform);


                        }
                        else if(hit.collider.name == "Block3TouchCollider" || hit.collider.name == "Block4TouchCollider") 
                        {
                            PlayerPosition = GameObject.Find("Block3").transform.position;
                            GameObject.Find("Block3").transform.position = new Vector3(PlayerPosition.x + t.deltaPosition.x*0.2f, PlayerPosition.y , PlayerPosition.z );
                            BoundsBlockX(GameObject.Find("Block3").transform);

                            PlayerPosition = GameObject.Find("Block4").transform.position;
                            GameObject.Find("Block4").transform.position = new Vector3(PlayerPosition.x + t.deltaPosition.x*0.2f, PlayerPosition.y , PlayerPosition.z );
                            BoundsBlockX(GameObject.Find("Block4").transform);
                    
                        }
                        
                    }
                }
            }
        }
        
    }
}

