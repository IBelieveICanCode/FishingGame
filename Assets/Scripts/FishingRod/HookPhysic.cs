using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPhysic : MonoBehaviour
{
    private Transform ConnectedRigidbody;
    private float Distance;
    private float Spring = 0.1f;
    private float Damper = 1f;
    private float speed = 5f;
    private Rigidbody Rigidbody;

    void Start()
    {
        ConnectedRigidbody = transform.parent;
        Rigidbody = GetComponent<Rigidbody>();
        Distance = Vector3.Distance(Rigidbody.position, ConnectedRigidbody.position);
    }

    void LateUpdate()
    {
        Vector3 connection = Rigidbody.position - ConnectedRigidbody.position;
        float distanceDiscrepancy = Distance - connection.magnitude;
        Rigidbody.position += distanceDiscrepancy * connection.normalized;
        Vector3 velocityTarget = connection + (Rigidbody.velocity + Physics.gravity * Spring);
        Vector3 projectOnConnection = Vector3.Project(velocityTarget, connection);
        Rigidbody.velocity = (velocityTarget - projectOnConnection) / (1 + Damper * Time.fixedDeltaTime);
    }
}
