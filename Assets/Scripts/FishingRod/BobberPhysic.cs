using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BobberPhysic: MonoBehaviour
{
    public Transform ConnectedRigidbody;
    public bool DetermineDistanceOnStart = true;
    public bool ThrowingRodInWater = false;
    public float Distance;
    public float Spring = 0.1f;
    public float Damper = 5f;
    private float speed = 5f;
    public Rigidbody Rigidbody;
    private LineRenderer line;

    private bool timer = true;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        
    }

    void Start()
    {
        //Rigidbody = GetComponent<Rigidbody>();
        ConnectedRigidbody = FishingControl.Instance.Rod.whatTheRopeIsConnectedTo;
        //if (DetermineDistanceOnStart && ConnectedRigidbody != null)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TerrainCollider>() != null)
        {
            Vector3 _dir =  - FishingControl.Instance.BitingFish.FishBehaviour.ChosenDirection.PossibleDir;
            FishingControl.Instance.BitingFish.FishBehaviour.ChosenDirection.PossibleDir = _dir;
        }
    }

    public void CanSeeBobber(bool active)
    {
        GetComponent<MeshRenderer>().enabled = active;
    }
    public void BobberInWater()
    {
        transform.eulerAngles = Vector3.zero;
        Rigidbody.velocity = Vector3.zero;
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

        float rotateAngle = Vector3.Angle(transform.position, direction);
        transform.eulerAngles = new Vector3(-angle, 0f, 0f);
    }
}