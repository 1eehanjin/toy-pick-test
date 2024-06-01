using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 시스템을 사용하기 위해 추가

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float speed = 120.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    public float initial_x = 0.0f;
    public float initial_y = 0.0f;
    private Vector3 direction = Vector3.zero; // 회전 방향을 저장할 벡터

    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        initial_x = angles.y;
        y = angles.x;
        initial_y = angles.x;
    }

    void Update()
    {
        if (target)
        {
            x += direction.x * speed * distance * Time.deltaTime;
            y -= direction.y * speed * Time.deltaTime;


            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, -0.3f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;

            transform.LookAt(target);
        }
    }

    public void RotateCenter()
    {
        x = initial_x;
        y = initial_y;
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void RotateRight()
    {
        SetDirection(new Vector3(-1, 0, 0));
    }
    public void RotateLeft()
    {
        SetDirection(new Vector3(1, 0, 0));
    }
    public void StopRotate()
    {
        SetDirection(new Vector3(0, 0, 0));
    }

}
