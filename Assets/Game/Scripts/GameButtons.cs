using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameButtons : MonoBehaviour {
    [SerializeField]
	private RectTransform canvasSettings;
    [SerializeField]
    private RectTransform playPauseGame;

    public bool settingsActive, play;
	public Sprite PlayTexture, PauseTexture;

	private static GameButtons instance;
	
	public static GameButtons Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<GameButtons> ();
			
			return GameButtons.instance;
		}
		
	}

    public void Play()
    {
        if (play)
        {
            Time.timeScale = 0f;
            play = false;
            playPauseGame.GetComponent<Image>().sprite = PlayTexture;
    
        }
        else
        {
            Time.timeScale = 1f;
            play = true;
            playPauseGame.GetComponent<Image>().sprite = PauseTexture;
            BallPhysic.Instance.PlayGame();
        }
    }
    public void BallOutOfBounds()
    {
        playPauseGame.GetComponent<Image>().sprite = PlayTexture;
        play = false;
    }
	public void OpenSettings()
	{
        Time.timeScale = 0f;
        settingsActive = true;
           
        canvasSettings.GetComponent<Canvas>().enabled = true;
    }
    void Start()
    {
        settingsActive = false;
        play = false;
    }
}
