using UnityEngine;

[CreateAssetMenu(menuName = "Cooking/Recipe Data")]
public class RecipeData : ScriptableObject
{
    public string recipeName;        // Tên món ăn (Burger, Pizza)
    public Sprite dishIcon;          // Ảnh món ăn
    public string[] requiredIds;     // Các id nguyên liệu cần
}
