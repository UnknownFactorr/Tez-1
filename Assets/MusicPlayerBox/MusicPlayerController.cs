using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MusicPlayerController : MonoBehaviour
{
    public AudioSource musicPlayerAudioSource;

    public GameObject musicPlayerMenu;

    [Header("ListOfTracks")]
    [SerializeField] private Track[] audioTracks;
    private int trackIndex;

    [Header("TextUI")]
    [SerializeField] private TextMeshProUGUI trackTextUI;

    private void Start()
    {
        trackIndex = 0;
        // "trackAudioClip" below comes from the scriptable object defined in "Track" script
        musicPlayerAudioSource.clip = audioTracks[trackIndex].trackAudioClip;
        trackTextUI.text = audioTracks[trackIndex].name;
    }

    public void SkipForwardButton()
    {
        if (trackIndex <= audioTracks.Length - 2)
        {
            trackIndex++;
            StartCoroutine(FadeOut(musicPlayerAudioSource, 0.5f));
        }
    }

    public void SkipBackwardButton()
    {
        if (trackIndex >= 1)
        {
            trackIndex--;
            StartCoroutine(FadeOut(musicPlayerAudioSource, 0.5f));
        }
    }

    void UpdateTrack(int index)
    {
        musicPlayerAudioSource.clip = audioTracks[index].trackAudioClip;
        trackTextUI.text = audioTracks[index].name;
    }

    public void AudioVolumeSetter(float volume)
    {
        musicPlayerAudioSource.volume = volume;
    }

    public void PlayAudio()
    {
        StartCoroutine(FadeIn(musicPlayerAudioSource, 0.5f));
    }

    public void PauseAudio()
    {
        musicPlayerAudioSource.Pause();
    }

    public void StopAudio()
    {
        StartCoroutine(FadeOut(musicPlayerAudioSource, 0.5f));
    }

    public IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        UpdateTrack(trackIndex);
    }

    public IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.volume = startVolume;
    }

    public void CloseButton()
    {
        musicPlayerMenu.SetActive(false);
    }

    public void OpenMusicMenu()
    {
        musicPlayerMenu.SetActive(true);
    }
}
