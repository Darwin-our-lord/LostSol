using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    GameObject playerCam;

    [SerializeField] 
    GameObject feetFinder;

    [SerializeField] 
    LayerMask layerMask;

    Rigidbody rb;

    [Header("PlayerStats")]
    [SerializeField]
    float moveSpeed = 200;
    [SerializeField]
    float rotationSpeed = 100;
    [SerializeField]
    float maxCheckForSlopeDistance = 0.7f;
    [SerializeField]
    float jumpHeight = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //move
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

    
        Vector3 dir = transform.forward * vertical + transform.right * horizontal;

        bool hit = Physics.Raycast(feetFinder.transform.position, dir, out RaycastHit ray, maxCheckForSlopeDistance, layerMask);

        dir = Vector3.Normalize(dir+ray.normal) * moveSpeed;

        dir.y = rb.linearVelocity.y;
        rb.linearVelocity = dir;

        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool rayFloor = Physics.Raycast(gameObject.transform.position, Vector3.down, out RaycastHit hitFloor, 1.1f, layerMask);
            if (rayFloor) GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }


        //camera
        float rotationVer = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        float rotationHor = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        transform.Rotate(0, rotationHor, 0);
        playerCam.transform.Rotate(rotationVer, 0, 0);

    }
}
