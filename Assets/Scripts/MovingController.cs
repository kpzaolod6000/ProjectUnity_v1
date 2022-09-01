using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class MovingController : MonoBehaviour
{
    
    private Rigidbody rb;
    private Vector2 inputMov;
    private Vector2 inputRot;
    private Transform camera;
    private Vector3 normalScale;
    private Vector3 modScale;
    private bool squat;


    //VARIABLES
    [SerializeField] public float velTalking = 10f;
    [SerializeField] public float velRun = 20f;
    [SerializeField] public float sensibilityMouse = 1;
    [SerializeField] public float rotX;
    [SerializeField] public float forceJump = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = transform.GetChild(0);
        rotX = camera.eulerAngles.x;

        normalScale = transform.localScale;
        modScale = normalScale;
        modScale.y = normalScale.y * .75f;

    }

    // Update is called once per frame
    void Update()
    {
        inputMov.x = Input.GetAxis("Horizontal");
        inputMov.y = Input.GetAxis("Vertical");

        inputRot.x = Input.GetAxis("Mouse X") * sensibilityMouse;
        inputRot.y = Input.GetAxis("Mouse Y") * sensibilityMouse;

        squat = Input.GetKey(KeyCode.C);

        if (Input.GetButtonDown("Jump")) rb.AddForce(0, forceJump, 0);

    }
    private void FixedUpdate()
    {
        bool value = Input.GetKey(KeyCode.LeftShift);
        float velocity = value ? velRun : velTalking;
        rb.velocity = transform.forward * velocity * inputMov.y //deslizamiento up y down
                      + transform.right * velocity * inputMov.x //deslizamiento right y left
                      + new Vector3(0, rb.velocity.y, 0);

        transform.rotation *= Quaternion.Euler(0, inputRot.x, 0);

        rotX -= inputRot.y;
        rotX = Mathf.Clamp(rotX, -50, 50);
        camera.localRotation = Quaternion.Euler(rotX,0, 0);

        //agacharse
        transform.localScale = squat ? modScale : normalScale;
    }
}
