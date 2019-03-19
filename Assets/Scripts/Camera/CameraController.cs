using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private bool freeCam = true;
    [SerializeField]
    private float minZoomFOV;
    [SerializeField]
    private float maxZoomFOV;
    [SerializeField]
    private Vector3 beginPos;
    [SerializeField]
    private float zoomSensitivity;
    [SerializeField]
    private float fov;
    private const float EPESILON = .1f;
    private Vector3 startAngle;
    private float zoomReset = 2;
    private float step = 5;
    
    [Header("Camera rotation movement")]
    [SerializeField]
    private float velocityX;
    [SerializeField]
    private float velocityY;

    [SerializeField]
    private float finalX;
    [SerializeField]
    private float finalY;

    [SerializeField]
    private float xSpeed;
    [SerializeField]
    private float ySpeed;

    [SerializeField]
    private float yMinLimit = -30f;
    [SerializeField]
    private float yMaxLimit = 30f;
    [SerializeField]
    private float xMinLimit = -30f;
    [SerializeField]
    private float xMaxLimit = 30f;

    public float smoothTime = 2f;

    public float rotationYAxis = 0.0f;
    public float rotationXAxis = 0.0f;

    private void Start() {
        fov = Camera.main.fieldOfView;
        startAngle = transform.eulerAngles;
    }

    private void FixedUpdate() { 
        Zoom();
        MoveCameraWithMouse();
        if (Input.GetKeyDown(KeyCode.T)) {
            ResetZoom();
        }
    }

    private void MoveCameraWithMouse() {
        if (Input.GetMouseButton(0)) {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * xSpeed * Time.deltaTime, -Input.GetAxis("Mouse X") * ySpeed * Time.deltaTime, 0));
            velocityX = transform.rotation.eulerAngles.x;
            velocityY = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(velocityX, velocityY, 0);
           finalX = ClampAngle(velocityX, xMinLimit, xMaxLimit);
           finalY = ClampAngle(velocityY, xMinLimit, xMaxLimit);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(velocityX, velocityY, 0), Time.time * .01f);
        }
    }

    private void Zoom() {
        if (freeCam) {
            fov += -Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
            fov = Mathf.Clamp(fov, minZoomFOV, maxZoomFOV);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, step * Time.deltaTime);
        }
    }

    private void ResetZoom() {
        StartCoroutine(ResetZoomStep());
    }

    private IEnumerator ResetZoomStep() {
        freeCam = false;
        do {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, maxZoomFOV, zoomReset * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        } while (Camera.main.fieldOfView <= (maxZoomFOV - EPESILON));
        fov = maxZoomFOV;
        freeCam = true;
    }

    public float ClampAngle(float angle, float min, float max) {
        if(angle > xMinLimit) {
            angle = (360 + angle) / 2;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
