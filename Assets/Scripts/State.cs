﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour {

    [HideInInspector]
    public static State instance;

    [Header("Objects & UI")]
    public GameObject player;
    public Text objectNameText;
    public Text playStopButtonText;
    public Text appName;

    [Header("UI Stuff")]
    public GameObject[] uiStuffToHideWhenPlayingLeft;
    public GameObject[] uiStuffToHideWhenPlayingRight;

    private GameObject playerClone = null;

    [HideInInspector]
    public static bool isPlaying = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameObjectsManager.lastSelectedObject = player;
        objectNameText.text = player.name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            ChangeGameState();
        }
    }

    public void ChangeGameState()
    {
        isPlaying = !isPlaying;

        if (isPlaying)
        {
            Play();
        }
        else
        {
            Edit();
        }
    }

    private void Play()
    {
        appName.color = Color.black;
        playStopButtonText.color = Color.red;

        player.SetActive(false);

        playerClone = Instantiate(player, player.transform.position, player.transform.rotation, player.transform.parent);
        playerClone.transform.localScale = player.transform.localScale;
        Rigidbody playerCloneRB = playerClone.GetComponent<Rigidbody>();
        playerCloneRB.isKinematic = false;
        playerCloneRB.useGravity = true;


        foreach (GameObject toHide in uiStuffToHideWhenPlayingLeft)
        {
            StartCoroutine(MoveGO(toHide.transform, new Vector2(-2, -2), 40));
        }
        foreach (GameObject toHide in uiStuffToHideWhenPlayingRight)
        {
            StartCoroutine(MoveGO(toHide.transform, new Vector2(2, 2), 40));
        }

        playerClone.SetActive(true);

        CameraController.target = playerClone.transform;
    }

    private void Edit()
    {
        appName.color = Color.white;
        playStopButtonText.color = Color.white;

        Destroy(playerClone);
        player.SetActive(true);

        GameObjectsManager.lastSelectedObject = player;
        objectNameText.text = player.name;

        foreach (GameObject toHide in uiStuffToHideWhenPlayingLeft)
        {
            StartCoroutine(MoveGO(toHide.transform, new Vector2(2, 2), 40));
        }
        foreach (GameObject toHide in uiStuffToHideWhenPlayingRight)
        {
            StartCoroutine(MoveGO(toHide.transform, new Vector2(-2, -2), 40));
        }
    }

    private IEnumerator MoveGO(Transform toHide, Vector2 direction, int times)
    {
        for (int i = 0; i <= times; i++)
        {
            toHide.Translate(direction);
            yield return new WaitForSeconds(.01f);
        }
    }

}
