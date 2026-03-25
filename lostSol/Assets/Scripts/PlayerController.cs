using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerCam;

    Rigidbody rb;
    float moveSpeed = 200;
    float rotationSpeed = 100;


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

        rb.linearVelocity = dir * moveSpeed * Time.deltaTime;


        //camera
        float rotationVer = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        float rotationHor = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        transform.Rotate(0, rotationHor, 0);
        playerCam.transform.Rotate(rotationVer, 0, 0);

    }
}
