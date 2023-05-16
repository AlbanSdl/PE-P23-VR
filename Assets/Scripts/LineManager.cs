using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class LineManager : MonoBehaviour
{
    public void Start() {
        Vibration.Init();
    }

    public void DrawLine(ARObjectPlacementEventArgs args)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, args.placementObject.transform.position);
        Vibration.VibratePop();
    }
}
