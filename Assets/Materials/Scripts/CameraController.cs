
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 pos, posCam;

    private float leftLimit, rightLimit;
    public float topLimit, bottomLimit;
    private bool moveCameraBoll = false;

    void Start()
    {
        leftLimit = GameObject.Find("leftLimit" + Convert.ToString(1)).transform.position.x;
        rightLimit = GameObject.Find("rightLimit" + Convert.ToString(1)).transform.position.x;
    }

    public void nextLevel() {
        Debug.Log("Mission complete");
        moveCameraBoll = true;

        leftLimit = GameObject.Find("leftLimit" + Convert.ToString(2)).transform.position.x;
        rightLimit = GameObject.Find("rightLimit" + Convert.ToString(2)).transform.position.x;
    }

    private void moveCamera() {
        if (Math.Round(leftLimit) == Math.Round(transform.position.x)) moveCameraBoll = false;
        posCam = new Vector3(leftLimit, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posCam, Time.deltaTime);
    }

    void Update()
    {
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
