using UnityEngine;

public class DropSlot : MonoBehaviour
{
    public Transform snapPoint;   // điểm chốt (đặt làm child ở đúng mặt bàn)
    public int orderIndex = -1;   // -1 = không yêu cầu thứ tự; >=0 = vị trí cần đặt theo thứ tự
    public bool IsOccupied { get; private set; }

    public void Occupy() { IsOccupied = true; }
    public void Vacate() { IsOccupied = false; }

    void Reset()
    { // auto tạo snapPoint khi thêm script
        if (snapPoint == null)
        {
            var t = new GameObject("SnapPoint").transform;
            t.SetParent(transform); t.localPosition = Vector3.zero; t.localRotation = Quaternion.identity;
            snapPoint = t;
        }
    }
}
