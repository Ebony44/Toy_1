// https://learn.unity.com/tutorial/calculating-trajectories#
// testing trajectory simulation

using UnityEngine;
using UnityEngine.UIElements;

public class TrajectoryTester : MonoBehaviour
{

    public GameObject shooter;
    public GameObject target;

    public GameObject bulletPrefab;
    public GameObject explosion;

    public Transform shooterTrans;

    SerializeField] float speed = 15f;
    [SerializeField] float rotSpeed = 2f;

    //[SerializeField] float speed = 0f;
    //[SerializeField] float ySpeed = 0f;
    //[SerializeField] float mass = 0f;
    //[SerializeField] float force = 0f;
    //[SerializeField] float drag = 0f;
    //[SerializeField] float gravity = -9.8f;

    //float gAccel;
    //float acceleration;




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Instantiate explosion effect at the collision point
            if(explosion != null)
            {
                GameObject exp = Instantiate(explosion, collision.contacts[0].point, Quaternion.identity);
                Destroy(exp, 2f); // Destroy the explosion effect after 2 seconds
            }
            // Destroy the projectile after collision
            Destroy(gameObject);
        }
    }

    public void LookOnTo()
    {
        Vector3 direction = target.transform.position - this.transform.position.normalized;
        // Quaternion lookRot = Quaternion.LookRotation(direction);
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRot, Time.deltaTime * rotSpeed);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(shooterTrans == null && shooter != null)
        {
            shooterTrans = shooter.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestTrajectory()
    {
        // vx = v0 * cos(еш)
        // vy = v0 * sin(еш) - (g * t)

        // speed = root of (vx^2 + vy^2)

        
    }
    public void FireBullet()
    {
        GameObject currentBullet = Instantiate(bulletPrefab, shooterTrans.position, shooterTrans.rotation);
        currentBullet.GetComponent<Rigidbody>().angularVelocity =  speed * shooterTrans.forward; // Set the bullet's velocity in the direction the shooter is facing

    }

    private float? CalculateAngle(bool low)
    {
        float result = 0f;
        Vector3 targetDir = target.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f; // Ignore the y component for horizontal angle calculation
        // float x = targetDir.magnitude; // dot magitude
        float x = targetDir.magnitude - 1;
        float gravity = -Physics.gravity.y; // Assuming gravity is negative in Unity
        Debug.Log($"Target Direction: {targetDir}, Y: {y}, X: {x}, Gravity: {gravity}");
        float sSqr = speed * speed;
        // result will be 2 possible angles, one for low and one for high trajectory
        // Tan(a) = sSqr + root of (sSqr^2 - g * (g * x^2 + 2 * y * sSqr)) / (g * x)
        // Tan(a) = sSqr - root of (sSqr^2 - g * (g * x^2 + 2 * y * sSqr)) / (g * x)
        float underTheSqrRoot = sSqr * sSqr - gravity * ( (gravity * x * x) + (2f * y * sSqr) );

        if(underTheSqrRoot < 0f)
        {
            Debug.LogError("No valid angle found for the given parameters. Under the square root is negative.");
            return null;
        }
        else if(underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;
            if(low)
            {
                return Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg; // Convert to degrees
            }
            else
            {
                // return Mathf.Atan(highAngle / (gravity * x)) * Mathf.Rad2Deg; // Convert to degrees
                return Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg; // Convert to degrees
            }
            // float tanAngle = (sSqr + Mathf.Sqrt(underTheSqrRoot)) / (gravity * x);
        }

        return result;
    }

    void Rotate()
    {
        float? angle = CalculateAngle(true);
        if(angle.HasValue)
        {
            shooterTrans.localEulerAngles = new Vector3(360 - (float)angle.Value, 0, 0); // Adjust the angle to match Unity's coordinate system
            //Quaternion targetRotation = Quaternion.Euler(0, angle.Value, 0);
            //shooter.transform.rotation = Quaternion.Slerp(shooter.transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
        }
        else
        {
            Debug.LogError("Failed to calculate angle for rotation.")
        }
    }


}
