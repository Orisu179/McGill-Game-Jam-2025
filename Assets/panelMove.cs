using Unity.VisualScripting.ReorderableList;
using UnityEditor.Rendering.LookDev;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class panelMove : MonoBehaviour
{
    public static bool inPanel;
    [SerializeField] private Transform spriteTransform;
    public static Transform currentPanel;
    [SerializeField] private playerMovement movementScript;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inPanel == true){
            setSize(1);

            if(movementScript.enabled == false){
                movementScript.enabled = true;
            }
        }
        else{
            setSize(2);
            //disable player movement script

            if(movementScript.enabled == true){
                movementScript.enabled = false;
                rb.linearVelocity *= 10;
            }
        }
    }

    private void setSize(float targetSize){
        //change size gradually to target size
        spriteTransform.localScale = Vector3.Lerp(spriteTransform.localScale, new Vector3(targetSize, targetSize), Time.deltaTime * 5);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //moving into panel
        inPanel = true;
        currentPanel = other.transform;
        cameraScript.cameraPanelSize = other.GetComponent<panelInfo>().size;
    }

    private void OnTriggerExit2D(Collider2D other) {
        //moving out of panel
        inPanel = false;
        currentPanel = null;
    }


}
