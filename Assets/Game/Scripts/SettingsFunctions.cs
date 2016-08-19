using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SettingsFunctions : MonoBehaviour {

    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject cameraChange;
    [SerializeField]
    private GameObject controlChange;
    [SerializeField]
    private GameObject DPad;


    public float PlayerVelocity { get; private set; }
    private static SettingsFunctions instance;
    public static SettingsFunctions Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<SettingsFunctions>();

            return instance;
        }

    }

    public GameObject GetDpad()
    {
        return DPad != null ? DPad : GameObject.Find("DPad");
    }
    public void Return(Canvas canvas)
	{
        gameObject.GetComponent<Canvas>().enabled = false;

        GameButtons.Instance.settingsActive = false;
        Time.timeScale = 1f;

    }
	public void ChangeControl()
	{
        //Change control 1 Dpad / 0 Touch
        if(Convert.ToBoolean(PlayerPrefs.GetInt("Control")))
        {
            controlChange.GetComponentInChildren<Text>().text = "Control: Touch";
            PlayerPrefs.SetInt("Control", 0);
            SetControls();
        } 
        else
        {
            controlChange.GetComponentInChildren<Text>().text = "Control: Dpad";
            PlayerPrefs.SetInt("Control", 1);
            SetControls();
        }
		
	}
    public void ChangeCamera()
    {
        // Camera 1 - angled/ 0 - top
        if (Convert.ToBoolean(PlayerPrefs.GetInt("Camera")))
        {
            cameraChange.GetComponentInChildren<Text>().text = "Camera: Top";
            PlayerPrefs.SetInt("Camera", 0);
        }
        else
        {
            cameraChange.GetComponentInChildren<Text>().text = "Camera: Angled";
            PlayerPrefs.SetInt("Camera", 1);
        }

        SetCamera();
	}
    private void SetCamera()
    {
        if (SceneManager.GetActiveScene().name.Equals("Game"))
        {
            if (Convert.ToBoolean(PlayerPrefs.GetInt("Camera")))
            {
                mainCamera.transform.position = new Vector3(5.5f, 35f, -10f);
                mainCamera.transform.rotation = Quaternion.Euler(61.5f, 0f, 0f);
            }
            else
            {
                mainCamera.transform.position = new Vector3(5.5f, 41.5f, 10f);
                mainCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            }
        }
    }
    private void SetControls()
    {
        if (SceneManager.GetActiveScene().name.Equals("Game"))
        {
            bool onOff = Convert.ToBoolean(PlayerPrefs.GetInt("Control"));
            PlayerVelocity = onOff ? 0.25f : 0.025f;
            DPad.SetActive(onOff);
        }

    }

    void Awake()
	{

        if (!PlayerPrefs.HasKey("Control"))
            PlayerPrefs.SetInt("Control", 1);
        if (!PlayerPrefs.HasKey("Camera"))
            PlayerPrefs.SetInt("Camera", 1);

        cameraChange.GetComponentInChildren<Text>().text =
            Convert.ToBoolean(PlayerPrefs.GetInt("Camera")) ? 
            "Camera: Angled" : "Camera: Top";

        controlChange.GetComponentInChildren<Text>().text =
            Convert.ToBoolean(PlayerPrefs.GetInt("Control")) ? 
            "Control: Dpad" : "Control: Touch";

        SetControls();
        SetCamera();
    }
}
