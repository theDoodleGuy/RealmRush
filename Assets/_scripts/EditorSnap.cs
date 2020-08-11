using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class EditorSnap : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(
            waypoint.GetGridPos().x * gridSize,
            0f,
            waypoint.GetGridPos().y * gridSize
            );
    }

    private void UpdateLabel()
    {
        Vector2Int snapPos = waypoint.GetGridPos();
        TextMesh textMesh = GetComponentInChildren<TextMesh>();

        textMesh.text = snapPos.x + "," + snapPos.y;

        gameObject.name = "Cube (" + textMesh.text.ToString() + ")";
    }
}
