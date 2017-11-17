using UnityEngine;
using UnityEngine.UI;

public class UIMiniMap : MonoBehaviour {
    private const float MINIMAP_WIDTH = 300.0f;
    [SerializeField]
    private Image[] imgVehicles;
    private Vehicle[] vehicles;
    private float goalX = 0;

    public void setRaceInfo(Vehicle[] vehicles, float goalX) {
        this.vehicles = vehicles;
        this.goalX = goalX;
    }

    public void updateVehicles() {
        for(int i = 0; i < vehicles.Length; i++) {
            float miniMapPosX = -MINIMAP_WIDTH * 0.5f + vehicles[i].transform.position.x / goalX * MINIMAP_WIDTH;
            imgVehicles[i].transform.localPosition = new Vector2(miniMapPosX, imgVehicles[i].transform.localPosition.y);
        }
    }
}
