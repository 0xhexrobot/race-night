using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager instance = null;
    [Header("Normal UI")]
    public RectTransform normalUi;
    public UITransmission uiTransmission;
    public UIMiniMap uiMiniMap;
    public Text txtVelocity;
    public Text txtCounter;
    public Text txtLives;
    [Header("End UI")]
    public RectTransform endUI;
    public GameObject winElements;
    public GameObject loseElements;

    void Awake() {
        instance = this;
    }
}
