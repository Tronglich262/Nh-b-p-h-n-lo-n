using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string id;
    public bool isUsed = false;

    private Camera cam;
    private bool dragging = false;
    private IngredientFloat floatScript;

    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        cam = Camera.main;
        floatScript = GetComponent<IngredientFloat>();

        startPos = transform.position;
        startRot = transform.rotation;
    }

    void OnMouseDown()
    {
        if (isUsed) return;
        dragging = true;

        if (floatScript != null)
            floatScript.enabled = false;
    }

    void OnMouseDrag()
    {
        if (!dragging || isUsed) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5f;
        transform.position = cam.ScreenToWorldPoint(mousePos);
    }

    void OnMouseUp()
    {
        if (!dragging) return;
        dragging = false;

        // check xem ingredient đang overlap với slot nào
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f);
        bool snapped = false;

        foreach (var hit in hits)
        {
            IngredientSlot slot = hit.GetComponent<IngredientSlot>();
            if (slot != null && !slot.HasIngredient)
            {
                slot.AcceptIngredient(this);
                snapped = true;
                break;
            }
        }

        // nếu không có slot hợp lệ -> reset lại
        if (!snapped)
        {
            ResetToOrigin();
        }
    }

    public void UseIngredient(Transform slot)
    {
        isUsed = true;
        gameObject.SetActive(false);
    }

    public void ResetToOrigin()
    {
        isUsed = false;
        transform.position = startPos;
        transform.rotation = startRot;
        gameObject.SetActive(true);

        if (floatScript != null)
            floatScript.enabled = true;
    }
}
