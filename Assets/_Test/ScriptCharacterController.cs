using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCharacterController : MonoBehaviour {

    [SerializeField]
    private Transform playerCam, character, centerPoint;
    [SerializeField]
    private float mouseYPosition = 1f;

    private float mouseX, mouseY;

    [SerializeField]
    private float mouseSensitivity = 10f;

    //private float moveFB, moveLR;

    //[SerializeField]
    //private float moveSpeed = 2f;

    private float zoom;
    [SerializeField]
    private float zoomSpeed = 4;
    [SerializeField]
    private float zoomMin = -2f;
    [SerializeField]
    private float zoomMax = -10f;

    [SerializeField]
    private float rotationSpeed = 5f;

    //jump
    [SerializeField]
    private float jumpVelocity = 5f;
    [SerializeField]
    private float fallMultiplier = 2.5f;
    [SerializeField]
    private float lowJumpMultiplier = 2f;

    private Rigidbody playerRigidbody;

    // Use this for initialization
    void Start () {
        zoom = -3;
        playerRigidbody = character.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (zoom > zoomMin)
        {
            zoom = zoomMin;
        }
        if (zoom < zoomMax)
        {
            zoom = zoomMax;
        }

        playerCam.transform.localPosition = new Vector3(0, 0, zoom);

        //if (Input.GetMouseButton(1))
        //{
        //    mouseX += Input.GetAxis("Mouse X");
        //    mouseY += Input.GetAxis("Mouse Y");
        //}

        mouseX += Input.GetAxis("Mouse X")*mouseSensitivity;
        mouseY -= Input.GetAxis("Mouse Y")*mouseSensitivity;


        mouseY = Mathf.Clamp(mouseY, -60f, 60f);
        playerCam.LookAt(centerPoint);
        centerPoint.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        //moveFB = Input.GetAxis("Vertical") * moveSpeed;
        //moveLR = Input.GetAxis("Horizontal") * moveSpeed;

        //Vector3 movement = new Vector3(moveLR, 0, moveFB);
        //movement = character.rotation * movement;

        //character.GetComponent<CharacterController>().Move(movement * Time.deltaTime);
        //jouer avec les forces
        //character.GetComponent<Rigidbody>().velocity += movement;
        //centerPoint.position = new Vector3(character.position.x, character.position.y + mouseYPosition, character.position.z);

        Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);

        //character.rotation = Quaternion.Slerp(character.rotation, turnAngle,Time.deltaTime*rotationSpeed);

        character.rotation = turnAngle;

        //if (Input.GetButtonDown("Jump"))
        //{
        //    //playerRigidbody.velocity += Vector3.up * jumpVelocity;
        //    playerRigidbody.AddForce(Vector3.up * jumpVelocity,ForceMode.Impulse);
        //}

        

	}

    private void FixedUpdate()
    {
        //if (playerRigidbody.velocity.y < 0)
        //{
        //    //playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //    playerRigidbody.AddForce(Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        //}
        //else if (playerRigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
        //{
        //    //playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //    playerRigidbody.AddForce(Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        //}
    }
}
