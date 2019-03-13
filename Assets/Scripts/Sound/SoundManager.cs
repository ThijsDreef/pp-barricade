using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixerGroup audioMixerGroup;
    public static SoundManager Instance;

    public Sound[] sounds;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start() {
        AudioSourcesCreator();
    }

    private void AudioSourcesCreator() {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].soundName);
            _go.transform.SetParent(this.transform);
            AudioSource audioSource = _go.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            sounds[i].SetSource(audioSource);
        }
    }

    public void PlaySound(string _soundName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].soundName == _soundName)
            {
                StartCoroutine(Delay(i, sounds[i].delay));
                return;
            }
        }
        //no sounds found
        Debug.LogWarning("AudioManager: Sound not found in list. " + _soundName);
    }

    private IEnumerator Delay(int index, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        sounds[index].Play();
    }
}
