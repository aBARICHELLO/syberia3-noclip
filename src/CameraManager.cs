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
        this.m_mainCamera.transform.position = this.lastFramePosition;
        if (Input.GetKeyDown(KeyCode.PageUp)) {
            this.m_mainCamera.transform.position += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.PageDown)) {
            this.m_mainCamera.transform.position += Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.Home)) {
            this.m_mainCamera.transform.position += Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.End)) {
            this.m_mainCamera.transform.position += Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.Insert)) {
            this.m_mainCamera.transform.position += Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.Delete)) {
            this.m_mainCamera.transform.position += Vector3.back;
        }
        this.lastFramePosition = this.m_mainCamera.transform.position;
    }

    [HideInInspector]
    public Camera m_mainCamera;

    bool cheatCameraEnabled;

    Vector3 lastFramePosition;
}
