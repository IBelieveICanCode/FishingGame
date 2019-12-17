using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGravity : MonoBehaviour
{
    public Transform ConnectedRigidbody;
    private float Distance;
    public float Spring = 0.1f;
    public float Damper = 5f;
    private Rigidbody Rigidbody;

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Distance = Vector3.Distance(Rigidbody.position, ConnectedRigidbody.position);
    }
    void FixedUpdate()
    {
        Vector3 connection = Rigidbody.position - ConnectedRigidbody.position;
        float distanceDiscrepancy = Distance - connection.magnitude;
        Rigidbody.position += distanceDiscrepancy * connection.normalized;
        Vector3 velocityTarget = connection + (Rigidbody.velocity + Physics.gravity * Spring);
        Vector3 projectOnConnection = Vector3.Project(velocityTarget, connection);
        Rigidbody.velocity = (velocityTarget - projectOnConnection) / (1 + Damper * Time.fixedDeltaTime);       
    }
}
