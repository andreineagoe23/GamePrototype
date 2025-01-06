using UnityEngine;

[ExecuteInEditMode]
public class CircleArenaBuilder : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject wallPrefab;        // Wall prefab
    public GameObject columnPrefab;      // Column prefab

    [Header("Arena Settings")]
    public int totalComponents = 80;     // Total number of walls + columns
    public float radius = 40f;           // Radius of the circle
    public float wallScaleX = 1.5f;       // Wall prefab scale in X
    public float wallScaleY = 1.5f;     // Wall prefab scale in Y
    public float wallScaleZ = 1.5f;       // Wall prefab scale in Z
    public float columnScaleX = 1f;     // Column prefab scale in X
    public float columnScaleY = 1.5f;   // Column prefab scale in Y
    public float columnScaleZ = 1f;     // Column prefab scale in Z

    [ContextMenu("Build Arena Components")]
    public void BuildArenaComponents()
    {
        if (!ValidatePrefabs())
        {
            Debug.LogError("Some prefabs are missing! Assign all required prefabs before building.");
            return;
        }

        ClearArena();

        // Calculate angle step between each prefab
        float angleStep = 360f / totalComponents;

        for (int i = 0; i < totalComponents; i++)
        {
            // Calculate the position of each prefab along the circle
            float angle = i * angleStep;
            Vector3 position = GetPositionOnCircle(angle, radius);

            // Determine whether to place a wall or column
            GameObject prefabToPlace = (i % 2 == 0) ? wallPrefab : columnPrefab;

            // Instantiate the prefab at the calculated position
            GameObject placedObject = Instantiate(prefabToPlace, position, Quaternion.identity, transform);

            // Scale the prefab according to the wall or column size
            if (prefabToPlace == wallPrefab)
            {
                placedObject.transform.localScale = new Vector3(wallScaleX, wallScaleY, wallScaleZ);
            }
            else if (prefabToPlace == columnPrefab)
            {
                placedObject.transform.localScale = new Vector3(columnScaleX, columnScaleY, columnScaleZ);
            }

            placedObject.name = prefabToPlace.name + "_Component_" + i;
        }

        Debug.Log($"Arena components built with {totalComponents} components in a circle.");
    }

    private Vector3 GetPositionOnCircle(float angle, float radius)
    {
        float radian = Mathf.Deg2Rad * angle;
        float x = Mathf.Cos(radian) * radius;
        float z = Mathf.Sin(radian) * radius;
        return new Vector3(x, 0, z); // Place on the ground (Y = 0)
    }

    [ContextMenu("Clear Arena")]
    public void ClearArena()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        Debug.Log("Arena cleared.");
    }

    private bool ValidatePrefabs()
    {
        return wallPrefab && columnPrefab;
    }
}
