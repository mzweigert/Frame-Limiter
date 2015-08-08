using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public void ChangeToScene (string SceneToChange)
	{
		Application.LoadLevel(SceneToChange);
	}	
	public void QuitApp()
	{
		Application.Quit ();

	}
}
