using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using TMPro;

public class LineManager : MonoBehaviour
{
    public TextMeshPro textPrefab;
    public void Start() {
        Vibration.Init();
    }

    public void DrawLine(ARObjectPlacementEventArgs args)
    {
        Vibration.VibratePop();
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, args.placementObject.transform.position);

        if (lineRenderer.positionCount > 1) {
            Vector3 from = lineRenderer.GetPosition(lineRenderer.positionCount - 2);
            Vector3 to = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
            float distance = Vector3.Distance(from, to);
            
            // Rotate text
            Vector3 direction = to - from;
            Vector3 normal = args.placementObject.transform.up;
            Vector3 upwards = Vector3.Cross(direction, normal).normalized;
            Quaternion rotation = Quaternion.LookRotation(-normal, upwards);

            // Position text
            Vector3 position = from + direction * 0.5f + upwards * 0.01f;

            TextMeshPro inst = Instantiate(textPrefab, position, rotation);
            inst.SetText(distance.ToString("N2"));
        }
    }
}
