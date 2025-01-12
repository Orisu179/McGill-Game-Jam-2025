using UnityEngine;

public class PortalTeleport : MonoBehaviour
{

    public Transform target;
    private CircleCollider2D circleCollider;

    void Start()
    {
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        float multiplier = other.transform.position.x < this.transform.position.x ? 1 : -1;
        other.transform.position = new Vector3(target.transform.position.x + 1*multiplier, target.transform.position.y - 0.5f);
        
    }
}
