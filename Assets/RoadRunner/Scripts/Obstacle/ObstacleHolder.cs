using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject[] childs;

    [SerializeField]
    private float limitAxisX;

    [SerializeField]
    private Vector3 firstPos, secondPos;

    // IS CALLED FIRST WHEN THE GAME STARTS
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-GameplayController.instance.moveSpeed * Time.deltaTime, 0f, 0f);

        if (transform.localPosition.x <= limitAxisX)
        {
            // Inform gameplay controller that the obstacle is not active
            GameplayController.instance.obstacles_Is_Active = false;
            gameObject.SetActive(false);
        }
    }

    // IS CALLED SECOND WHEN THE GAME STARTS
    void OnEnable()
    {
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].SetActive(true);
        }

        if (Random.value <= 0.5f)
        {
            transform.localPosition = firstPos;
        }
        else
        {
            transform.localPosition = secondPos;
        }
    }
}
