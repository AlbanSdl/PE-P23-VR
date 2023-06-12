using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardText : MonoBehaviour
{

    public string text;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshPro>().text = text;
    }

}
