using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpringJump : MonoBehaviour
{

    [SerializeField] Rigidbody2D _rigidbody;

    [SerializeField] Hero _hero;

    [SerializeField] PlaySoundComponent _playSound;

    public void Start()
    {
        _rigidbody.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playSound.Play("jump");
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _hero._jumpingPower * 2);
        }
    }

}
