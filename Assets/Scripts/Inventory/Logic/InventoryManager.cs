using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.Inventory//使用命名空间，避免代码耦合
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        public ItemToolTip itemToolTip;
        [Header("物品数据")]
        public ItemDataList_SO itemDataList_SO;
        [Header("背包数据")]
        public InventoryBag_SO playerBag_SO;
        private void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,playerBag_SO.itemList);
        }
        /// <summary>
        /// 通过ID返回物品信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataList_SO.itemDetailsList.Find(i => i.itemID == ID);
        }
        /// <summary>
        /// 添加物品到Player背包
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toDestroy">是否销毁物品</param>
        public void AddItem(Item item, bool toDestroy)
        {
            var index = GetItemIndexInBag(item.itemID);
            AddItemAtIndex(item.itemID,index,1);
            if (toDestroy)
            {
                Destroy(item.gameObject);
            }
            //更新UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag_SO.itemList);
        }
        /// <summary>
        /// 检查背包是否有空位
        /// </summary>
        /// <returns></returns>
        private bool CheckBagCapacity()
        {
            for (int i = 0; i < playerBag_SO.itemList.Count; i++)
            {
                if (playerBag_SO.itemList[i].itemID == 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 通过ID获得背包中已有物品位置
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <returns>-1则没有该物品</returns>
        private int GetItemIndexInBag(int ID)
        {
            for (int i = 0; i < playerBag_SO.itemList.Count; i++)
            {
                if (playerBag_SO.itemList[i].itemID == ID)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 在背包指定位置添加物品
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="index"></param>
        /// <param name="amount"></param>
        private void AddItemAtIndex(int ID, int index, int amount)
        {
            if (index == -1 && CheckBagCapacity())
            {
                var item = new InventoryItem { itemID = ID, itemAmount = amount };
                for (int i = 0; i < playerBag_SO.itemList.Count; i++)
                {
                    if (playerBag_SO.itemList[i].itemID == 0)
                    {
                        playerBag_SO.itemList[i] = item;
                        break;
                    }
                }
            }
            else
            {
                int currentAmount = playerBag_SO.itemList[index].itemAmount + amount;
                var item = new InventoryItem{itemID = ID,itemAmount = currentAmount};
                playerBag_SO.itemList[index] = item;
            }
        }
        /// <summary>
        /// Player背包范围内交换物品
        /// </summary>
        /// <param name="fromIndex"></param>
        /// <param name="targetIndex"></param>
        public void SwapItem(int fromIndex,int targetIndex)
        {
            InventoryItem currentItem = playerBag_SO.itemList[fromIndex];
            InventoryItem targetItem = playerBag_SO.itemList[targetIndex];
            if(targetItem.itemID!=0)
            {
                playerBag_SO.itemList[fromIndex] = targetItem;
                playerBag_SO.itemList[targetIndex] = currentItem;
            }
            else
            {
                playerBag_SO.itemList[targetIndex] = currentItem;
                playerBag_SO.itemList[fromIndex] = new InventoryItem();
            }
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag_SO.itemList);
        }
    }
}

