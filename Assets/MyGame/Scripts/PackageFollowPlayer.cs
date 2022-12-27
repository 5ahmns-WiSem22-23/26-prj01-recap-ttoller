using UnityEngine;

public class PackageFollowPlayer : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float packageCarDistance = 5.0f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position;
    }
    private void FixedUpdate()
    {
        Vector3 destPos = player.transform.position + player.transform.up * packageCarDistance;
        transform.position = Vector3.Lerp(transform.position, destPos, speed * Time.deltaTime);
    }
}
