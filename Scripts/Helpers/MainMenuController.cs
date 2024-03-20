using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject hero_Menu;
    [SerializeField]
    private Text startScoreText;

    [SerializeField]
    private Image music_Img;
    [SerializeField]
    private Sprite music_Off, music_On;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void HeroMenu()
    {
        hero_Menu.SetActive(true);
        // display the star score
        startScoreText.text = "" + GameManager.instance.starScore;
    }

    public void HomeButton()
    {
        hero_Menu.SetActive(false);
    }

    public void MusicButton()
    {
        if (GameManager.instance.playSound)
        {
            music_Img.sprite = music_Off;
            GameManager.instance.playSound = false;
        }
        else
        {
            music_Img.sprite = music_On;
            GameManager.instance.playSound = true;
        }
    }
}
