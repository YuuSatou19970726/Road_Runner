using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private AudioSource move_Audio_Source,
        jump_Audio_Source,
        power_Up_Die_Audio_Source,
        background_Audio_Source;


    [SerializeField]
    private AudioClip
        power_Up_Clip,
        die_Clip,
        coint_Clip,
        game_Over_Clip;

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        // TEST IF WE SHOULD PLAY BG SOUND
        if (GameManager.instance.playSound)
        {
            background_Audio_Source.Play();
        }
        else
        {
            background_Audio_Source.Stop();
        }
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void PlayMoveLineSound()
    {
        move_Audio_Source.Play();
    }

    public void PlayJumpSound()
    {
        jump_Audio_Source.Play();
    }

    public void PlayDeadSound()
    {
        power_Up_Die_Audio_Source.clip = die_Clip;
        power_Up_Die_Audio_Source.Play();
    }

    public void PlayPowerUpSound()
    {
        power_Up_Die_Audio_Source.clip = power_Up_Clip;
        power_Up_Die_Audio_Source.Play();
    }

    public void PlayCoinSound()
    {
        power_Up_Die_Audio_Source.clip = coint_Clip;
        power_Up_Die_Audio_Source.Play();
    }

    public void PlayGameOverClip()
    {
        background_Audio_Source.Stop();
        background_Audio_Source.clip = game_Over_Clip;
        background_Audio_Source.loop = false;
        background_Audio_Source.Play();
    }
}
