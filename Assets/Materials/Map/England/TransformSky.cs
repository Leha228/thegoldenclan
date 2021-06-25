using UnityEngine;

public class TransformSky : MonoBehaviour
{

    void Start()    
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position - Vector3.right * Time.deltaTime;
    }
}
