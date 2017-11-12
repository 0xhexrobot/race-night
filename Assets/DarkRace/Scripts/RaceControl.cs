using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceControl : MonoBehaviour {
    [SerializeField]
    AIControl[] aiControls;

    void Start() {
        StartCoroutine(startRace());
    }

    private IEnumerator startRace() {
        yield return new WaitForSeconds(1.0f);

        foreach(AIControl aiControl in aiControls) {
            aiControl.startAccel();
        }
    }
}
