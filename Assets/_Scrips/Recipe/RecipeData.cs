using UnityEngine;

[System.Serializable]
public class IngredientEntry
{
    public string id;
    public string name;
    public Sprite icon;
}

[CreateAssetMenu(menuName = "Cooking/Recipe Data")]
public class RecipeData : ScriptableObject
{
    public string recipeName;
    public Sprite dishIcon;
    public IngredientEntry[] ingredients;    // thay cho requiredIds
}
