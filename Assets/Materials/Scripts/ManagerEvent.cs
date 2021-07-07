using System.Collections;
using System;
using UnityEngine;

public class ManagerEvent : MonoBehaviour
{
    private GameObject leftLimitBird;
    private int _level;
    private bool _coroutine = false;
    public GameObject bird;

    private void Start() {
        ReplaceLevel();
        StartCoroutine(CreateBird(1f));
    }

    void Update()
    {
        if (!_coroutine) StartCoroutine(CreateBird(15));
        CheckLevel();
    }

    IEnumerator CreateBird(float timeSecond) {
        float counter = 0f;
        _coroutine = true;

        while (counter < timeSecond) {
            counter += Time.deltaTime;
            yield return null;
        }

        _coroutine = false;
        Instantiate(bird, leftLimitBird.transform.position, leftLimitBird.transform.rotation);
    } 

    private void ReplaceLevel() {
        _level = CameraController.singleton.level;
        leftLimitBird = GameObject.Find("leftLimit" + Convert.ToString(_level + 1));
    }

    private void CheckLevel() {
        if (_level == CameraController.singleton.level) return;
        ReplaceLevel();
    }
}
