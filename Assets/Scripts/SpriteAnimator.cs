using UnityEngine;
using UnityEngine.Events;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private int _frameRate;
    [SerializeField] private bool _loop;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private UnityEvent _onComplete;


    private SpriteRenderer _spriteRenderer;
    private float _secondsPerFrame;
    public int _currentSpriteIndex;
    private float _nextFrameTime;

    private bool _isPlaying = true;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _secondsPerFrame = 1f / _frameRate;
        _nextFrameTime = Time.time + _secondsPerFrame;
    }

    void Update()
    {
        if (!_isPlaying || Time.time < _nextFrameTime)
        {
            return;
        }

        

        _spriteRenderer.sprite = _sprites[_currentSpriteIndex];

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
            _spriteRenderer.sprite = _sprites[0];
        }
    }
}
