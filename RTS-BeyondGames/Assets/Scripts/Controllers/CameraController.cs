using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Movement Keys"), SerializeField] private KeyCode UP = KeyCode.W;
    [SerializeField] private KeyCode DOWN = KeyCode.S;
    [SerializeField] private KeyCode LEFT = KeyCode.A;
    [SerializeField] private KeyCode RIGHT = KeyCode.D;

    [Header("Speed"), SerializeField] private float moveSpeed = 50f;
    [SerializeField] private float zoomSpeed = 1f;

    [Header("Zoom Y Values"), SerializeField] private float minY = 3f;
    [SerializeField] private float maxY = 10f;

    private float cameraY;

    private void Awake() => cameraY = transform.position.y;

    private void Update()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(UP))
            moveY = +1f;
        if (Input.GetKey(DOWN))
            moveY = -1f;
        if (Input.GetKey(LEFT))
            moveX = -1f;
        if (Input.GetKey(RIGHT))
            moveX = +1f;

        Vector3 moveDir = new Vector3(moveX, 0, moveY).normalized;
        transform.position += moveSpeed * Time.deltaTime * moveDir;
    }

    private void Zoom()
    {
        cameraY += -Input.mouseScrollDelta.y * zoomSpeed;
        cameraY = Mathf.Clamp(cameraY, minY, maxY);
        transform.position = new Vector3(transform.position.x, cameraY, transform.position.z);
    }

}
