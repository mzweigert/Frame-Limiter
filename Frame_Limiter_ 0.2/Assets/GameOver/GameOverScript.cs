using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public Vector3 speed;
	// Use this for initialization
	void Start () 
	{
		speed = new Vector3(0f, -15f, 0f);
		gameObject.rigidbody.velocity = speed;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
