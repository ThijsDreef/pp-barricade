using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private bool freeCam = true;
    private float maxX;
    private float maxY;
    [SerializeField]
    private float minZoomFOV;
    [SerializeField]
    private float maxZoomFOV;
    private Vector3 beginPos;
    [SerializeField]
    private float zoomSensitivity = 10f;
    private float zoomReset = 2;
    [SerializeField]
    private float step;
    [SerializeField]
    private float fov;
    private const float EPESILON = .001f;

    private void Start() {
        fov = Camera.main.fieldOfView;
    }

    private void FixedUpdate() { 
        Zoom();
        if (Input.GetKeyDown(KeyCode.T)) {
            ResetZoom();
        }
    }

    private void MoveCameraWithMouse() {

    }

    private void Zoom() {
        if (freeCam) {
            fov += -Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity * Time.deltaTime;
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
}
