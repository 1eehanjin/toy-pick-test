using UnityEngine;

public class CollisionResponse : MonoBehaviour
{
    public float pushForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRigidbody = collision.rigidbody;

        if (otherRigidbody != null)
        {
            Vector3 pushDirection = collision.contacts[0].point - transform.position;
            pushDirection = -pushDirection.normalized;

            otherRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}