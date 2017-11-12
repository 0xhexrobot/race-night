using UnityEngine;
using UnityEngine.UI;

public class UITransmission : MonoBehaviour {
    public static UITransmission instance = null;
    [SerializeField]
    private Transform indicator;

    void Awake() {
        instance = this;
    }

    public void setValue(float value) {
        indicator.localPosition = new Vector3(value * 220.0f, 0, 0);
    }
}
