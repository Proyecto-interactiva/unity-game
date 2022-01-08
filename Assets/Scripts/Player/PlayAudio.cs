using UnityEngine.Audio;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource stepAudio;
    public void Step()
    {
        stepAudio.Play();
    }
}
