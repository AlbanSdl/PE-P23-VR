using UnityEngine;

public class Terrain : MonoBehaviour {
    public TerrainType type = TerrainType.STRAIGHT;

    public void OnReached() {
        // display proper colors
        SpriteRenderer topColorEffect = transform.GetChild(0)!.GetComponent<SpriteRenderer>();
        SpriteRenderer bottomColorEffect = transform.GetChild(1)!.GetComponent<SpriteRenderer>();
        switch(type) {
            case TerrainType.STRAIGHT:
                topColorEffect.color = bottomColorEffect.color = new Color(255, 156, 0);
                break;
            case TerrainType.PARABOLIC:
                topColorEffect.color = bottomColorEffect.color = new Color(0, 208, 255);
                break;
        }
    }
}
