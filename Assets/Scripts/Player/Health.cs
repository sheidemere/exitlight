using AlpaSunFade;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{

    [SerializeField] InputSystem _youdied;
    [SerializeField] PlaySoundComponent _playSound;

    public int _health = 3;

    private bool _dead = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (_health <= 0 && _dead == false)
        {
            Dead();
        }

        if (Input.anyKeyDown && _dead == true)
        {
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1;
        }
    }

    public void Damage(int damage)
    {
        _health -= damage;
    }
    public void Dead() 
    {
        _youdied.Fade();
        _playSound.Play("dead");
        Time.timeScale = 0;
        _dead = true;
    }
}
