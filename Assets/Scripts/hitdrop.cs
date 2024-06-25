using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class hitdrop : MonoBehaviour
{
    [SerializeField] public MonoBehaviour _stopAnimation;
    [SerializeField] public GameObject _drop;
    [SerializeField] PlaySoundComponent _playSound;
    [SerializeField] public SpriteAnimator _spriteAnimator;

    private bool _isUsed = false;

    [SerializeField] Inventory _inventory;


    public void Start()
    {
        var gameObject = GetComponent<GameObject>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isUsed)
        {
            _playSound.Play("drop");
            _isUsed = true;
            _drop.SetActive(true);
            StartCoroutine(Move(_drop));

            if (_stopAnimation != null)
            {
                _stopAnimation.enabled = false;
                _spriteAnimator.SetFirstSpriteAndStopAnimation(true);
            }
        }
    }


    IEnumerator Move(GameObject _drop)
    {
        float duration = 1.0f;
        Vector3 startPos = _drop.transform.position;
        Vector3 endPos = startPos + Vector3.up * 0.75f;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            _drop.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

}
