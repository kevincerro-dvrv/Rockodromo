using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Velocimeter : MonoBehaviour {
    public InputActionProperty handCotrollerVelocityProperty;

    public Vector3 GetVelocity() {
        return handCotrollerVelocityProperty.action.ReadValue<Vector3>();
    }
}
