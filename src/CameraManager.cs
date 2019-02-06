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
            this.cheatCameraEnabled = !this.cheatCameraEnabled;
            this.lastFramePosition = this.m_mainCamera.transform.position;
        }
        // ...
    }

    void LateUpdate() {
        // ...
        if (this.cheatCameraEnabled) {
            this.CheatCamera();
        }
    }

    public void CheatCamera() {
        Camera camera = this.m_mainCamera;

        // Camera Rotation
        this.cameraXAngle += cameraSpeed * Input.GetAxis("Mouse X");
        this.cameraYAngle -= cameraSpeed * Input.GetAxis("Mouse Y");
        Mathf.Clamp(this.cameraYAngle, minCameraAngle, maxCameraAngle);
        camera.transform.eulerAngles = new Vector3(this.cameraYAngle, this.cameraXAngle, 0f);

        // Camera Translation
        camera.transform.position = this.lastFramePosition;
        float speedModifier = 1f;
        if (Input.GetKey(KeyCode.H)) { // boost
            speedModifier = 7f;
        }
        if (Input.GetKey(KeyCode.I)) { // forwards
            Vector3 direction = camera.transform.forward;
            direction.Normalize();
            camera.transform.position += Time.deltaTime * speedModifier * direction;
        }
        if (Input.GetKey(KeyCode.K)) { // backwards
            Vector3 direction = camera.transform.forward;
            direction.Normalize();
            camera.transform.position -= Time.deltaTime * speedModifier * direction;
        }
        if (Input.GetKey(KeyCode.J)) { // left
            Vector3 direction = camera.transform.right;
            direction.Normalize();
            camera.transform.position -= Time.deltaTime * speedModifier * direction;
        }
        if (Input.GetKey(KeyCode.L)) { // right
            Vector3 direction = camera.transform.right;
            direction.Normalize();
            camera.transform.position += Time.deltaTime * speedModifier * direction;
        }
        if (Input.GetKey(KeyCode.U)) { // down
            Vector3 direction = camera.transform.up;
            direction.Normalize();
            camera.transform.position -= Time.deltaTime * speedModifier * direction;
        }
        if (Input.GetKey(KeyCode.O)) { // up
            Vector3 direction = camera.transform.up;
            direction.Normalize();
            camera.transform.position += Time.deltaTime * speedModifier * direction;
        }
        this.lastFramePosition = camera.transform.position;
    }

    [HideInInspector]
    public Camera m_mainCamera;

    bool cheatCameraEnabled;
    Vector3 lastFramePosition;
    float cameraXAngle;
    float cameraYAngle;
    const float minCameraAngle = -50f;
    const float maxCameraAngle = 50f;
    const float cameraSpeed = 0.5f;
}
