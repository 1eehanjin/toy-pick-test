using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    Camera camera;
    void Awake()
    {
        camera = GetComponent<Camera>();
        camera.orthographicSize = 844 * 0.005f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camera.orthographicSize = 844  * 0.005f;


    }
}
