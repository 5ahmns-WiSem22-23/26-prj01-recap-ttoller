using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private float zoomOutFactor = 2.0f;

    void FixedUpdate()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = transform.position;
        position.y = Mathf.Lerp(transform.position.y, player.transform.position.y, interpolation);
        position.x = Mathf.Lerp(transform.position.x, player.transform.position.x, interpolation);

        this.transform.position = position;

        float distance = Vector3.Distance(player.transform.position, transform.position) + transform.position.z;
        GetComponent<Camera>().orthographicSize = 5 + (distance * zoomOutFactor);
    }
}
