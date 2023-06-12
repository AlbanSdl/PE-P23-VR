using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class CardDetector : MonoBehaviour
{

    public int CardNumber;
    public int year;
    [SerializeField] WinCondition winCondition;

    private void Start() {
        transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{year}";
    }

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
