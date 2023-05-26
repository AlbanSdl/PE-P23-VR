using UnityEngine;
using System.Collections.Generic;


public class CardDetector : MonoBehaviour
{

    public int CardNumber;
    [SerializeField] WinCondition winCondition;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == $"card{CardNumber}") {
            // User used the correct card
            winCondition.RightCount++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == $"card{CardNumber}") {
            // User used the correct card
            winCondition.RightCount--;
        }
    }
}
