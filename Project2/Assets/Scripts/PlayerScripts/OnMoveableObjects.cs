using UnityEngine;

public class OnMoveableObjects: MonoBehaviour
{
    public float pushForce = 4.0f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
        {
            return;
        }

        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }
        
        body.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
     
        Vector3 pushDir = hit.gameObject.transform.position - transform.position;
        pushDir.y = 0.0f;
        pushDir.Normalize();

        body.AddForceAtPosition(pushDir * pushForce, transform.position, ForceMode.Impulse);
    }
}