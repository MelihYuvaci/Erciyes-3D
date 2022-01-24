using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Metrics")]
    public float damp;
    [Range(1, 20)]
    public float rotationSpeed;
    float normalFov;
    public float SprintFov;

    float inputX;
    float inputY;
    float maxSpeed;

    public Transform Model;

    Animator Anim;
    Vector3 StickDirection;
    Camera mainCam;

    public KeyCode SprintButton = KeyCode.LeftShift;
    public KeyCode WalkButton = KeyCode.C


; void Start()
    {
        Anim = GetComponent<Animator>();
        mainCam = Camera.main;
        normalFov = mainCam.fieldOfView;
    }

    private void LateUpdate()
    {

        InputMove();
        InputRotation();
        Movement();
    }
    void Movement()
    {
        StickDirection = new Vector3(inputX, 0, inputY);

        if (Input.GetKey(SprintButton))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, SprintFov, Time.deltaTime * 2);

            maxSpeed = 2;
            inputX = 2 * Input.GetAxis("Horizontal");
            inputY = 2 * Input.GetAxis("Vertical");
        }
        else if (Input.GetKey(WalkButton))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);

            maxSpeed = 0.2f;
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
        }
        else
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);

            maxSpeed = 1;
            inputX =Input.GetAxis("Horizontal");
            inputY =Input.GetAxis("Vertical");

        }
    }
    void InputMove()
    {
        Anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
    }
    void InputRotation()
    {
        Vector3 rotOfset = mainCam.transform.TransformDirection(StickDirection);
        rotOfset.y = 0;

        Model.forward = Vector3.Slerp(Model.forward, rotOfset, Time.deltaTime * rotationSpeed);




    }
}
