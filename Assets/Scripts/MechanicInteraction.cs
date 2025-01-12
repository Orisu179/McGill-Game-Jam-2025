using UnityEngine;
using System.Collections;
public class MechanicInteraction : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _bc;
    private Vector3 _startPos;
    private EffectsAudioControl _audioControl;
    public GameObject bangEffect;
    public static bool inThought;
    public float ThoughtShotSpeed;
    public float ThoughtRotationSpeed;
    public Transform mySprite;

    void Start()
    {
        _startPos = transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _audioControl = gameObject.GetComponentInChildren<EffectsAudioControl>();
        _bc = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(inThought){
            transform.Rotate(0,0,ThoughtRotationSpeed*Time.deltaTime);

            if(Input.GetKeyDown(KeyCode.Space)){
                _rb.linearVelocity = mySprite.transform.up * ThoughtShotSpeed;
                _rb.gravityScale = 1;
                inThought = false;

				StartCoroutine(wait());

            }
        }
        else
        {
            lerpRotate(mySprite, 0, 5);
        }
    }

	private IEnumerator wait()
	{
    	yield return new WaitForSeconds(0.06f);
    	_bc.enabled = true;   // Re-enable player collider
	}

    private void lerpRotate(Transform setter, float angle, float speed)
    {
        Vector3 originalAngle = setter.eulerAngles;
        setter.eulerAngles = new Vector3(0, 0, angle);
        Quaternion to = setter.rotation;

        setter.eulerAngles = originalAngle;
        setter.rotation = Quaternion.SlerpUnclamped(setter.rotation, to, Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Spring")){
            Instantiate(bangEffect, transform.position, Quaternion.identity);
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 15);
            CameraScript.Shake(0.3f);
            other.gameObject.GetComponent<Animation>().Play();
            _audioControl.PlayCollisionSound("Spring");
        }
        else if(other.gameObject.CompareTag("Bubble")){
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 12);
            transform.position = other.transform.position;
            _audioControl.PlayCollisionSound("Bubble");
        }
        else if (other.gameObject.CompareTag("Thought"))
        {
            inThought = true;
            transform.position = other.gameObject.transform.position;
            _rb.gravityScale = 0;
			_bc.enabled = false;
            _audioControl.PlayCollisionSound("Thought");
        }
        else if (other.gameObject.CompareTag("Border"))
        {
            transform.position = _startPos;
            _audioControl.PlayCollisionSound("Border");
        }
    }
}
