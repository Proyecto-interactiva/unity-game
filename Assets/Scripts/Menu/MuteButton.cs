using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private AudioManager audioManager;
    public Sprite mutedSprite;
    public Sprite unmutedSprite;


    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager.muted)
        {
            gameObject.GetComponent<Image>().sprite = mutedSprite;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = unmutedSprite;
        }
    }

    public void mute()
    {
        audioManager.MuteToggle();
        if (audioManager.muted)
        {
            gameObject.GetComponent<Image>().sprite = mutedSprite;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = unmutedSprite;
        }
    }
}
