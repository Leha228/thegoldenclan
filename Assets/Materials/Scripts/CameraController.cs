
using System.Collections;
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController singleton { get; private set; }
    public Transform player, enemy;
    private Vector3 pos, posCam;
    private float leftLimit, rightLimit, startPlayerX, startPlayerY;
    public float topLimit, bottomLimit;
    private bool moveCameraBoll = false;
    private bool coroutine = false;
    public int level = 1;

    private void Awake() {
        singleton = this;
    }

    void Start()
    {
        leftLimit = GameObject.Find("leftLimit" + Convert.ToString(1)).transform.position.x;
        rightLimit = GameObject.Find("rightLimit" + Convert.ToString(1)).transform.position.x;
        startPlayerX = GameObject.Find(Convert.ToString(0)).transform.position.x;
        startPlayerY = GameObject.Find(Convert.ToString(0)).transform.position.y;

        player.position = new Vector3(startPlayerX, startPlayerY + 1.5f, 0f);
        transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);

        StartCoroutine(timer(5));
    }

    IEnumerator timer(float timeSecond) {
        float counter = 0f;
        coroutine = true;

        while (counter < timeSecond) {
            counter += Time.deltaTime;
            yield return null;
        }

        PlayerController.singleton.enabled = !PlayerController.singleton.enabled;
        EnemyShoot.singleton.enabled = !EnemyShoot.singleton.enabled;

        if (EnemyShoot.singleton.enabled) EnemyShoot.singleton.shootToPlayer = true;

        coroutine = false;
    }

    public void nextLevel(int numberLevel) {
        level = numberLevel;
        moveCameraBoll = true;

        leftLimit = GameObject.Find("leftLimit" + Convert.ToString(numberLevel)).transform.position.x;
        rightLimit = GameObject.Find("rightLimit" + Convert.ToString(numberLevel)).transform.position.x;
    }

    private void moveCamera() {
        if (Math.Round(leftLimit) == Math.Round(transform.position.x)) moveCameraBoll = false;
        posCam = new Vector3(leftLimit, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posCam, Time.deltaTime);
    }

    void Update()
    {
        if (!coroutine) StartCoroutine(timer(5)); 
        if (moveCameraBoll) moveCamera();
        else {
            move();
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit), 
                Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                transform.position.z
            );
        }
    }

    private void move() {
        pos = player.position;
        pos.z = -10f;

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));   
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
    }
}
