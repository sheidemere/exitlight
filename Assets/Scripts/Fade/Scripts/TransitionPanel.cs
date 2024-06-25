using UnityEngine;
using UnityEngine.UIElements;

namespace AlpaSunFade
{
	internal class TransitionPanel : MonoBehaviour
	{
		[Header("Properties")]
		[SerializeField] private Color transitionColor;

		private VisualElement _transitionPanel;

		private void OnEnable()
		{
			_transitionPanel = GetComponent<UIDocument>().rootVisualElement.Q("Container");

			_transitionPanel.style.opacity = 0;

			_transitionPanel.style.backgroundColor = new Color(transitionColor.r, transitionColor.g, transitionColor.b, 1);
		}


		internal void StartTransition(bool fadeToDark, float waitDuration, float fadeDuration)
		{
			if(fadeToDark)
			{
				_transitionPanel.style.opacity = 0;
				StartCoroutine(StaticCoroutines.LerpVisualElementOpacity(_transitionPanel, 0, 1, fadeDuration, waitDuration));
			}
			else
			{
				_transitionPanel.style.opacity = 1;
				StartCoroutine(StaticCoroutines.LerpVisualElementOpacity(_transitionPanel, 1, 0, fadeDuration, waitDuration));
			}
		}
	}
}