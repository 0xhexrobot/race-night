using System.Collections;
using UnityEngine;

public abstract class VehicleControl : MonoBehaviour {
    public bool accelerating = false;

    public void startAccel() {
        StartCoroutine(accelerate());
    }

    public abstract IEnumerator accelerate();

    public void stopAcceleration() {
        accelerating = false;
    }
}
