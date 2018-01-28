using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public static AudioController Instance;

    public List<AudioClip> Sfxs = new List<AudioClip>();
    public List<AudioClip> WarriorSfxs = new List<AudioClip>();
    public List<AudioClip> SorcererSfxs = new List<AudioClip>();
    public List<AudioClip> AlchemistSfxs = new List<AudioClip>();

    AudioSource audioSource;

    void Start () {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
	}

    public void PlaySfx(int index)
    {
        audioSource.PlayOneShot(Sfxs[index]);
    }

    public void PlayWarriorSfx(int index)
    {
        audioSource.PlayOneShot(WarriorSfxs[index]);
    }

    public void PlaySorcererSfx(int index)
    {
        audioSource.PlayOneShot(SorcererSfxs[index]);
    }

    public void PlayAlchemistSfx(int index)
    {
        audioSource.PlayOneShot(AlchemistSfxs[index]);
    }
}
