using UnityEngine;
using System.Collections.Generic;

public class ControlZ : MonoBehaviour
{
    // Store just the positions, not transforms
    private List<List<Vector3>> positionHistory = new List<List<Vector3>>();

    // Optional: Reference to all tracked objects
    private List<Transform> trackedObjects = new List<Transform>();

    private void Start()
    {
        // Cache all children once at start (assuming scene hierarchy doesn't change)
        trackedObjects = new List<Transform>(GetComponentsInChildren<Transform>());

        // Optional: remove self from list if needed
        trackedObjects.Remove(transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // Save on S
        {
            SaveScene();
        }

        if (Input.GetKeyDown(KeyCode.Z)) // Undo on Z
        {
            LoadScene();
        }
    }

    public void SaveScene()
    {
        List<Vector3> snapshot = new List<Vector3>();

        foreach (Transform obj in trackedObjects)
        {
            snapshot.Add(obj.position);
        }

        if (positionHistory.Count > 100) { positionHistory.RemoveAt(0); }
        positionHistory.Add(snapshot);
        Debug.Log($"Saved: {snapshot.Count} positions. Total saves: {positionHistory.Count}");
    }

    private void LoadScene()
    {
        if (positionHistory.Count == 0)
        {
            Debug.Log("Nothing to undo.");
            return;
        }

        List<Vector3> lastSnapshot = positionHistory[positionHistory.Count - 1];

        for (int i = 0; i < trackedObjects.Count && i < lastSnapshot.Count; i++)
        {
            trackedObjects[i].position = lastSnapshot[i];
        }

        positionHistory.RemoveAt(positionHistory.Count - 1);
        Debug.Log("Undo performed.");
    }
}
