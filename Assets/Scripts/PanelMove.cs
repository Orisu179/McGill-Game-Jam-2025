using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEditor.Callbacks;
using UnityEditor.Rendering.LookDev;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Serialization;

public class PanelMove : MonoBehaviour
{
    public static bool InPanel;
    public Transform currentPanel;

    [SerializeField] private Transform spriteTransform;
    private CharacterMovement _movementScript;
    private Rigidbody2D _rb;
    private Vector2 leaveVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _movementScript = gameObject.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InPanel)
        {
            setSize(1);
            //reenable movement
            if (!_movementScript.enabled)
            {
                //re-entering a panel
                _movementScript.enabled = true;
                _rb.gravityScale = 1;

                //slow down player
                _rb.linearVelocity /= 3;
                CameraScript.Shake(0.3f);
            }
        }
        else
        {
            setSize(2);
            //disable player movement script

            if (_movementScript.enabled)
            {
                //leaving a panel
                _movementScript.enabled = false;

                //boost
                //_rb.linearVelocity = _rb.linearVelocity.normalized * 10;
                _rb.linearVelocity = leaveVelocity * 15;

                _rb.gravityScale = 0;
                _rb.linearDamping = 0;

                CameraScript.Shake(0.3f);
            }
        }
    }

    private void setSize(float targetSize)
    {
        //change size gradually to target size
        spriteTransform.localScale = Vector3.Lerp(spriteTransform.localScale, new Vector3(targetSize, targetSize),
            Time.deltaTime * 10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //moving into panel
        InPanel = true;
        currentPanel = other.transform;
        CameraScript.CameraPanelSize = other.GetComponent<PanelInfo>().size;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //moving out of panel
        InPanel = false;
        currentPanel = null;
        if(transform.position.x > other.transform.position.x + other.transform.localScale.x / 2.2f){
            leaveVelocity = new Vector2(1, 0);
        }
        else if(transform.position.x < other.transform.position.x - other.transform.localScale.x / 2.2f){
            leaveVelocity = new Vector2(-1, 0);
        }

        if(transform.position.y > other.transform.position.y + other.transform.localScale.y / 2.2f){
            leaveVelocity = new Vector2(0, 1);
        }
        else if(transform.position.y < other.transform.position.y - other.transform.localScale.y / 2.2f){
            leaveVelocity = new Vector2(0, -1);
        }
    }
}