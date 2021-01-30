using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundClass  {

    public string name;
    public AudioClip clip;
    public bool PlayOnAwake;
    public bool Loop;
    [Range(0f,1f)]
    public float vol;
    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
