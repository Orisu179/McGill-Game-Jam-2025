using Unity.VisualScripting;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{

    public Transform target;
    public float direction;
    private EffectsAudioControl _effectsAudioControl;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           _effectsAudioControl = other.gameObject.GetComponentInChildren<EffectsAudioControl>();
           _effectsAudioControl.PlayCollisionSound("Portal");
        }
        other.transform.position = new Vector3(target.transform.position.x + 1 * direction, target.transform.position.y - 0.5f);
    }
}
