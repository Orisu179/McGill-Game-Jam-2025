using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float moveDistance = 3.0f;
    // 1 for right initially, -1 for left
    [SerializeField] private int direction = 1;
    private Vector3 startPos;
    private Vector3 endPos;
    private float trackPercent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       startPos = transform.position;
       endPos = startPos + new Vector3(direction, 0, 0) * moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        trackPercent += direction * speed * Time.deltaTime;
        float x = (endPos.x - startPos.x) * trackPercent + startPos.x;
        float y = (endPos.y - startPos.y) * trackPercent + startPos.y;
        transform.position = new Vector3(x, y, startPos.z);

        if ((direction == 1 && trackPercent > 0.9f) || (direction == -1 && trackPercent < 0.1f))
        {
            direction *= -1;
        }
    }
}
