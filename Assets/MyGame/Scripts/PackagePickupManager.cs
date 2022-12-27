using UnityEngine;

public class PackageManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision");
    }
}
