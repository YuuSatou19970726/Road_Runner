using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Animator anim;

    private string jump_Animation = "PlayerJump", change_Line_Animation = "ChangeLine";

    [SerializeField]
    private GameObject player, shadow;

    [SerializeField]
    private Vector3 first_Pos_Of_Player, second_Pos_Of_Player;

    [HideInInspector]
    public bool player_Died;

    [HideInInspector]
    public bool player_Jumped;

    [SerializeField]
    private GameObject explosion;

    private SpriteRenderer player_Renderer;

    [SerializeField]
    private Sprite TRex_Sprite, player_Sprite;

    private bool TRex_Trigger;

    private GameObject[] start_Effect;

    void Awake()
    {
        MakeInstance();
        anim = player.GetComponent<Animator>();

        player_Renderer = player.GetComponent<SpriteRenderer>();

        start_Effect = GameObject.FindGameObjectsWithTag(MyTags.STAR_EFFECT);
    }

    // Start is called before the first frame update
    void Start()
    {
        TRex_Trigger = false;
        // player_Renderer.sprite = player_Sprite;

        string path = "Sprites/Player/hero" + GameManager.instance.selected_Index + "_big";
        player_Sprite = Resources.Load<Sprite>(path);
        player_Renderer.sprite = player_Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        HandleChangeLine();
        HandleJump();
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

    void HandleChangeLine()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            anim.Play(change_Line_Animation);
            transform.localPosition = second_Pos_Of_Player;

            // PLAY THE SOUND
            SoundManager.instance.PlayMoveLineSound();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            anim.Play(change_Line_Animation);
            transform.localPosition = first_Pos_Of_Player;

            // PLAY THE SOUND
            SoundManager.instance.PlayMoveLineSound();
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!player_Jumped)
            {
                anim.Play(jump_Animation);
                player_Jumped = true;
            }

            // PLAY THE SOUND
            SoundManager.instance.PlayJumpSound();
        }
    }

    void Die()
    {
        player_Died = true;
        player.SetActive(false);
        shadow.SetActive(false);

        GameplayController.instance.moveSpeed = 0f;
        GameplayController.instance.GameOver();

        // PLAY SOUND PLAYER DEAD
        SoundManager.instance.PlayDeadSound();
        // PLAY SOUND GAME OVER
        SoundManager.instance.PlayGameOverClip();

    }

    void DieWithObstacle(Collider2D target)
    {
        Die();

        explosion.transform.position = target.transform.position;
        explosion.SetActive(true);
        target.gameObject.SetActive(false);

        // SOUND MANAGER PLAY PLAYER DEAD SOUND
        SoundManager.instance.PlayDeadSound();
    }

    IEnumerator TRexDuration()
    {
        yield return new WaitForSeconds(7f);

        if (TRex_Trigger)
        {
            TRex_Trigger = false;
            player_Renderer.sprite = player_Sprite;
        }
    }

    void DestroyObstacle(Collider2D target)
    {
        explosion.transform.position = target.transform.position;
        explosion.SetActive(false); // turn off the explosion if it's already turned on
        explosion.SetActive(true);

        target.gameObject.SetActive(false);

        // SOUND MANAGER PLAY DEAD SOUND
        SoundManager.instance.PlayDeadSound();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.OBSTACLE)
        {
            if (!TRex_Trigger)
            {
                DieWithObstacle(target);
            }
            else
            {
                DestroyObstacle(target);
            }
        }

        if (target.CompareTag(MyTags.T_REX))
        {
            TRex_Trigger = true;
            player_Renderer.sprite = TRex_Sprite;
            target.gameObject.SetActive(false);

            // SOUND MANAGER TO PLAY THE MUSIC
            SoundManager.instance.PlayPowerUpSound();

            StartCoroutine(TRexDuration());
        }

        if (target.tag == MyTags.STAR)
        {
            for (int i = 0; i < start_Effect.Length; i++)
            {
                if (!start_Effect[i].activeInHierarchy)
                {
                    start_Effect[i].transform.position = target.transform.position;
                    start_Effect[i].SetActive(true);
                    break;
                }
            }


            target.gameObject.SetActive(false);
            // SOUND MANAGER PLAY SOUND
            SoundManager.instance.PlayCoinSound();

            // GAMEPLAY CONTROLLER INCREASE STAR SCORE
            GameplayController.instance.UpdateStarScore();
        }
    }
}
