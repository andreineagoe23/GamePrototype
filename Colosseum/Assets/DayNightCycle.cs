using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Sun Settings")]
    public Light directionalLight; // Reference to the directional light (sun)
    public float daySpeed = 1f; // Speed of the day-night cycle (rotation speed)

    [Header("Lighting Settings")]
    public Color dayColor = new Color(1f, 1f, 1f, 1f); // Daytime color
    public Color nightColor = new Color(0.2f, 0.2f, 0.5f, 1f); // Nighttime color
    public float dayAmbientIntensity = 1f; // Ambient intensity during the day
    public float nightAmbientIntensity = 0.2f; // Ambient intensity during the night

    [Header("Skybox Settings")]
    public Material daySkybox; // Day skybox
    public Material nightSkybox; // Night skybox

    private void Start()
    {
        if (directionalLight == null)
        {
            directionalLight = GetComponent<Light>(); // Get the directional light if not assigned
        }
    }

    private void Update()
    {
        // Rotate the sun over time
        RotateSun();

        // Smooth transition of light intensity and color
        TransitionLighting();
    }

    void RotateSun()
    {
        // Rotate the sun (directional light) around the X-axis to simulate the passage of time
        directionalLight.transform.Rotate(Vector3.right, daySpeed * Time.deltaTime);
    }

    void TransitionLighting()
    {
        // Adjust the ambient lighting and skybox based on the sun's position
        float sunAngle = directionalLight.transform.rotation.eulerAngles.x;

        // Check if the sun is below the horizon (nighttime)
        if (sunAngle > 180f && sunAngle < 360f)
        {
            // Nighttime
            RenderSettings.ambientLight = nightColor;
            RenderSettings.skybox = nightSkybox;
            RenderSettings.ambientIntensity = nightAmbientIntensity;
        }
        else
        {
            // Daytime
            RenderSettings.ambientLight = dayColor;
            RenderSettings.skybox = daySkybox;
            RenderSettings.ambientIntensity = dayAmbientIntensity;
        }
    }
}
