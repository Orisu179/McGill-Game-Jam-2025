using UnityEngine;

public class PortalTeleport : MonoBehaviour
{

    public Transform target;
    public float direction;
    private CircleCollider2D circleCollider;
    

    void Start()
    {
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.position = new Vector3(target.transform.position.x + 1 * direction, target.transform.position.y - 0.5f);
        
    }
}
