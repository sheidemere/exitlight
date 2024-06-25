using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AlpaSunFade
{
    internal class InputSystem : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] TransitionPanel transitionPanelScript;

        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        public void Fade()
        {
            bool fadeOut = true;
            float startValue = 0;
            float fadeDuration = 3;

            transitionPanelScript.StartTransition(fadeOut, startValue, fadeDuration);
        }
    }
}
