using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Transform _audioSourceParent;
    private AudioSource[] _audioSourcesArray;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        int _childCount = _audioSourceParent.transform.childCount;
        _audioSourcesArray = new AudioSource[_childCount];
        _audioSourcesArray = _audioSourceParent.transform.GetComponentsInChildren<AudioSource>();

        //testing play
        Play(_audioSourcesArray[1].clip, 0, true);
    }

    private void Play(AudioClip audioClip, int audioSourceIndex)
    {
        AudioSource _audioSource = _audioSourcesArray[audioSourceIndex];
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    private void Play(AudioClip audioClip, int audioSourceIndex, bool IsFading)
    {
        if (IsFading)
        {
            Debug.Log("isfading");
            AudioSource _audioSource = _audioSourcesArray[audioSourceIndex];
            StartCoroutine(StartFadeIn(_audioSource, audioClip,1, 0));
           // StartCoroutine(StartFadeOut(_audioSource, audioClip, 1, 1));
        }
        else
        {
            Play(audioClip, audioSourceIndex); //default method 
        }
    }

    private void Play(AudioClip audioClip, int audioSourceIndex, bool IsFading, float delay)
    {
        AudioSource _audioSource = _audioSourcesArray[audioSourceIndex];

        if (IsFading)
        {
            StartCoroutine(  StartFadeIn(_audioSource, audioClip,1f, 0, delay));
            // StartCoroutine(  StartFadeOut(_audioSource, audioClip, 1f, 1, delay));
        }
        else
        {
            StartCoroutine(    StartFadeIn(_audioSource, audioClip, 0.01f, 0, delay));
            // StartCoroutine(  StartFadeOut(_audioSource, audioClip, 0.01f, 1, delay));
        }
    }

    private void Stop(int audioSourceIndex)
    {
        _audioSourcesArray[audioSourceIndex].Stop();
    }

    public IEnumerator StartFadeIn(AudioSource audioSource,AudioClip audioClip, float duration, float targetVolume, float delay = 1)
    {
        yield return new WaitForSeconds(delay);
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        StartCoroutine(StartFadeOut(audioSource,audioClip,duration,1,delay));
        yield break;
    }

    public static IEnumerator StartFadeOut(AudioSource audioSource, AudioClip audioClip, float duration,
        float targetVolume, float delay = 1)
    {
        yield return new WaitForSeconds(delay);
        audioSource.clip = audioClip;
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audioSource.Play();
        yield break;
    }

    // public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    // {
    //     float currentTime = 0;
    //     float currentVol;
    //     audioMixer.GetFloat(exposedParam, out currentVol);
    //     currentVol = Mathf.Pow(10, currentVol / 20);
    //     float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
    //
    //     while (currentTime < duration)
    //     {
    //         currentTime += Time.deltaTime;
    //         float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
    //         audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
    //         yield return null;
    //     }
    //     yield break;
    // }
}