using Unity.VisualScripting.ReorderableList;
using UnityEditor.Rendering.LookDev;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PanelMove : MonoBehaviour
{
    public static Transform CurrentPanel;

    [SerializeField] private Transform spriteTransform;
    private CharacterMovement _movementScript;
    private static bool _inPanel;
    private Rigidbody2D _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _movementScript = gameObject.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_inPanel == true){
            setSize(1);
            //reenable movement
            if(_movementScript.enabled == false){
                //re-entering a panel
                _movementScript.enabled = true;
                _rb.gravityScale = 1;

                //slow down player
                _rb.linearVelocity /= 3;
            }
        }
        else{
            setSize(2);
            //disable player movement script

            if(_movementScript.enabled == true){

                //leaving a panel
                _movementScript.enabled = false;

                //boost
                _rb.linearVelocity = _rb.linearVelocity.normalized * 10;

                _rb.gravityScale = 0;
            }
        }
    }

    private void setSize(float targetSize){
        //change size gradually to target size
        spriteTransform.localScale = Vector3.Lerp(spriteTransform.localScale, new Vector3(targetSize, targetSize), Time.deltaTime * 5);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //moving into panel
        _inPanel = true;
        CurrentPanel = other.transform;
        cameraScript.CameraPanelSize = other.GetComponent<panelInfo>().size;
    }

    private void OnTriggerExit2D(Collider2D other) {
        //moving out of panel
        _inPanel = false;
        CurrentPanel = null;
    }
}
