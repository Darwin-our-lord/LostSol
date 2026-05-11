using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerCamera : MonoBehaviour
{


    public float sensitivity = 17f;
    float speed = 5;

    [SerializeField] GameObject player;

    float minFov = 35f;
    float maxFov = 100f;

    private Transform playerTransform;
    private Vector3 offset;
    private float yOffset = 0.0f;
    private float zOffset = 2.0f;

    [SerializeField]
    LayerMask camLayerMask;

    [SerializeField]
    GameObject playerCamPoint;

    [SerializeField]
    GameObject playerCamOBJ;

    [Header("Usage")]
    float pitch = 0f; //used to stop the player from looking all the way around

    void Start()
    {
        playerTransform = player.transform;
        offset = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, playerTransform.position.z + zOffset);
    }

    void FixedUpdate()
    {

        transform.RotateAround(playerTransform.position, transform.up, Input.GetAxis("Mouse X") * speed);
        transform.RotateAround(playerTransform.position, transform.right, Input.GetAxis("Mouse Y") * -speed);

        //zoom

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;


        //playerCamPoint.transform.rotation = Quaternion.Euler(pitch, 0f, 0f);

        bool camHit = Physics.Raycast(playerCamPoint.transform.position, -playerCamPoint.transform.forward, out RaycastHit rayInfo, 6, camLayerMask);

        if (camHit) playerCamOBJ.transform.localPosition = new Vector3(0, 1, -rayInfo.distance);
        else playerCamOBJ.transform.localPosition = new Vector3(0, 1, -6);
    }
}
