using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public RawImage rawImage;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (rawImage == null)
        {
            // Используем FindFirstObjectByType вместо FindObjectOfType
            rawImage = Object.FindFirstObjectByType<RawImage>();
        }

        // Убедитесь, что аудио источник подключен к видео плееру
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);

        // Подключаем текстуру видео к RawImage
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }

    private void OnVideoPrepared(VideoPlayer source)
    {
        rawImage.texture = source.texture;
        source.prepareCompleted -= OnVideoPrepared;
        source.Play();
    }
}