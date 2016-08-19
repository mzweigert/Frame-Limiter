using UnityEngine;
using System.Collections;
using System;


public class BlocksControl : MonoBehaviour
{

    [SerializeField]
    private GameObject Terrain;
    private GameObject DPad;

    private Ray ray;
    private Vector3 PlayerPosition;
    private float vlcty;
    private GameObject block;
    // Use this for initialization
	
	void Start () 
	{
		PlayerPosition = gameObject.transform.position;
  
        DPad = SettingsFunctions.Instance.GetDpad();
    }
    void BoundsBlockZ(Vector3 newPosition, Transform objTransform)
	{
        if (newPosition.z > (Terrain.transform.localScale.z - objTransform.localScale.y - objTransform.localScale.x / 2f))
            objTransform.position = new Vector3(objTransform.position.x, objTransform.position.y, Terrain.transform.localScale.z - objTransform.localScale.y - objTransform.localScale.x / 2f);
        else if (newPosition.z < objTransform.localScale.y + objTransform.localScale.x / 2f)
            objTransform.position = new Vector3(objTransform.position.x, objTransform.position.y, objTransform.localScale.y + objTransform.localScale.x / 2f);
        else
            objTransform.position = newPosition;
    }
    void BoundsBlockX(Vector3 newPosition, Transform objTransform)
	{
        if (newPosition.x > (Terrain.transform.localScale.x - objTransform.localScale.y - objTransform.localScale.x / 2f))
            objTransform.position = new Vector3(Terrain.transform.localScale.x - objTransform.localScale.y - objTransform.localScale.x / 2f, objTransform.position.y, objTransform.position.z);
        else if (newPosition.x < objTransform.localScale.y + objTransform.localScale.x / 2f)
            objTransform.position = new Vector3(objTransform.localScale.y + objTransform.localScale.x / 2f, objTransform.position.y, objTransform.position.z);
        else
            objTransform.position = newPosition;
    }

    // Update is called once per frame
    void Update () 
	{

        if (GameButtons.Instance.settingsActive || Time.timeScale == 0f)
            return;

        vlcty = SettingsFunctions.Instance.PlayerVelocity;
        if (Convert.ToBoolean(PlayerPrefs.GetInt("Control")))
        {
          
            if (gameObject.name == "Block1" || gameObject.name == "Block2")
            {
                if (DPad.GetComponentInChildren<ArrowControl>().moveUp)
                {
                    PlayerPosition = gameObject.transform.position;
                    PlayerPosition.z += (DPad.GetComponentInChildren<ArrowControl>().direction.x * vlcty);
                    BoundsBlockZ(PlayerPosition, gameObject.transform);
                }
                else if (DPad.GetComponentInChildren<ArrowControl>().moveDown)
                {
                    PlayerPosition = gameObject.transform.position;
                    PlayerPosition.z += (DPad.GetComponentInChildren<ArrowControl>().direction.x * vlcty);
                    BoundsBlockZ(PlayerPosition, gameObject.transform);
                }
            }
            else if (gameObject.name == "Block3" || gameObject.name == "Block4")
            {
                if (DPad.GetComponentInChildren<ArrowControl>().moveLeft)
                {
                    PlayerPosition = gameObject.transform.position;
                    PlayerPosition.x += (DPad.GetComponentInChildren<ArrowControl>().direction.y * vlcty);
                    BoundsBlockX(PlayerPosition, gameObject.transform);
                }
                else if (DPad.GetComponentInChildren<ArrowControl>().moveRight)
                {
                    PlayerPosition = gameObject.transform.position;
                    PlayerPosition.x += (DPad.GetComponentInChildren<ArrowControl>().direction.y * vlcty);
                    BoundsBlockX(PlayerPosition, gameObject.transform);
                }
            }
        }
        else
        {

            foreach (Touch t in Input.touches)
            {

                ray = Camera.main.ScreenPointToRay(t.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.name == "Block1TouchCollider" || hit.collider.name == "Block2TouchCollider")
                    {
                        block = GameObject.Find("Block1");
                        PlayerPosition = block.transform.position;
                        BoundsBlockZ(new Vector3(PlayerPosition.x, PlayerPosition.y, PlayerPosition.z + t.deltaPosition.y * vlcty), block.transform);

                        block = GameObject.Find("Block2");
                        PlayerPosition = block.transform.position;
                        BoundsBlockZ(new Vector3(PlayerPosition.x, PlayerPosition.y, PlayerPosition.z + t.deltaPosition.y * vlcty), block.transform);
                    }
                    else if (hit.collider.name == "Block3TouchCollider" || hit.collider.name == "Block4TouchCollider")
                    {

                        block = GameObject.Find("Block3");
                        PlayerPosition = block.transform.position;
                        BoundsBlockX(new Vector3(PlayerPosition.x + t.deltaPosition.x * vlcty, PlayerPosition.y, PlayerPosition.z), block.transform);

                        block = GameObject.Find("Block4");
                        PlayerPosition = block.transform.position;
                        BoundsBlockX(new Vector3(PlayerPosition.x + t.deltaPosition.x * vlcty, PlayerPosition.y, PlayerPosition.z), block.transform);
                    }

                }
            }
           
        }


    }
}

