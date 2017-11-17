using UnityEngine;

public class Transmission {
    public TransmissionPhase[] transmissionPhases;
    public float transmissionValue = 0;
    private float phaseChange = 0.04f;

    public void update() {
        phaseChange += 0.05f;
        transmissionValue = Mathf.Sin(phaseChange);
    }

    public void applyAxis(float change) {
        transmissionValue += change;
        transmissionValue = Mathf.Clamp(transmissionValue, -1.0f, 1.0f);
    }
}
