using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayBtn : MonoBehaviour
{
    private UIDocument _main;
    private UISounds _sound;

    [SerializeField] private UIDocument _settings;
    [SerializeField] private UIDocument _keyBinding;

    private Button _btnPlay;
    private Button _btnSettings;
    private Button _btnKeyBinding;
    private Button _quit;

    private VisualElement _menuContainer;
    private VisualElement _settingsContainer;
    private VisualElement _keyContainer;

    private void Awake()
    {
        UnityEngine.Cursor.visible = true;
    }

    void Start()
    {
        _main = GetComponent<UIDocument>();
        _sound = GetComponent<UISounds>();

        _btnPlay = SetupButton("PlayBtn", PlayGame);
        _btnSettings = SetupButton("SettingBtn", OpenSettings);
        _btnKeyBinding = SetupButton("KeyButton", OpenKeyBinding);
        _quit = SetupButton("QuitBtn", Quit);

        _menuContainer = _main.rootVisualElement.Q("MenuContainer");
        _settingsContainer = _settings.rootVisualElement.Q("SettingsContainer");
        _keyContainer = _keyBinding.rootVisualElement.Q("KeyContainer");
    }

    private Button SetupButton(string name, System.Action clickAction)
    {
        var button = _main.rootVisualElement.Q<Button>(name);
        button.clicked += clickAction;
        return button;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscapeKey();
        }
    }

    private void HandleEscapeKey()
    {
        if (_settingsContainer.style.display == DisplayStyle.Flex)
        {
            ToggleVisibility(_menuContainer, _settingsContainer);
        }
        else if (_keyContainer.style.display == DisplayStyle.Flex)
        {
            ToggleVisibility(_menuContainer, _keyContainer);
        }
    }

    private void ToggleVisibility(VisualElement show, VisualElement hide)
    {
        show.style.display = DisplayStyle.Flex;
        hide.style.display = DisplayStyle.None;
    }

    public void PlayGame()
    {
        StartCoroutine(_sound.PlaySoundAndWaitThenAction(() =>
        {
            SceneManager.LoadScene("SampleScene");
        }));
    }

    public void OpenSettings()
    {
        PlayButtonClickSound();
        ToggleVisibility(_settingsContainer, _menuContainer);
    }

    public void OpenKeyBinding()
    {
        PlayButtonClickSound();
        ToggleVisibility(_keyContainer, _menuContainer);
    }

    public void Quit()
    {
        PlayButtonClickSound();
        Application.Quit();
    }

    private void PlayButtonClickSound()
    {
        _sound._buttonClickSound.Play();
    }
}
