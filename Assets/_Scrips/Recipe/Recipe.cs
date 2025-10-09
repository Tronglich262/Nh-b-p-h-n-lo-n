using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string recipeName;
    public string[] requiredIds; // list id nguyên liệu (vd: {"1", "3", "5", "7"})
}
