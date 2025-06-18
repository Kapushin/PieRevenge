using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settings;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string firtsScene = "1_Near House Timeline";

    // �������� ������� UI
    private static VisualElement visuals;

    // ������ ����
    private Button _play;
    private Button _load;
    private Button _options;
    private Button _exit;

    private void Start()
    {
        string window = PlayerPrefs.GetString("WindowMode");
        if (window == "windowed" || window == null)
        {
            Screen.fullScreen = false;
        }
        
        else
        {
            Screen.fullScreen = true;
        }

        audioMixer.SetFloat("Effect", PlayerPrefs.GetFloat("SoundVolume"));
        audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("MusicVolume"));
    }

    void OnEnable()
    {
        EventManager.SendCursorOn();

        visuals = GetComponent<UIDocument>().rootVisualElement;
        _play = visuals.Q<Button>("Play");
        _play.clicked += PlayButton;
        _load = visuals.Q<Button>("Load");
        _load.clicked += LoadButton;
        _options = visuals.Q<Button>("Settings");
        _options.clicked += OptionsButton;
        _exit = visuals.Q<Button>("Exit");
        _exit.clicked += ExitButton;
    }

    void PlayButton()
    {
        EventManager.SendCursorOff();
        GetComponent<SaveLoadSystem>().SavesClearing();
        LoadingData.sceneToLoad = firtsScene;
        SceneManager.LoadScene("LoadingScreen");

    }

    void LoadButton()
    {
        EventManager.SendCursorOff();
        GetComponent<SaveLoadSystem>().LoadGame();
    }

    void OptionsButton()
    {
        settings.SetActive(true);
        gameObject.SetActive(false);
    }

    void ExitButton()
    {
        Application.Quit();
    }
}
