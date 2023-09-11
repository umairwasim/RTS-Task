using UnityEngine;

public class VfxManager : MonoBehaviour
{
    public static VfxManager Instance;

    public GameObject teleportVfx;

    private void Awake()
    {
        Instance = this;
    }

    //Display Vfx
    public void DisplayVfx(GameObject vfxToSpawn, Vector3 position, float delay = 3f)
    {
        GameObject vfx = Instantiate(vfxToSpawn, position, Quaternion.identity);
        Destroy(vfx, delay);
    }
}
