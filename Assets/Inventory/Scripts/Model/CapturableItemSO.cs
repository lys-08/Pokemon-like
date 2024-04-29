using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "CaptureItem", menuName = "Inventory/CaptureItem")]
    public class CaptureItemSO : ItemSO, IDestroyableItem
    {
        [field: SerializeField] public Type type;
        [field: SerializeField] public float value = 0f;
    }
}