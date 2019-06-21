using UnityEngine;

public class BobberPhysic: MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    public Transform ConnectedRigidbody;
    public bool DetermineDistanceOnStart = true;
    public bool ThrowingRodInWater = false;
    public float Distance;
    public float Spring = 0.1f;
    public float Damper = 5f;
    private float speed = 5f;
    private LineRenderer lineRenderer;
    public Rigidbody Rigidbody;
    bool caught = true;

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        if (DetermineDistanceOnStart && ConnectedRigidbody != null)
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
    public void CastBobber()
    {
        caught = true;
        ThrowingRodInWater = true;      
        Rigidbody.velocity = BallisticVel(GameController.Instance.Marker.transform, 30f);
    }

    public void BobberInWater()
    {
        Rigidbody.velocity = Vector3.down;
        Vector3 velocityTarget = Rigidbody.velocity + Physics.gravity;
    }

    public Vector3 BallisticVel(Transform target, float angle)
    {
        Vector3 direction = target.position - transform.position;
        Vector3 directionXZ = new Vector3(direction.x, 0, direction.z);
        
        float x = directionXZ.magnitude;
        float y = direction.y;
        float g = Physics.gravity.magnitude;
        float rad = angle * Mathf.Deg2Rad;
        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(rad) * x) * Mathf.Pow(Mathf.Cos(rad), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));
        return direction.normalized * v;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water" && GameController.Instance.Player.State == PlayerState.Cast)
        {
            GameController.Instance.Player.State = PlayerState.Wait;
        }
    }

    public void CatchingFish()
    {             
        if (caught)
        {
            anim.SetBool("Catched", true);
            GameController.Instance.Bending.Bending(true);
            caught = false;
        }
        GameController.Instance.Player.UpdateState(PlayerState.Catching);
    }

    public void Pull()
    {
        //Vector3 direction = GameController.Instance.Player.transform.position - transform.position;
        Vector3 direction = GameController.Instance.Line.whatTheRopeIsConnectedTo.position - transform.position;
        Vector3 directionXZ = new Vector3(direction.x, 0, direction.z);
        //transform.position = Vector3.Lerp(transform.position, directionXZ.normalized, Time.deltaTime * 0.1f);
        transform.Translate(directionXZ.normalized * Time.deltaTime);
        if ((GameController.Instance.Player.transform.position - transform.position).magnitude < 7f)
        {
            anim.SetBool("Catched", false);
            ThrowingRodInWater = false;
            GameController.Instance.Player.UpdateState(PlayerState.Idle);
            GameController.Instance.Marker.ChangeColor(Color.blue);
        }
    }


}