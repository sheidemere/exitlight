using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements;

public class UIAnimator : MonoBehaviour
{
    [SerializeField] private int _frameRate;
    [SerializeField] private bool _loop;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private UnityEvent _onComplete;


    private UIDocument _hud;
    private float _secondsPerFrame;
    public int _currentSpriteIndex;
    private float _nextFrameTime;

    private bool _isPlaying = true;

    private Label _hp;
    private Label _hp2;
    private Label _hp3;

    void Start()
    {
        _hud = GetComponent<UIDocument>();
        _secondsPerFrame = 1f / _frameRate;
        _nextFrameTime = Time.time + _secondsPerFrame;

        _hp = _hud.rootVisualElement.Q<Label>("hp1");
        _hp2 = _hud.rootVisualElement.Q<Label>("hp2");
        _hp3 = _hud.rootVisualElement.Q<Label>("hp3");
    }

    void Update()
    {
        if (!_isPlaying || Time.time < _nextFrameTime)
        {
            return;
        }

        StyleBackground newBackground = new StyleBackground(_sprites[_currentSpriteIndex]);


        _hp.style.backgroundImage = newBackground;
        _hp2.style.backgroundImage = newBackground;
        _hp3.style.backgroundImage = newBackground;


        if (_currentSpriteIndex >= _sprites.Length - 1)
        {
            if (_loop)
            {
                _currentSpriteIndex = 0;
            }
            else
            {
                _isPlaying = false;
                _onComplete?.Invoke();
                return;
            }
        }
        else
        {
            _currentSpriteIndex++;
        }

        _nextFrameTime += _secondsPerFrame;
    }

    public void SetFirstSpriteAndStopAnimation(bool stop)
    {
        if (stop == true)
        {
            StyleBackground newBackground = new StyleBackground(_sprites[0]);

            _hp.style.backgroundImage = newBackground;
            _hp2.style.backgroundImage = newBackground;
            _hp3.style.backgroundImage = newBackground;
        }
    }
}
