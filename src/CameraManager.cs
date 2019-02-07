using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using dPatch;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraManager : BaseKoaUScriptEvent, IBackupHandler {

    void Update() {
        if (Input.GetKeyDown(KeyCode.Numlock)) {
            cheatCameraEnabled = !cheatCameraEnabled;
            lastFramePosition = m_mainCamera.transform.position;
            if (cheatCameraEnabled) {
                cameraStartingPosition = m_mainCamera.transform.position;
                cameraStartingRotation = m_mainCamera.transform.eulerAngles;
                cameraStartingFOV = m_mainCamera.fov;
                cameraXAngle = m_mainCamera.transform.eulerAngles.y;
                cameraYAngle = m_mainCamera.transform.eulerAngles.x;
            } else {
                m_mainCamera.transform.position = cameraStartingPosition;
                m_mainCamera.transform.eulerAngles = cameraStartingRotation;
            }
        }
        // ...
    }

    void LateUpdate() {
        // ...
        if (cheatCameraEnabled) {
            CheatCamera();
        }
    }

    public void CheatCamera() {
        Camera camera = m_mainCamera;

        // Camera Rotation
        cameraXAngle += cameraSpeed * Input.GetAxis("Mouse X");
        cameraYAngle -= cameraSpeed * Input.GetAxis("Mouse Y");
        Mathf.Clamp(cameraYAngle, minCameraAngle, maxCameraAngle);
        camera.transform.eulerAngles = new Vector3(cameraYAngle, cameraXAngle, 0f);

        // Camera Field of View
        if (Input.GetKey(KeyCode.KeypadMinus)) {
            cameraStartingFOV -= changeRateFOV * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.KeypadPlus)) {
            cameraStartingFOV += changeRateFOV * Time.deltaTime;
        }
        camera.fov = cameraStartingFOV;

        // Camera Translation
        camera.transform.position = lastFramePosition;
        float speedModifier = 1f;
        if (Input.GetKey(KeyCode.RightControl)) { // boost
            speedModifier = 11f;
        }
        if (Input.GetKey(KeyCode.Home)) { // forwards
            Vector3 direction = camera.transform.forward;
            direction.Normalize();
            camera.transform.position += Time.deltaTime * speedModifier * direction;
        }
        if (Input.GetKey(KeyCode.End)) { // backwards
            Vector3 direction = camera.transform.forward;
            direction.Normalize();
            camera.transform.position -= Time.deltaTime * speedModifier * direction;
        }
        if (Input.GetKey(KeyCode.Delete)) { // left
            Vector3 direction = camera.transform.right;
            direction.Normalize();
            camera.transform.position -= Time.deltaTime * speedModifier * direction;
        }
        if (Input.GetKey(KeyCode.PageDown)) { // right
            Vector3 direction = camera.transform.right;
            direction.Normalize();
            camera.transform.position += Time.deltaTime * speedModifier * direction;
        }
        if (Input.GetKey(KeyCode.Insert)) { // down
            Vector3 direction = camera.transform.up;
            direction.Normalize();
            camera.transform.position -= Time.deltaTime * speedModifier * direction;
        }
        if (Input.GetKey(KeyCode.PageUp)) { // up
            Vector3 direction = camera.transform.up;
            direction.Normalize();
            camera.transform.position += Time.deltaTime * speedModifier * direction;
        }
        lastFramePosition = camera.transform.position;
    }

    [HideInInspector]
    public Camera m_mainCamera;

    bool cheatCameraEnabled;
    Vector3 lastFramePosition;
    Vector3 cameraStartingPosition;
    Vector3 cameraStartingRotation;
    float cameraStartingFOV;
    float cameraXAngle;
    float cameraYAngle;
    const float minCameraAngle = -50f;
    const float maxCameraAngle = 50f;
    const float cameraSpeed = 0.5f;
    const float changeRateFOV = 7f;
}
