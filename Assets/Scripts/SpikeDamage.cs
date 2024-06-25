using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    [SerializeField] Health _current;
    [SerializeField] PlaySoundComponent _playSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playSound.Play("damage");
            _current.Damage(1);
        }
    }


}
