using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    GameObject playerCamPoint;

    [SerializeField]
    GameObject playerCamOBJ;

    [SerializeField] 
    GameObject feetFinder;

    [SerializeField]
    GameObject weaponHolder;

    [SerializeField] 
    LayerMask layerMask;

    [SerializeField]
    LayerMask camLayerMask;

    [Header("PlayerStats")]
    [SerializeField]
    float moveSpeed = 200;
    [SerializeField]
    float rotationSpeed = 10;
    [SerializeField]
    float dodgeSpeed = 10;
    [SerializeField]
    float dodgeTime = 1;
    [SerializeField]
    float maxCheckForSlopeDistance = 0.7f;
    [SerializeField]
    float jumpHeight = 20;

    Rigidbody rb;
    Animator animator;
    Animator wepAnimator;

    [Header("Usage")]
    float pitch = 0f; //used to stop the player from looking all the way around
    public bool dodging = false;
    public bool blocking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        wepAnimator = weaponHolder.transform.GetChild(0).GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        /*
        #region look
        float rotationVer = Input.GetAxis("Mouse Y") * rotationSpeed;
        float rotationHor = Input.GetAxis("Mouse X") * rotationSpeed;

        playerCamPoint.transform.RotateAround(transform.position, Vector3.left, rotationHor);

        pitch -= rotationVer;
        pitch = Mathf.Clamp(pitch, -80f, 90f);

        playerCamPoint.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        bool camHit = Physics.Raycast(playerCamPoint.transform.position, -playerCamPoint.transform.forward, out RaycastHit rayInfo, 6, camLayerMask);

        if (camHit) playerCamOBJ.transform.localPosition = new Vector3 (0,1,-rayInfo.distance);
        else playerCamOBJ.transform.localPosition = new Vector3(0, 1, -6);
        #endregion
        */

        if (dodging) return; //anything below this cant be done while dodging

        #region block
        if (Input.GetKey(KeyCode.F))
        {
            blocking = true;
            wepAnimator.SetBool("Blocking", true);
        }
        else 
        { 
            blocking = false;
            wepAnimator.SetBool("Blocking", false);
        }
        #endregion

        #region walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Get camera-relative movement direction
        Vector3 forward = Vector3.ProjectOnPlane(playerCamOBJ.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(playerCamOBJ.transform.right, Vector3.up).normalized;


        Vector3 dir = forward * vertical + right * horizontal;
        dir = Vector3.ClampMagnitude(dir, 1f);

        //find ground:D
        bool grounded = Physics.Raycast(feetFinder.transform.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit ray, maxCheckForSlopeDistance, layerMask);

        //move like a slope slope sahur
        if (grounded)
        {
            dir = Vector3.ProjectOnPlane(dir, ray.normal).normalized;
            rb.linearVelocity = dir * moveSpeed;
        }
        else
        {
            //keep vertical velocity when in air
            Vector3 velocity = rb.linearVelocity;
            velocity.x = dir.x * moveSpeed;
            velocity.z = dir.z * moveSpeed;
            rb.linearVelocity = velocity;
        }
        #endregion

        #region jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool rayFloor = Physics.Raycast(gameObject.transform.position, Vector3.down, out RaycastHit hitFloor, 1.1f, layerMask);
            if (rayFloor) GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
        #endregion

        #region dodge
        if (Input.GetKey(KeyCode.Q) && !blocking)
        {
            dodging = true;

            Vector3 dodgeDir = new Vector3();
            if (Input.GetKey(KeyCode.W)) {dodgeDir = transform.forward; animator.SetTrigger("DodgeFront");}
            else if (Input.GetKey(KeyCode.D)) {dodgeDir = transform.right; animator.SetTrigger("DodgeRight");}
            else if (Input.GetKey(KeyCode.A)) {dodgeDir = -transform.right; animator.SetTrigger("DodgeLeft");}
            else { dodgeDir = -transform.forward; animator.SetTrigger("DodgeBack"); }

            rb.linearVelocity = dodgeDir * dodgeSpeed;

            StartCoroutine(DodgeWait());
        }
        #endregion
    }

    IEnumerator DodgeWait()
    {
        yield return new WaitForSeconds(dodgeTime);
        dodging = false;
    }
}
