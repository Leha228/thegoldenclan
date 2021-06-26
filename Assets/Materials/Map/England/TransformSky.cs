using UnityEngine;

public class TransformSky : MonoBehaviour
{

    private bool reverse = false;
    void Start()    
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Round(transform.position.x) == Mathf.Round(Camera.main.transform.position.x)) reverse = true;
        if (Mathf.Round(transform.position.x) == 1063f) reverse = false;

        if (!reverse)
            transform.position = transform.position - Vector3.right * Time.deltaTime;
        else
            transform.position = transform.position - Vector3.left * Time.deltaTime;
    }
}
