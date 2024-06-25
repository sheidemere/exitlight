using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _open;

    [SerializeField] Inventory _inventory;

    [SerializeField] PlaySoundComponent _playSound;

    private bool _isopen = false;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && (_inventory._keyCount == 1) && _isopen == false)
        {
            _playSound.Play("chest");
            _spriteRenderer.sprite = _open;
            _isopen = true;
        }
    }
}
