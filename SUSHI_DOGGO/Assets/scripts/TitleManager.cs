using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleManager : MonoBehaviour
{
    public AudioClip bgmClip;  // ��Inspector�����з���BGM��Ƶ�ļ�
    public AudioSource bgmAudioSource;  // ���ڲ���BGM����ƵԴ
    public float fadeInDuration = 2.0f;  // �������ʱ�䣨�룩

    private void Start()
    {
        // ����BGM
        if (bgmClip != null && bgmAudioSource != null)
        {
            bgmAudioSource.clip = bgmClip;
            bgmAudioSource.Play();

            // ��ʼ����
            StartCoroutine(FadeInBGM());
        }
    }

    // Coroutine ���ڵ���BGM
    private IEnumerator FadeInBGM()
    {
        float timer = 0f;
        float originalVolume = bgmAudioSource.volume;

        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            bgmAudioSource.volume = Mathf.Lerp(0, originalVolume, timer / fadeInDuration);
            yield return null;
        }

        // ȷ���������մﵽԭʼ����
        bgmAudioSource.volume = originalVolume;
    }
}
