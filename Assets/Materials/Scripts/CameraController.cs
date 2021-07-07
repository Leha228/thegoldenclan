
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

        StartCoroutine(Timer(5));
    }

    IEnumerator Timer(float timeSecond) {
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

    public void NextLevel(int numberLevel) {
        level = numberLevel;
        moveCameraBoll = true;

        leftLimit = GameObject.Find("leftLimit" + Convert.ToString(numberLevel)).transform.position.x;
        rightLimit = GameObject.Find("rightLimit" + Convert.ToString(numberLevel)).transform.position.x;
    }

    private void MoveCamera() {
        if (Math.Round(leftLimit) == Math.Round(transform.position.x)) moveCameraBoll = false;
        posCam = new Vector3(leftLimit, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posCam, Time.deltaTime);
    }

    void Update()
    {
        if (!coroutine) StartCoroutine(Timer(5)); 
        if (moveCameraBoll) MoveCamera();
        else {
            Move();
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit), 
                Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                transform.position.z
            );
        }
    }

    private void Move() {
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
