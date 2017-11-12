using System.Collections;
using UnityEngine;

public class AIControl : MonoBehaviour {
    public float acceleration = 0.1f;
    public bool accelerating = false;
    public float velocity = 0;
    public float maxVelocity = 0;

    public void startAccel() {
        StartCoroutine(accelerate());
    }

    public IEnumerator accelerate() {
        accelerating = true;

        yield return new WaitForSeconds(1.0f);

        while(accelerating) {
            if(velocity > maxVelocity) {
                velocity = maxVelocity;
            } else {
                velocity += acceleration;
            }

            float newX = transform.position.x + velocity * Time.deltaTime;
            transform.position = new Vector3(newX, 0, transform.position.z);

            yield return new WaitForEndOfFrame();
        }
    }
}
