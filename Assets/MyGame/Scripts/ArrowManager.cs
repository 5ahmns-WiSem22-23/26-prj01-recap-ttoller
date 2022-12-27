using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if (target != null)
        {
            Vector3 dir = player.transform.InverseTransformDirection(target.transform.position - player.transform.position);
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg - 180;
            transform.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
