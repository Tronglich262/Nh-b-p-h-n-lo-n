using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RecipeTooltipUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Panel hiển thị nguyên liệu")]
    public GameObject detailPanel;              // Panel hiển thị
    public Image[] ingredientIcons;             // Các ô icon nguyên liệu
    public Text[] ingredientTexts;              // Các ô text (cùng index với icon)

    private RecipeData linkedRecipe;

    public void Setup(RecipeData recipe)
    {
        linkedRecipe = recipe;
        if (linkedRecipe != null)
            Debug.Log($"[Tooltip] Setup với {linkedRecipe.recipeName}");
        else
            Debug.Log("[Tooltip] Setup(null)");
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(" OnPointerDown " + (linkedRecipe != null ? linkedRecipe.recipeName : "null"));
        if (linkedRecipe != null)
            ShowRecipeDetail(linkedRecipe);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(" OnPointerUp");
        Hide();
    }


    private void ShowRecipeDetail(RecipeData recipe)
    {
        if (detailPanel == null) return;

        detailPanel.SetActive(true);

        // reset tất cả ô
        for (int i = 0; i < ingredientIcons.Length; i++)
        {
            ingredientIcons[i].sprite = null;
            ingredientIcons[i].enabled = false;

            if (i < ingredientTexts.Length)
                ingredientTexts[i].text = "";
        }

        // hiển thị nguyên liệu
        for (int i = 0; i < recipe.ingredients.Length && i < ingredientIcons.Length; i++)
        {
            var entry = recipe.ingredients[i];

            // gán icon
            if (entry.icon != null)
            {
                ingredientIcons[i].sprite = entry.icon;
                ingredientIcons[i].enabled = true;
            }

            // gán text theo name
            if (i < ingredientTexts.Length)
                ingredientTexts[i].text = entry.name;   //  dùng name thay vì id
        }
    }



    private void Hide()
    {
        if (detailPanel != null)
            detailPanel.SetActive(false);
    }
}
