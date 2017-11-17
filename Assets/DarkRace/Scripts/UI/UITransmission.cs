using UnityEngine;
using UnityEngine.UI;

public class UITransmission : MonoBehaviour {
    [SerializeField]
    private Transform indicator;

    public void setValue(float value) {
        indicator.localPosition = new Vector3(value * 220.0f, 0, 0);
    }
}
