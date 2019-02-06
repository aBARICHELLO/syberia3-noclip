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
        float speedModifier = 1f;
        if (Input.GetKey(KeyCode.LeftShift)) {
            speedModifier = 7f;
        }

        // Camera Translation
        this.m_mainCamera.transform.position = this.lastFramePosition;
        if (Input.GetKey(KeyCode.PageUp)) {
            this.m_mainCamera.transform.position += Time.deltaTime * speedModifier * Vector3.right;
        }
        if (Input.GetKey(KeyCode.PageDown)) {
            this.m_mainCamera.transform.position += Time.deltaTime * speedModifier * Vector3.left;
        }
        if (Input.GetKey(KeyCode.Home)) {
            this.m_mainCamera.transform.position += Time.deltaTime * speedModifier * Vector3.up;
        }
        if (Input.GetKey(KeyCode.End)) {
            this.m_mainCamera.transform.position += Time.deltaTime * speedModifier * Vector3.down;
        }
        if (Input.GetKey(KeyCode.Insert)) {
            this.m_mainCamera.transform.position += Time.deltaTime * speedModifier * Vector3.forward;
        }
        if (Input.GetKey(KeyCode.Delete)) {
            this.m_mainCamera.transform.position += Time.deltaTime * speedModifier * Vector3.back;
        }
        this.lastFramePosition = this.m_mainCamera.transform.position;

        // Camera Rotation
        this.cameraXAngle += cameraSpeed * Input.GetAxis("Mouse X");
        this.cameraYAngle -= cameraSpeed * Input.GetAxis("Mouse Y");
        Mathf.Clamp(this.cameraYAngle, minCameraAngle, maxCameraAngle);
        this.m_mainCamera.transform.eulerAngles = new Vector3(this.cameraYAngle, this.cameraXAngle, 0f);
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
