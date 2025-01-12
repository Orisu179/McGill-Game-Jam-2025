using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    //makes the platform wait a bit before changing direction
    public float delay = 0.5f;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float moveDistance = 2.5f;
    // 1 for right initially, -1 for left
    [SerializeField] private int direction = 1;
    private Vector3 startPos;
    private Vector3 endPos;
    private float trackPercent;
    private bool isWaiting = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       startPos = transform.position;
       endPos = startPos + new Vector3(direction, 0, 0) * moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting) return;

        trackPercent += direction * speed * Time.deltaTime;
        float x = Mathf.Lerp(startPos.x, endPos.x, trackPercent);
        float y = Mathf.Lerp(startPos.y, endPos.y, trackPercent);
        transform.position = new Vector3(x, y, startPos.z);

        if ((direction == 1 && trackPercent >= 1.0f) || (direction == -1 && trackPercent <= 0.0f))
        {
            StartCoroutine(WaitAndSwitchDirection());
        }
    }

    private IEnumerator WaitAndSwitchDirection()
    {
        isWaiting = true;
        yield return new WaitForSeconds(delay);
        direction *= -1;
        isWaiting = false;
    }
}
