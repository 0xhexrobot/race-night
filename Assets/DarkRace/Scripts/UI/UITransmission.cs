using UnityEngine;
using UnityEngine.UI;

public class UITransmission : MonoBehaviour {
    [SerializeField]
    private Transform indicator;
    [SerializeField]
    private RectTransform tolerance;

    public void setValue(float value) {
        indicator.localPosition = new Vector3(value * 220.0f, 0, 0);
    }

    public void setTolerance(float tolerance) {
        this.tolerance.sizeDelta = new Vector2(440.0f * tolerance, this.tolerance.sizeDelta.y);
    }
}
