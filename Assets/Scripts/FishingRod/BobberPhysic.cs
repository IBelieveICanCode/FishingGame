using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BobberPhysic: MonoBehaviour
{
    public Transform ConnectedRigidbody;
    public bool ThrowingRodInWater = false;
    public float Distance;
    public float Spring = 0.1f;
    public float Damper = 5f;

    [HideInInspector]
    public Rigidbody Rigidbody;



    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        ConnectedRigidbody = FishingControl.Instance.Rod.whatTheRopeIsConnectedTo;
        Distance = Vector3.Distance(Rigidbody.position, ConnectedRigidbody.position);
    }

    void FixedUpdate()
    {
        Vector3 connection = Rigidbody.position - ConnectedRigidbody.position;
        float distanceDiscrepancy = Distance - connection.magnitude;
        if (!ThrowingRodInWater)
        {
            Rigidbody.position += distanceDiscrepancy * connection.normalized;
            Vector3 velocityTarget = connection + (Rigidbody.velocity + Physics.gravity * Spring);
            Vector3 projectOnConnection = Vector3.Project(velocityTarget, connection);
            Rigidbody.velocity = (velocityTarget - projectOnConnection) / (1 + Damper * Time.fixedDeltaTime);
        }
    }

    public void BobberInWater()
    {
        
        Rigidbody.velocity = Vector3.zero;
        //transform.Translate(Vector3.down * 0.1f);
        //Vector3 velocityTarget = Rigidbody.velocity + Physics.gravity;
    }

    public void CastBobber(float angle)
    {
        transform.rotation = Quaternion.identity;
        Transform target = FishingControl.Instance.Marker.transform;
        ThrowingRodInWater = true;

        Vector3 direction = target.position - transform.position;
        Vector3 directionXZ = new Vector3(direction.x, 0, direction.z);
        
        float x = directionXZ.magnitude;
        float y = direction.y;
        float g = Physics.gravity.magnitude;
        float rad = angle * Mathf.Deg2Rad;
        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(rad) * x) * Mathf.Pow(Mathf.Cos(rad), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));
        Rigidbody.velocity = direction.normalized * v;
    }

}