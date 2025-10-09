using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject[] ingredientPrefabs;

    [Header("Spawn Area")]
    public Transform leftAreaCenter;
    public Transform rightAreaCenter;
    public int rows = 3;              // số hàng (tầng cao thấp)
    public float verticalSpacing = 2; // khoảng cách theo trục Y
    public float horizontalSpacing = 2.5f; // khoảng cách theo trục X
    public float randomOffset = 0.5f; // độ lệch nhỏ để nhìn tự nhiên

    void Start()
    {
        SpawnIngredients();
    }

    void SpawnIngredients()
    {
        int half = ingredientPrefabs.Length / 2;
        int leftCount = Mathf.Min(half, ingredientPrefabs.Length);
        int rightCount = ingredientPrefabs.Length - leftCount;

        // spawn bên trái
        SpawnSide(leftAreaCenter.position, ingredientPrefabs, 0, leftCount);

        // spawn bên phải
        SpawnSide(rightAreaCenter.position, ingredientPrefabs, leftCount, ingredientPrefabs.Length);
    }

    void SpawnSide(Vector3 center, GameObject[] prefabs, int start, int end)
    {
        int count = end - start;
        int perRow = Mathf.CeilToInt((float)count / rows);

        for (int i = 0; i < count; i++)
        {
            int row = i / perRow;
            int col = i % perRow;

            Vector3 basePos = center + new Vector3(
                col * horizontalSpacing,
                -row * verticalSpacing,
                0
            );

            // thêm random offset để không bị thẳng tắp
            Vector3 pos = basePos + new Vector3(
                Random.Range(-randomOffset, randomOffset),
                Random.Range(-randomOffset, randomOffset),
                0
            );

            GameObject ing = Instantiate(prefabs[start + i], pos, Random.rotation);
            ing.AddComponent<IngredientFloat>();
        }
    }
}
