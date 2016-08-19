using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {

    [SerializeField]
    private RectTransform hiScore;

	public void ChangeToScene (string SceneToChange)
	{
		SceneManager.LoadScene(SceneToChange);
	}	
	public void QuitApp()
	{
		Application.Quit ();
	}

    void Awake()
    {
        if (hiScore == null)
            return;

        string nameScene = SceneManager.GetActiveScene().name;
        Text hsText = hiScore.GetComponent<Text>();

        if (nameScene.Equals("MainMenu"))
        {
            if (PlayerPrefs.GetInt("HiScore") > 0)
                hsText.text = "HiScore: " + PlayerPrefs.GetInt("HiScore");
        }
        else if (nameScene.Equals("GameOver") && ObjectManager.Instance != null)
        {
            int score = ObjectManager.Instance.score;
            if (score > 0)
            {
                if (score > PlayerPrefs.GetInt("HiScore"))
                {
                    hsText.text = PlayerPrefs.HasKey("HiScore") ?
                        "You have beat your record. Score: " + score :
                        "Score: " + score;

                    PlayerPrefs.SetInt("HiScore", score);
                }
                else
                    hsText.text = "You have'nt beat your record. Try again. HiScore: " + PlayerPrefs.GetInt("HiScore");
            }
            else
                hsText.text = "Try again. Score: 0";
        }

    }
}
