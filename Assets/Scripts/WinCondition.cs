using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{

    public int RightCount;
    [SerializeField] ParticleSystem winParticles;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void WinScreen() {
        if (RightCount == 4) {
            // Win
            winParticles.Play();
        }
    }

}
