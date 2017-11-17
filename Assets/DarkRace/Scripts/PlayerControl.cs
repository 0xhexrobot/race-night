using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : VehicleControl {
    private float lastFloorX = 20;
    private float accumAxis = 0;
    private float accumAxisChange = 0.05f;
    private Transmission transmission;

    void Start() {
        transmission = new Transmission();
    }

    public override IEnumerator accelerate() {
        accelerating = true;

        yield return new WaitForSeconds(1.0f);

        while(accelerating) {
            float distance = Mathf.Abs(transmission.transmissionValue);
            float accelPercent = 1.0f - Mathf.Sqrt(1 - (1 - distance * distance));

            Debug.Log("Accel percent: " + accelPercent);

            GetComponent<Vehicle>().accelerate(accelPercent);

            yield return new WaitForEndOfFrame();
        }
    }

    void Update() {
        //create new floor
        if(lastFloorX - transform.position.x < 20) {
            lastFloorX += 20;
            GameObject floorObject = ObjectPool.instance.getObjectForType("floor");
            floorObject.transform.position = new Vector3(lastFloorX, 0, 0);
        }

        transmission.update();

        float axis = Input.GetAxis("Horizontal");

        accumAxis += accumAxisChange * axis;

        Debug.LogFormat("Axis: {0}, accumAxis: {1}", axis, accumAxis);

        transmission.applyAxis(accumAxis);

        accumAxis *= 0.98f;

        UIManager.instance.uiTransmission.setValue(transmission.transmissionValue);
        //km per h
        int realVelocity = Mathf.RoundToInt(GetComponent<Vehicle>().velocity * 3.6f);
        UIManager.instance.txtVelocity.text = realVelocity.ToString();
    }
}
