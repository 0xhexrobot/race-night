using UnityEngine;

public class Trophy : MonoBehaviour {
    [SerializeField]
    private float change = 1.0f;

    void Update() {
        transform.Rotate(0, change * Time.deltaTime, 0);
    }
}
