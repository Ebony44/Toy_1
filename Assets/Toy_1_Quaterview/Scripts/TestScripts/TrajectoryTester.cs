// https://learn.unity.com/tutorial/calculating-trajectories#
// testing trajectory simulation

using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TrajectoryTester : MonoBehaviour
{

    public GameObject shooter;
    public GameObject target;

    public GameObject bulletPrefab;
    public GameObject explosion;

    public Transform shooterTrans;
    // public PlayerInput playerInput;
    private GameInput gameInput;

    public GameObject bulletIndicator;

    [SerializeField] float speed = 25f;
    [SerializeField] float rotSpeed = 8f;

    [SerializeField] bool bIsUpdateLogicEnabled = true;

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

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(shooterTrans == null && shooter != null)
        {
            shooterTrans = shooter.transform;
        }

        
        
        
    }
    private void OnEnable()
    {
        if(gameInput == null)
        {
            gameInput = new GameInput();
        }
        gameInput.Enable();
        gameInput.Player.Space.performed += OnPressSpace;
        // gameInput.Player.Space.started += OnPressSpace;
        // difference between performed and started is that performed is called when the button is pressed down,
        // while started is called when the button is pressed down and held.
        
    }
    private void OnDisable()
    {
        if(gameInput != null)
        {
            gameInput.Player.Space.performed -= OnPressSpace;
            // gameInput.Player.Space.started -= OnPressSpace;
            gameInput.Disable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!bIsUpdateLogicEnabled)
        {
            return;
        }
        #region physics based trajectory
        //LookOnTo();
        //Rotate();
        //CalculateAngle(true); // Calculate the angle for low trajectory
        #endregion physics based trajectory end

        #region input based trajectory
        //if(bulletIndicator == null)
        //{
        //    bulletIndicator = Instantiate(bulletPrefab, shooterTrans.position, Quaternion.identity);
        //}

        //bulletIndicator.transform.position =
        //CalculateBulletVector(shooterTrans.position, target.transform.position, Time.deltaTime, 5f);
        #endregion input based trajectory end

    }
    public void OnPressSpace(InputAction.CallbackContext callbackContext)
    {
        // bool isPressed = callbackContext.performed || callbackContext.started;
        float pressValue = callbackContext.ReadValue<float>();
        //float currentAxis = callbackContext.ReadValue<float>();
        Debug.Log("Space Pressed: " + pressValue);
        //scrollInput.y = currentAxis > 0 ? 1
        //    : currentAxis < 0 ? -1
        //    : 0;
        // FireBulletWithPhysics();
        // FireBulletWithInputBase();
        StartCoroutine(CalculateBulletVectorRoutine(shooterTrans.position, target.transform.position, 5f));
    }

    public void LookOnTo()
    {
        Vector3 direction = (target.transform.position - shooter.transform.position).normalized;
        // Quaternion lookRot = Quaternion.LookRotation(direction);
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        shooter.transform.rotation = Quaternion.Slerp(shooter.transform.rotation, lookRot, Time.deltaTime * rotSpeed);
        // float? angle = Rotate();


    }
    float? Rotate()
    {
        float? angle = CalculateAngle(true);
        Debug.Log($"Calculated angle: {angle} degrees");
        if (angle.HasValue)
        {
            shooterTrans.localEulerAngles = new Vector3(360 - (float)angle, 0, 0); // Adjust the angle to match Unity's coordinate system
            
            //shooterTrans.localEulerAngles = new Vector3(360 - (float)angle,
            //    shooterTrans.transform.localEulerAngles.y,
            //    shooterTrans.transform.localEulerAngles.z); // Adjust the angle to match Unity's coordinate system

            //shooterTrans.localEulerAngles = new Vector3(0, 360 - (float)angle, 0); // Adjust the angle to match Unity's coordinate system
            //Quaternion targetRotation = Quaternion.Euler(0, angle.Value, 0);
            //shooter.transform.rotation = Quaternion.Slerp(shooter.transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
        }
        else
        {
            Debug.LogError("Failed to calculate angle for rotation.");
        }
        return angle;
    }

    private float? CalculateAngle(bool low)
    {
        float result = 0f;
        Vector3 targetDir = target.transform.position - shooter.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f; // Ignore the y component for horizontal angle calculation
        // float x = targetDir.magnitude; // dot magitude
        float x = targetDir.magnitude - 1.0f;
        float gravity = -Physics.gravity.y; // Assuming gravity is negative in Unity
        // Debug.Log($"Target Direction: {targetDir}, Y: {y}, X: {x}, Gravity: {gravity}");
        float sSqr = speed * speed;
        // result will be 2 possible angles, one for low and one for high trajectory
        // Tan(a) = sSqr + root of (sSqr^2 - g * (g * x^2 + 2 * y * sSqr)) / (g * x)
        // Tan(a) = sSqr - root of (sSqr^2 - g * (g * x^2 + 2 * y * sSqr)) / (g * x)

        // float underTheSqrRoot = sSqr * sSqr - gravity * ( (gravity * x * x) + (2f * y * sSqr) );
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);
        Debug.Log("magnitude: " + targetDir.magnitude + ", x: " + x + ", y: " + y + ", gravity: " 
            + gravity + ", sSqr: " + sSqr + ", underTheSqrRoot: " + underTheSqrRoot);
        if (underTheSqrRoot < 0f)
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

        // return result;
        return null;
    }

    public void FireBulletWithPhysics()
    {
        GameObject currentBullet = Instantiate(bulletPrefab, shooterTrans.position, shooterTrans.rotation);
        currentBullet.GetComponent<Rigidbody>().linearVelocity =  speed * shooterTrans.forward; // Set the bullet's velocity in the direction the shooter is facing

    }
    public void FireBulletWithInputBase()
    {
        if (bulletIndicator == null)
        {
            bulletIndicator = Instantiate(bulletPrefab, shooterTrans.position, Quaternion.identity);
        }
        // Calculate the bullet's trajectory vector based on the current time and arrival time
        Vector3 bulletPosition = CalculateBulletVector(shooterTrans.position, target.transform.position, Time.deltaTime, 5f);
        bulletIndicator.transform.position = bulletPosition;
        // Optionally, you can also set the rotation of the bullet indicator to face the target
        // Vector3 direction = (target.transform.position - shooterTrans.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(direction);
        //bulletIndicator.transform.rotation = lookRotation;
    }

    private IEnumerator CalculateBulletVectorRoutine(Vector3 start, Vector3 end, float arriveTime)
    {
        if (bulletIndicator == null)
        {
            bulletIndicator = Instantiate(bulletPrefab, shooterTrans.position, Quaternion.identity);
        }
        var currentTime = 0f;
        var clampedTime = 0f;
        Vector3 midPoint = (start + end) / 2f;
        midPoint.y += 3f; // Adjust the y value to be slightly above the midpoint
        // please move bulletIndicator to end position
        while (clampedTime < 1f)
        {
            // Calculate the bullet's trajectory vector based on the current time and arrival time
            // Vector3 bulletPosition = CalculateBulletVector(start, end, currentTime, arriveTime);

            var startVector = Vector3.Lerp(start, midPoint, clampedTime);
            var endVector = Vector3.Lerp(midPoint, end, clampedTime);
            var result = Vector3.Lerp(startVector, endVector, clampedTime);

            bulletIndicator.transform.position = result;
            // Optionally, you can also set the rotation of the bullet indicator to face the target
            Vector3 direction = (end - start).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            bulletIndicator.transform.rotation = lookRotation;
            clampedTime += Time.deltaTime / arriveTime; // Increment current time based on the arrival time
            yield return null; // Wait for the next frame
        }
        Debug.Log("Bullet reached the target position.");
        yield return new WaitForSeconds(0.1f); // Wait for a moment before deactivating the bullet indicator
        bulletIndicator.SetActive(false); // Optionally deactivate the bullet indicator after reaching the target
        yield return null;
    }
    private Vector3 CalculateBulletVector(Vector3 start, Vector3 end, float currentTime, float arriveTime)
    {
        // generate mid point between start and end
        // with slightly positive of y value?
        Vector3 midPoint = (start + end) / 2f;
        midPoint.y += 3f; // Adjust the y value to be slightly above the midpoint

        //then vector lerp with mid point?
        // float currentTime = 0f;
        var startVector = Vector3.Lerp(start, midPoint, currentTime);
        var endVector = Vector3.Lerp(midPoint, end, currentTime);
        var result = Vector3.Lerp(startVector, endVector, currentTime);

        // above adjust vectors with arrive time
        if (arriveTime > 0f)
        {
            startVector = Vector3.Lerp(start, midPoint, currentTime / arriveTime);
            endVector = Vector3.Lerp(midPoint, end, currentTime / arriveTime);
            result = Vector3.Lerp(result, end, currentTime / arriveTime);
            
        }

        return result;

    }

    // https://foo897.tistory.com/24
    #region rip from 
    void LaucherProjecttile()
    {
        Vector3 Vo = CalculateVelcoity(target.transform.position, transform.position, 1f);
        // Rigidbody obj = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        Rigidbody obj = Instantiate(bulletPrefab, target.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        obj.linearVelocity = Vo;
        //setTrajectoryPoints(transform.position, Vo);
        DrawPath(Vo);
    }

    //�� ����� ��ǥ ���Ϳ� ������ �������� �ʿ��մϴ�.
    //time : ����ð�
    Vector3 CalculateVelcoity(Vector3 target, Vector3 origin, float time)
    {
        //define the distance x and y first
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance; //x��z�� ����̸� �⺻������ �Ÿ��� ���� ����
        distanceXZ.y = 0f;//y�� 0���� ����

        //create a float the represent our distance
        float Sy = distance.y;//���� ������ �Ÿ��� ����
        float Sxz = distanceXZ.magnitude;

        //�ӵ� ���
        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        //������� ���� ������ �ʱ� �ӵ� ������ ���ο� ���͸� ����� ����
        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }

    void DrawPath(Vector3 velocity)
    {
        Vector3 previousDrawPoint = transform.position;
        int resolution = 30;
        //lineRenderer.positionCount = resolution;
        for (int i = 1; i <= resolution; i++)
        {
            //float simulationTime = i / (float)resolution * launchData.timeToTarget;
            float simulationTime = i / (float)resolution * 1f;

            Vector3 displacement = velocity * simulationTime + Vector3.up * Physics.gravity.y * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = transform.position + displacement;
            // DebugExtension.DebugPoint(drawPoint, 1, 1000f);//����Ƽ ���½���� Debug Extension
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            //lineRenderer.SetPosition(i - 1, drawPoint);
            previousDrawPoint = drawPoint;
        }
    }
    #endregion

}
