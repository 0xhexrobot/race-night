using System.Collections;
using UnityEngine;

public class AIControl : VehicleControl {
    [SerializeField]
    private float interval;
    [SerializeField]
    private float minAccelPercent;
    [SerializeField]
    private float maxAccelPercent;
    [SerializeField]
    [Range(0, 1.0f)]
    private float delayPercentToChangeSpeed;
    private float currentTime = 0;
    private float lastInterval = 0;
    private float accelPercent = 0;
    private float startingAccelPercent = 0;
    private float endingAccelPercent = 0;
    private Transmission transmission;
    private Vehicle vehicle;

    void Awake() {
        transmission = GetComponent<Transmission>();
        vehicle = GetComponent<Vehicle>();
    }

    public override IEnumerator accelerate() {
        accelerating = true;

        startingAccelPercent = Random.Range(minAccelPercent, maxAccelPercent);
        endingAccelPercent = Random.Range(minAccelPercent, maxAccelPercent);

        yield return new WaitForSeconds(1.0f);

        while(accelerating) {
            currentTime += Time.deltaTime;

            // try to change speed
            if(transmission.hasNextPhase()) {
                if(vehicle.velocity >= transmission.getVelocityToNextPhase()) {
                    float phaseRange = transmission.getMaxSpeed() - transmission.getVelocityToNextPhase();
                    float percentToMaxVelocity = (vehicle.velocity - transmission.getVelocityToNextPhase()) / phaseRange;

                    if(percentToMaxVelocity >= delayPercentToChangeSpeed) {
                        vehicle.tryToChangeTransmissionPhase();
                    }
                }
            }

            // accel
            if(currentTime > lastInterval + interval) {
                lastInterval += interval;
                startingAccelPercent = endingAccelPercent;
                endingAccelPercent = Random.Range(minAccelPercent, maxAccelPercent);
            }

            float interpolation = (currentTime - lastInterval) / interval;

            accelPercent = Mathf.Lerp(startingAccelPercent, endingAccelPercent, interpolation);
            vehicle.updateAccelFactor(accelPercent);

            yield return new WaitForEndOfFrame();
        }
    }
}
