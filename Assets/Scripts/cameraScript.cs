using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private Transform player;

    public static float CameraPanelSize;

    private Vector3 _targetPosition;
    private Camera _camera;
    private static float _shake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {   
        //if the players in a panel, go to the panel
        //if not, got to the player
        if(PanelMove.CurrentPanel == null){
            //not in a panel

            _targetPosition = player.position;
            CameraPanelSize = 7;
        }
        else{
            _targetPosition = PanelMove.CurrentPanel.position;
        }

        //go to target position
        transform.position = Vector2.Lerp(transform.position, _targetPosition, Time.deltaTime * 5);

        //reset camera position (-10)
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        //set camera size
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, CameraPanelSize, Time.deltaTime * 2);

        transform.position += new Vector3(Random.Range(-_shake, _shake), Random.Range(-_shake, _shake));
        _shake *= 0.9f;
    }

    public static void Shake(float amount){
        if(amount > _shake){
            _shake = amount;
        }
    }
}
