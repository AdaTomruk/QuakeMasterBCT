using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothmovecam : MonoBehaviour
{
    public Vector3 initialCameraPosition = new Vector3(0, 0, -5); // Yeni başlangıç pozisyonunu burada ayarlayın.

    public Transform target;
    public Camera mainCamera;
    [Range(0.1f, 5f)]
    [Tooltip("How sensitive the mouse drag to camera rotation")]
    public float mouseRotateSpeed = 0.8f;
    [Range(0.01f, 100)]
    [Tooltip("How sensitive the touch drag to camera rotation")]
    public float touchRotateSpeed = 17.5f;
    [Tooltip("Smaller positive value means smoother rotation, 1 means no smooth apply")]
    public float slerpValue = 0.25f;
    public enum RotateMethod { Mouse, Touch };
    [Tooltip("How do you like to rotate the camera")]
    public RotateMethod rotateMethod = RotateMethod.Mouse;


    public Vector2 swipeDirection; //swipe delta vector2
    public Quaternion cameraRot; // store the quaternion after the slerp operation
    public Touch touch;
    public float distanceBetweenCameraAndTarget;

    public float minXRotAngle = -80; //min angle around x axis
    public float maxXRotAngle = 80; // max angle around x axis

    //Mouse rotation related
    public float rotX; // around x
    public float rotY; // around y
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        distanceBetweenCameraAndTarget = Vector3.Distance(mainCamera.transform.position, target.position);

        distanceBetweenCameraAndTarget = Vector3.Distance(mainCamera.transform.position, target.position);

        // Başlangıç pozisyonunu ayarla
        mainCamera.transform.position = target.position + initialCameraPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateMethod == RotateMethod.Mouse)
        {
            if (Input.GetMouseButton(1))
            {
                rotX += -Input.GetAxis("Mouse Y") * mouseRotateSpeed; // around X
                rotY += Input.GetAxis("Mouse X") * mouseRotateSpeed;
            }

            if (rotX < minXRotAngle)
            {
                rotX = minXRotAngle;
            }
            else if (rotX > maxXRotAngle)
            {
                rotX = maxXRotAngle;
            }
        }
        else if (rotateMethod == RotateMethod.Touch)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    //Debug.Log("Touch Began");

                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    swipeDirection += touch.deltaPosition * Time.deltaTime * touchRotateSpeed;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    //Debug.Log("Touch Ended");
                }
            }

            if (swipeDirection.y < minXRotAngle)
            {
                swipeDirection.y = minXRotAngle;
            }
            else if (swipeDirection.y > maxXRotAngle)
            {
                swipeDirection.y = maxXRotAngle;
            }
        }

    }

    private void LateUpdate()
    {

        Vector3 dir = new Vector3(0, 0, -distanceBetweenCameraAndTarget); //assign value to the distance between the maincamera and the target

        Quaternion newQ; // value equal to the delta change of our mouse or touch position
        if (rotateMethod == RotateMethod.Mouse)
        {
            newQ = Quaternion.Euler(rotX, rotY, 0); //We are setting the rotation around X, Y, Z axis respectively
        }
        else
        {
            newQ = Quaternion.Euler(swipeDirection.y, -swipeDirection.x, 0);
        }
        cameraRot = Quaternion.Slerp(cameraRot, newQ, slerpValue);  //let cameraRot value gradually reach newQ which corresponds to our touch
        mainCamera.transform.position = target.position + cameraRot * dir;
        mainCamera.transform.LookAt(target.position);

    }

    public void SetCamPos()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        mainCamera.transform.position = new Vector3(0, 0, -distanceBetweenCameraAndTarget);
    }
}
