using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{

    public AudioSource _buttonClickSound;

    public IEnumerator PlaySoundAndWaitThenAction(System.Action action)
    {
        _buttonClickSound.Play();

        yield return new WaitForSeconds(_buttonClickSound.clip.length);

        action?.Invoke();
    }
}
