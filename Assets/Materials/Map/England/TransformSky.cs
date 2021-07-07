using UnityEngine;

public class TransformSky : MonoBehaviour
{

    private bool _reverse = false;

    void Update()
    {
        if (Mathf.Round(transform.position.x) == Mathf.Round(Camera.main.transform.position.x)) _reverse = true;
        if (Mathf.Round(transform.position.x) == 1063f) _reverse = false;

        if (!_reverse)
            transform.position = transform.position - Vector3.right * Time.deltaTime;
        else
            transform.position = transform.position - Vector3.left * Time.deltaTime;
    }
}
