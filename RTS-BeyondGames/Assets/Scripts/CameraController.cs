using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f;

    private float cameraY;

    private void Awake()
    {
        cameraY = transform.position.y;
    }

    private void Update()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        Vector3 moveDir = new Vector3(moveX, 0, moveY).normalized;

        if (moveX != 0 || moveY != 0)
        {
            // Not idle
        }

        transform.position += moveSpeed * Time.deltaTime * moveDir;
    }
 
    private void Zoom()
    {
        float zoomSpeed = 1f;
        cameraY += -Input.mouseScrollDelta.y * zoomSpeed;
        cameraY = Mathf.Clamp(cameraY, -3f, 17);
        transform.position = new Vector3(transform.position.x, cameraY, transform.position.z);
    }

}
