using UnityEngine;
using UnityEngine.UI;

public class UITransmission : MonoBehaviour {
    private const float UI_TRANSMISSION_WIDTH = 440.0f;
    [SerializeField]
    private Transform indicator;
    [SerializeField]
    private RectTransform tolerance;
    [SerializeField]
    private GameObject lblReady;
    [SerializeField]
    private Text txtTransmissionNumber;

    public void setValue(float value) {
        indicator.localPosition = new Vector3(value * UI_TRANSMISSION_WIDTH * 0.5f, 0, 0);
    }

    public void setTolerance(float tolerance) {
        this.tolerance.sizeDelta = new Vector2(UI_TRANSMISSION_WIDTH * tolerance, this.tolerance.sizeDelta.y);
    }

    public void setPhaseNumber(int phaseNumber) {
        txtTransmissionNumber.text = phaseNumber.ToString();
    }

    public void showLblReady(bool show) {
        lblReady.SetActive(show);
    }

    public void setLblReadyPhase(int phaseReady) {
        string strPhase = "?";

        switch(phaseReady) {
        case 2:
            strPhase = "2nd";
            break;
        case 3:
            strPhase = "3rd";
            break;
        case 4:
            strPhase = "4th";
            break;
        }

        lblReady.GetComponent<Text>().text = string.Format("{0} ready!", strPhase);
    }
}
