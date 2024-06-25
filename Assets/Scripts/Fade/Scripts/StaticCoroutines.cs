using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace AlpaSunFade
{
	internal static class StaticCoroutines
	{
		internal static IEnumerator LockControls(float duration, PlayerInput playerInput)
		{
			var actionMapStart = playerInput.currentActionMap;
			playerInput.currentActionMap = playerInput.actions.FindActionMap(nameOrId: "LockControls");

			yield return new WaitForSeconds(duration);

			playerInput.currentActionMap = actionMapStart;

			yield return null;
		}

		internal static IEnumerator LerpVisualElementOpacity(VisualElement container, float startOpacity, float endOpacity, float duration, float waitDuration)
		{
			if(startOpacity == 0)
			{
				container.style.opacity = 0;
			}

			yield return new WaitForSeconds(waitDuration);

			container.style.transitionDuration = new List<TimeValue> { duration };
			container.style.opacity = endOpacity;
		}
	}
}