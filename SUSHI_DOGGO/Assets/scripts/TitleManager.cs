using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleManager : MonoBehaviour
{
    public AudioClip bgmClip;  // 在Inspector窗口中分配BGM音频文件
    public AudioSource bgmAudioSource;  // 用于播放BGM的音频源
    public float fadeInDuration = 2.0f;  // 淡入持续时间（秒）

    private void Start()
    {
        // 播放BGM
        if (bgmClip != null && bgmAudioSource != null)
        {
            bgmAudioSource.clip = bgmClip;
            bgmAudioSource.Play();

            // 开始淡入
            StartCoroutine(FadeInBGM());
        }
    }

    // Coroutine 用于淡入BGM
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

        // 确保音量最终达到原始音量
        bgmAudioSource.volume = originalVolume;
    }
}
