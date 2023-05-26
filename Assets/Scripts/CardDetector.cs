using UnityEngine;

public class CardDetector : MonoBehaviour
{

    public int CardNumber;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == $"card{CardNumber}") {
            // User used the correct card
        }
    }
}
