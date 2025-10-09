using UnityEngine;

[System.Serializable]
public class IngredientData
{
    public string id;       // id trùng với Ingredient.id
    public Sprite icon;     // sprite tương ứng
}

[CreateAssetMenu(fileName = "IngredientDatabase", menuName = "Cooking/IngredientDatabase")]
public class IngredientDatabase : ScriptableObject
{
    public IngredientData[] ingredients;

    public Sprite GetSpriteById(string id)
    {
        foreach (var ing in ingredients)
        {
            if (ing.id == id) return ing.icon;
        }
        return null;
    }
}
