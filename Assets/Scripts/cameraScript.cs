using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 targetPosition;
    public static float cameraPanelSize;
    private Camera camera;
    private static float shake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {   
        //if the players in a panel, go to the panel
        //if not, got to the player
        if(panelMove.currentPanel == null){
            //not in a panel

            targetPosition = player.position;
            cameraPanelSize = 7;
        }
        else{
            targetPosition = panelMove.currentPanel.position;
        }

        //go to target position
        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 5);

        //reset camera position (-10)
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        //set camera size
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, cameraPanelSize, Time.deltaTime * 2);

        transform.position += new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake));
        shake *= 0.9f;
    }

    public static void Shake(float amount){
        if(amount > shake){
            shake = amount;
        }
    }
}
