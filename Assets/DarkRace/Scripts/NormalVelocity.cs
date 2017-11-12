using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalVelocity : MonoBehaviour {
    public static NormalVelocity instance = null;
    [SerializeField]
    private float accel = 0;
    public float maxVelocity = 0;
    [SerializeField]
    private Vehicle[] vehicles;
    [HideInInspector]
    public float velocity = 0;
    private bool moving = false;
    private float lastFloorX = 20;

    void Awake() {
        instance = this;
    }

    public void Start() {
        StartCoroutine(move());
    }

    private IEnumerator move() {
        moving = true;

        yield return new WaitForSeconds(1.0f);

        while(moving) {
            if(velocity > maxVelocity) {
                velocity = maxVelocity;
            } else {
                velocity += accel;
            }

            float newX = transform.position.x + velocity * Time.deltaTime;
            transform.position = new Vector3(newX, 0, 0);

            //create new floor
            if(lastFloorX - newX < 20) {
                lastFloorX += 20;
                GameObject floorObject = ObjectPool.instance.getObjectForType("floor");
                floorObject.transform.position = new Vector3(lastFloorX, 0, 0);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
