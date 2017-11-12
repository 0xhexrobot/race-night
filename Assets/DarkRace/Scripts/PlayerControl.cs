using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private float lastFloorX = 20;
    private float transmissionValue = 0;
    private float phaseChange = 0.05f;

    void Update() {
        //create new floor
        if(lastFloorX - transform.position.x < 20) {
            lastFloorX += 20;
            GameObject floorObject = ObjectPool.instance.getObjectForType("floor");
            floorObject.transform.position = new Vector3(lastFloorX, 0, 0);
        }

        phaseChange += 0.05f;
        transmissionValue = Mathf.Sin(phaseChange);

        UITransmission.instance.setValue(transmissionValue);
    }
}
