using UnityEngine;

public class IngredientFloat : MonoBehaviour
{
    public float floatAmplitude = 0.25f;
    public float floatFrequency = 1.5f;
    public float rotationSpeed = 25f;

    private Vector3 startPos;
    private float randomOffset;

    void Start()
    {
        startPos = transform.position;
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency + randomOffset) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * (rotationSpeed * 0.2f) * Time.deltaTime, Space.Self);
    }

}
