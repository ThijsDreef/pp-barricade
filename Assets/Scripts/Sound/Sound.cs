using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip[] clip;
    [Range(0,5)]
    public float delay;
    [Range(0, 1)]
    public float volume = 0.5f;
    [Range(0, 1.5f)]
    public float pitch = 1;

    private AudioSource source;
    
    Sound() {
        volume = 1;
        pitch = 1;
    }

    public void SetSource(AudioSource _source)
    {
        source = _source;
        int randomNumber = Random.Range(0, clip.Length);
        if(clip.Length != 0) {
            source.clip = clip[randomNumber];
        } else {
            Debug.Log("no sound");
        }
    }

    public void Play()
    {
        if(!source.isPlaying) {
            if(clip.Length > 1) {
                SetSource(source);
            }
            source.pitch = pitch + Random.Range(-0.1f,0.1f);
            source.volume = volume;
            source.Play();
        }
    }
}
