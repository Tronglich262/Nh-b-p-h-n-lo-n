using UnityEngine;
using UnityEngine.UI;

public class IngredientSlot : MonoBehaviour
{
    [Header("UI hiển thị")]
    public Image slotImage;
    public IngredientDatabase database;

    private Ingredient currentIngredient;

    //  RecipeManager sẽ đọc biến này
    public string currentIngredientId { get; private set; } = null;

    public bool HasIngredient => currentIngredient != null;

    public void AcceptIngredient(Ingredient ing)
    {
        if (currentIngredient != null || ing.isUsed) return;

        currentIngredient = ing;

        //  Gán ID cho slot để RecipeManager đọc được
        currentIngredientId = ing.id;

        ing.UseIngredient(transform);

        if (slotImage != null && database != null)
        {
            Sprite icon = database.GetSpriteById(ing.id);
            if (icon != null) slotImage.sprite = icon;
        }

        Debug.Log($" [{name}] nhận ingredient: {currentIngredientId}");
    }

    public void ClearSlot()
    {
        if (currentIngredient != null)
        {
            currentIngredient.ResetToOrigin();
            currentIngredient = null;

            //  Reset ID
            currentIngredientId = null;

            if (slotImage != null)
                slotImage.sprite = null;

            Debug.Log($" [{name}] đã trả nguyên liệu");
        }
    }

    public void OnSlotButtonClick() => ClearSlot();

    public string GetCurrentIngredientId() => currentIngredientId;
}
