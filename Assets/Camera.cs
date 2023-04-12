using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject target;

    void Update() {
        this.transform.position = new Vector3(
            target.transform.position.x,
            this.transform.position.y,
            this.transform.position.z
        );
    }
}
