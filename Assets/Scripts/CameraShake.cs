using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private float shakeDuration = 0.35f;
    private float shakeAmplitude = 1f;
    private float shakeFrequency = 3f;

    void Start()
    {
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (noise != null)
            {
                StartCoroutine(ShakeCamera(shakeDuration, shakeAmplitude, shakeFrequency));
            }
        }
    }

    IEnumerator ShakeCamera(float duration, float amplitude, float frequency)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            noise.m_AmplitudeGain = Mathf.Lerp(1f, amplitude, elapsed / duration);
            noise.m_FrequencyGain = Mathf.Lerp(1f, frequency, elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        float fadeOutDuration = 0.5f;
        float startAmplitude = noise.m_AmplitudeGain;
        float startFrequency = noise.m_FrequencyGain;
        float timer = 0f;

        while (timer < fadeOutDuration)
        {
            noise.m_AmplitudeGain = Mathf.Lerp(startAmplitude, 0f, timer / fadeOutDuration);
            noise.m_FrequencyGain = Mathf.Lerp(startFrequency, 0f, timer / fadeOutDuration);

            timer += Time.deltaTime;
            yield return null;
        }

        noise.m_AmplitudeGain = 0.5f;
        noise.m_FrequencyGain = 0.5f;
    }
}
