using System.Collections;
using System;
using UnityEngine;

public class ManagerEvent : MonoBehaviour
{
    private GameObject leftLimitBird;
    private int level;
    private bool coroutine = false;
    public GameObject bird;

    private void Start() {
        replaceLevel();
        StartCoroutine(createBird(1f));
    }

    void Update()
    {
        if (!coroutine) StartCoroutine(createBird(15));
        checkLevel();
    }

    IEnumerator createBird(float timeSecond) {
        float counter = 0f;
        coroutine = true;

        while (counter < timeSecond) {
            counter += Time.deltaTime;
            yield return null;
        }

        coroutine = false;
        Instantiate(bird, leftLimitBird.transform.position, leftLimitBird.transform.rotation);
    } 

    private void replaceLevel() {
        level = CameraController.singleton.level;
        leftLimitBird = GameObject.Find("leftLimit" + Convert.ToString(level + 1));
    }

    private void checkLevel() {
        if (level == CameraController.singleton.level) return;
        replaceLevel();
    }
}
