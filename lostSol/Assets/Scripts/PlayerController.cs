using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject feetFinder;

    public LayerMask layerMask;

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

        dir = Vector3.Normalize(dir+ray.normal);

        rb.linearVelocity = dir * moveSpeed * Time.deltaTime;

        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0,jumpHeight,0));
        }


        //camera
        float rotationVer = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        float rotationHor = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        transform.Rotate(0, rotationHor, 0);
        playerCam.transform.Rotate(rotationVer, 0, 0);

    }
}
