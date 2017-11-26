using System.Collections;
using UnityEngine;

public class AIControl : VehicleControl {
    public override IEnumerator accelerate() {
        accelerating = true;

        yield return new WaitForSeconds(1.0f);

        while(accelerating) {
            GetComponent<Vehicle>().updateAccelFactor(1.0f);

            yield return new WaitForEndOfFrame();
        }
    }
}
