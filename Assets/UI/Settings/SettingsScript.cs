using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsScript : MonoBehaviour
{
    private UIDocument _settings;
    private Resolution[] _resolutions;
    private int _currentResolutionIndex = 0;

    public UISounds sound;

    private Button _btnResolution;
    private Button _btnVSync;
    private bool _vsyncEnabled;
    private Button _btnWindow;
    private bool _isWindowed;
    private Button _btnFPSLock;
    private bool _fpsLockEnabled;
    private Button _btnFPSMonitor;
    private bool _showFPS;

    private const int TargetFPS = 60;

    void Start()
    {
        _settings = GetComponent<UIDocument>();
        sound = GetComponent<UISounds>();
        _resolutions = Screen.resolutions;

        _btnResolution = SetupButton("btnRes", $"{Screen.width} x {Screen.height}", ChangeResolution);
        _btnFPSLock = SetupButton("btnFPSLock", "FPS LOCK ON", ToggleFPSLock);
        _btnFPSMonitor = SetupButton("btnFPSMonitor", "FPS MONITOR OFF", ToggleFPSMonitor);
        _btnVSync = SetupButton("btnVSync", "VSYNC OFF", ToggleVSync);
        _btnWindow = SetupButton("btnWindow", "FULLSCREEN", ToggleWindowMode);
    }

    private Button SetupButton(string name, string initialText, System.Action clickAction)
    {
        var button = _settings.rootVisualElement.Q<Button>(name);
        button.text = initialText;
        button.clicked += clickAction;
        return button;
    }

    void OnGUI()
    {
        if (GlobalData.ShowGUI)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                normal = { textColor = Color.white },
                fontSize = 18
            };

            float fps = 1.0f / Time.deltaTime;
            GUI.Label(new Rect(10, 10, 100, 20), "FPS: " + Mathf.Round(fps), style);
        }
    }

    private void ChangeResolution()
    {
        PlayButtonClickSound();
        _currentResolutionIndex = (_currentResolutionIndex + 1) % _resolutions.Length;
        Resolution nextResolution = _resolutions[_currentResolutionIndex];
        Screen.SetResolution(nextResolution.width, nextResolution.height, Screen.fullScreen);
        _btnResolution.text = $"{nextResolution.width} x {nextResolution.height}";
    }

    private void ToggleFPSMonitor()
    {
        PlayButtonClickSound();
        _showFPS = !_showFPS;
        _btnFPSMonitor.text = _showFPS ? "FPS MONITOR ON" : "FPS MONITOR OFF";
        GlobalData.ShowGUI = _showFPS;
    }

    private void ToggleFPSLock()
    {
        PlayButtonClickSound();
        _fpsLockEnabled = !_fpsLockEnabled;
        Application.targetFrameRate = _fpsLockEnabled ? TargetFPS : -1;
        _btnFPSLock.text = _fpsLockEnabled ? "FPS LOCK ON" : "FPS LOCK OFF";
    }

    private void ToggleWindowMode()
    {
        PlayButtonClickSound();
        _isWindowed = !_isWindowed;
        Screen.fullScreenMode = _isWindowed ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;
        _btnWindow.text = _isWindowed ? "WINDOWED" : "FULLSCREEN";
    }

    private void ToggleVSync()
    {
        PlayButtonClickSound();
        _vsyncEnabled = !_vsyncEnabled;
        QualitySettings.vSyncCount = _vsyncEnabled ? 1 : 0;
        _btnVSync.text = _vsyncEnabled ? "VSYNC ON" : "VSYNC OFF";
    }

    private void PlayButtonClickSound()
    {
        sound._buttonClickSound.Play();
    }

    public static class GlobalData
    {
        public static bool ShowGUI { get; set; }
    }
}
