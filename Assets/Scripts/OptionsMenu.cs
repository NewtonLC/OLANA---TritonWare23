using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    //Different Menu UIs to switch between
    public GameObject Options_MenuUI;
    public GameObject Other_MenuUI;
    public GameObject Options_ControlsUI;
    public GameObject Options_VideoUI;
    public GameObject Options_AudioUI;

    //Tracks what is currently open
    private static bool Options_Opened;
    private static bool Controls_Opened;
    private static bool Video_Opened;
    private static bool Audio_Opened;

    //Variable for Volume Control
    public AudioMixer audioMixer;

    //Variables for Resolution Option
    public TextMeshProUGUI resolutionText;
    private List<string> resolutionOptions;
    private int currentResIndex;
    Resolution[] resolutions;

    //Variables for Graphics Quality
    public TextMeshProUGUI graphicsText;
    private string[] qualityOptions;
    private int qualityIndex;
    
    void Start()
    {
        qualityOptions = QualitySettings.names;
        qualityIndex = 2;

        resolutions = Screen.resolutions;

        resolutionOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
                Debug.Log("Match Found: " + currentResIndex);
            }
        }

        resolutionText.SetText(Screen.currentResolution.width + " x " + Screen.currentResolution.height);
    }

    void Update()
    {
        //If ESC is pressed, exit current menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Options_Opened && !Controls_Opened && !Video_Opened && !Audio_Opened)
            {
                OptionsClose();
            }
            else if (Controls_Opened)
            {
                ControlsClose();
            }
            else if (Video_Opened)
            {
                VideoClose();
            }
            else if (Audio_Opened)
            {
                AudioClose();
            }
        }
    }

    public void OptionsOpen()
    {
        Options_Opened = true;
        Other_MenuUI.SetActive(false);
        Options_MenuUI.SetActive(true);
    }

    public void OptionsClose()
    {
        Options_Opened = false;
        Options_MenuUI.SetActive(false);
        Other_MenuUI.SetActive(true);
    }

    public void ControlsOpen()
    {
        Controls_Opened = true;
        Options_MenuUI.SetActive(false);
        Options_ControlsUI.SetActive(true);
    }

    public void ControlsClose()
    {
        Controls_Opened = false;
        Options_ControlsUI.SetActive(false);
        Options_MenuUI.SetActive(true);
    }

    public void VideoOpen()
    {
        Video_Opened = true;
        Options_VideoUI.SetActive(true);
        Options_MenuUI.SetActive(false);
    }

    public void VideoClose()
    {
        Video_Opened = false;
        Options_MenuUI.SetActive(true);
        Options_VideoUI.SetActive(false);
    }

    public void AudioOpen()
    {
        Audio_Opened = true;    
        Options_AudioUI.SetActive(true);
        Options_MenuUI.SetActive(false);
    }

    public void AudioClose()
    {
        Audio_Opened = false;
        Options_MenuUI.SetActive(true);
        Options_AudioUI.SetActive(false);
    }

    //TODO: Implement Music vs Master vs SFX Volume
    public void setVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    //Constantly changing Resolution bugs it out a little, makes button unresponsive
    public void nextResolution()
    {
        currentResIndex++;
        if (currentResIndex == resolutionOptions.Count)
        {
            currentResIndex--;
        }
        setResolution(currentResIndex);
    }

    //Constantly changing Resolution bugs it out a little, makes button unresponsive
    public void prevResolution()
    {
        currentResIndex--;
        if(currentResIndex < 0)
        {
            currentResIndex++;
        }
        setResolution(currentResIndex);
    }

    private void setResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width,resolution.height, Screen.fullScreen);
        resolutionText.SetText(resolution.width + " x " + resolution.height);
    }

    public void nextQuality()
    {
        qualityIndex++;
        if (qualityIndex == qualityOptions.Length)
        {
            qualityIndex--;
        }
        setQuality(qualityIndex);
    }

    public void prevQuality()
    {
        qualityIndex--;
        if(qualityIndex < 0)
        {
            qualityIndex++;
        }
        setQuality(qualityIndex);
    }

    private void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        graphicsText.SetText(qualityOptions[qualityIndex]);
    }

    public void setScreen(int screenIndex)
    {
        if(screenIndex == 1)
        {
            Screen.fullScreen = true;
        }
        if(screenIndex == 0)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        if(screenIndex == 2)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
