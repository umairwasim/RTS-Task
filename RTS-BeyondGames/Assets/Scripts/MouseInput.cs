using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public static MouseInput Instance;

    [SerializeField] private LayerMask mouseColliderLayerMask = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
            transform.position = raycastHit.point;
    }
    //Helper function to get Mouse World Position
    public static Vector3 GetMouseWorldPosition() => Instance.GetMouseWorldPosition_Instance();

    private Vector3 GetMouseWorldPosition_Instance()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask) ? raycastHit.point : Vector3.zero;
    }
}
