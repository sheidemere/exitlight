using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static SettingsScript;

public class HUDWork : MonoBehaviour
{
    private UIDocument _hud;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Health _hp;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Label _keyLabel;
    private Label _hp1Label;
    private Label _hp2Label;
    private Label _hp3Label;

    void Start()
    {
        _hud = GetComponent<UIDocument>();

        _keyLabel = _hud.rootVisualElement.Q<Label>("keys");
        _hp1Label = _hud.rootVisualElement.Q<Label>("hp1");
        _hp2Label = _hud.rootVisualElement.Q<Label>("hp2");
        _hp3Label = _hud.rootVisualElement.Q<Label>("hp3");

        UpdateKeyCount();
        UpdateHealthDisplay();
    }

    void Update()
    {
        if (_inventory._keyCount > 0)
        {
            UpdateKeyCount();
        }

        UpdateHealthDisplay();

        HandleInput();
    }

    private void UpdateKeyCount()
    {
        _keyLabel.text = $"x  {_inventory._keyCount}";
    }

    private void UpdateHealthDisplay()
    {
        _hp1Label.style.display = _hp._health < 3 ? DisplayStyle.None : DisplayStyle.Flex;
        _hp2Label.style.display = _hp._health < 2 ? DisplayStyle.None : DisplayStyle.Flex;
        _hp3Label.style.display = _hp._health < 1 ? DisplayStyle.None : DisplayStyle.Flex;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKey(KeyCode.C))
        {
            _spriteRenderer.material.color = Color.yellow;
        }
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
}
