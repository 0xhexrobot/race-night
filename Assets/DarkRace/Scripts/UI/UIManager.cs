using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager instance = null;
    public UITransmission uiTransmission;
    public UIMiniMap uiMiniMap;
    public Text txtVelocity;

    void Awake() {
        instance = this;
    }
}
