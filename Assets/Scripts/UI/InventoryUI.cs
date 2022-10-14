using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Farm.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("拖拽图片")]
        public Image dragItem;
        [Header("玩家背包UI")]
        [SerializeField] private GameObject bagUI;
        private bool bagOpened;
        [SerializeField] private SlotUI[] playerSlots;
        public ItemToolTip itemToolTip;
        private void OnEnable()
        {
            EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
        }
        private void OnDisable()
        {
            EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
        }
        /// <summary>
        /// 更新指定位置的Slot事件函数
        /// </summary>
        /// <param name="location">库存位置</param>
        /// <param name="list">数据列表</param>
        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch(location)
            {
                case InventoryLocation.Player:
                    for(int i = 0;i<playerSlots.Length;i++)
                    {
                        if(list[i].itemAmount>0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            playerSlots[i].UpdateSlot(item,list[i].itemAmount);
                        }
                        else
                        {
                            playerSlots[i].UpdateEmptySlot();
                        }
                    }
                    break;
            }
        }
        private void Start()
        {
            //给每一个格子序号
            for(int i= 0; i < playerSlots.Length;i++)
            {
                playerSlots[i].slotIndex = i;
            }
            bagOpened = bagUI.activeInHierarchy;
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.B))
            {
                OpenBagUI();
            }
        }
        public void OpenBagUI()
        {
            bagOpened = !bagOpened;
            bagUI.SetActive(bagOpened);
        }
        /// <summary>
        /// 更新Slot高亮显示 
        /// </summary>
        /// <param name="index"></param>
        public void UpdateSlotHighlight(int index)
        {
            foreach(var slot in playerSlots)
            {
                if(slot.isSelected && slot.slotIndex == index)
                {
                    slot.slotHighlight.gameObject.SetActive(true);
                }
                else
                {
                    slot.isSelected = false;
                    slot.slotHighlight.gameObject.SetActive(false);
                }
            }
        }
    }
}

