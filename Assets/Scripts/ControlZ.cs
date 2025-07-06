using UnityEngine;
using System.Collections.Generic;

public class ControlZ : MonoBehaviour
{
    private List<List<Vector3>> positionHistory = new List<List<Vector3>>();
    private List<List<Quaternion>> rotationHistory = new List<List<Quaternion>>(); // ← NUEVO

    private List<Transform> trackedObjects = new List<Transform>();
    private float timer = 0f, cooldown = 0.1f;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
    }

    private void Start()
    {
        // Cache all children once at start
        trackedObjects = new List<Transform>(GetComponentsInChildren<Transform>());
        trackedObjects.Remove(transform); // Optional: remove self
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10000) timer = cooldown;

        if (Input.GetKey(KeyCode.Z) && timer >= cooldown)
        {
            LoadScene();
            
            audioManager.PlaySFX(audioManager.Z);
            timer = 0f;
        }
    }

    public void SaveScene()
    {
        List<Vector3> snapshotPositions = new List<Vector3>();
        List<Quaternion> snapshotRotations = new List<Quaternion>();

        foreach (Transform obj in trackedObjects)
        {
            snapshotPositions.Add(obj.position);
            snapshotRotations.Add(obj.rotation); // ← Guardar rotación
        }

        // Limitar tamaño del historial
        if (positionHistory.Count > 100)
        {
            positionHistory.RemoveAt(0);
            rotationHistory.RemoveAt(0);
        }

        positionHistory.Add(snapshotPositions);
        rotationHistory.Add(snapshotRotations);

        Debug.Log($"Saved {snapshotPositions.Count} positions and rotations. Total saves: {positionHistory.Count}");
    }

    private void LoadScene()
    {
        if (positionHistory.Count < 1 || rotationHistory.Count < 2)
        {
            Debug.Log("Nothing to undo or rotation history too short.");
            return;
        }

        // Restaurar la última posición guardada
        List<Vector3> lastPositions = positionHistory[positionHistory.Count - 1];

        // Pero usamos la rotación del frame anterior al último (más "actual")
        List<Quaternion> matchingRotations = rotationHistory[rotationHistory.Count - 2];

        for (int i = 0; i < trackedObjects.Count && i < lastPositions.Count && i < matchingRotations.Count; i++)
        {
            trackedObjects[i].position = lastPositions[i];
            trackedObjects[i].rotation = matchingRotations[i]; // ← Coincide con la pose en esa posición
        }

        // Eliminar el último frame de posición y el anterior de rotación
        positionHistory.RemoveAt(positionHistory.Count - 1);
        rotationHistory.RemoveAt(rotationHistory.Count - 2); // ← Importante: borrar la que usamos
        Debug.Log("Undo performed: positions and synced rotations restored.");
    }

}

