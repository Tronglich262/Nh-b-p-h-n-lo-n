using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    [Header("UI hiển thị hàng đợi món ăn")]
    public Image currentRecipeImage;       // icon món hiện tại (to)
    public Image[] recipeQueueImages;      // icon các món tiếp theo (nhỏ bên dưới)

    [Header("Slot nguyên liệu người chơi kéo vào")]
    public IngredientSlot[] slots;

    [Header("Danh sách món ăn")]
    public List<RecipeData> allRecipes;
    public int queueSize = 5;

    [Header("Database nguyên liệu")]
    public IngredientDatabase database;   


    private Queue<RecipeData> recipeQueue = new Queue<RecipeData>();
    private RecipeData currentRecipe;
    public static RecipeManager Instance { get; private set; }
    private void Start()
    {
        if (slots == null || slots.Length == 0)
            slots = FindObjectsOfType<IngredientSlot>();

        // nạp queue
        for (int i = 0; i < queueSize && i < allRecipes.Count; i++)
        {
            recipeQueue.Enqueue(allRecipes[i]); // lấy đúng thứ tự list
        }


        NextRecipe(); // chọn món đầu tiên
    }

    public void ApplyRecipe()
    {
        if (currentRecipe == null)
        {
            Debug.Log(" Không có recipe để check.");
            return;
        }

        var need = new HashSet<string>(currentRecipe.ingredients.Select(e => e.id));
        var have = new HashSet<string>(
            slots.Select(s => s.GetCurrentIngredientId())
                 .Where(id => !string.IsNullOrEmpty(id))
        );

        Debug.Log($"Need: [{string.Join(", ", need)}] | Have: [{string.Join(", ", have)}]");

        if (have.Count == need.Count && have.SetEquals(need))
        {
            Debug.Log($" Đúng món {currentRecipe.recipeName}");
            NextRecipe();
        }
        else
        {
            Debug.Log(" Sai công thức!");
        }
    }

    private void NextRecipe()
    {
        // clear slot nguyên liệu
        foreach (var slot in slots)
            slot.ClearSlot();

        if (recipeQueue.Count > 0)
        {
            // lấy món tiếp theo
            currentRecipe = recipeQueue.Dequeue();

            // update UI món hiện tại
            if (currentRecipeImage != null)
            {
                currentRecipeImage.sprite = currentRecipe.dishIcon;

                // 🔥 gán RecipeData cho tooltip
                var tooltip = currentRecipeImage.GetComponent<RecipeTooltipUI>();
                if (tooltip != null)
                    tooltip.Setup(currentRecipe);
            }

            // update UI hàng chờ
            RefreshQueueUI();

            Debug.Log($"➡ Món mới: {currentRecipe.recipeName}");
        }
        else
        {
            currentRecipe = null;
            currentRecipeImage.sprite = null;

            var tooltip = currentRecipeImage.GetComponent<RecipeTooltipUI>();
            if (tooltip != null)
                tooltip.Setup(null); // clear tooltip

            RefreshQueueUI();
            Debug.Log("🏆 Chiến thắng! Hoàn thành tất cả món.");
        }
    }


    private void RefreshQueueUI()
    {
        var recipes = recipeQueue.ToList();

        for (int i = 0; i < recipeQueueImages.Length; i++)
        {
            var tooltip = recipeQueueImages[i].GetComponent<RecipeTooltipUI>();

            if (i < recipes.Count)
            {
                recipeQueueImages[i].sprite = recipes[i].dishIcon;
                recipeQueueImages[i].enabled = true;

                if (tooltip != null)
                    tooltip.Setup(recipes[i]); //  Gán recipe cho tooltip
            }
            else
            {
                recipeQueueImages[i].sprite = null;
                recipeQueueImages[i].enabled = false;

                if (tooltip != null)
                    tooltip.Setup(null); // clear nếu trống
            }
        }
    }



}
