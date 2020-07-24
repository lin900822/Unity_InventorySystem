using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver  //建立物品檔案資料庫
{
    /*
    功能：
    -物品資料庫，存取遊戲中所有的物品
     */
    public ItemObject[] ItemObjects;  //物品陣列
    //public Dictionary<int , ItemObject> GetItem = new Dictionary<int, ItemObject>();    //GetItem字典<key: 物品ID, value: 物品>，用於給ID獲得物品資訊

    [ContextMenu("Update ID's")]
    public void UpdateID()
    {
        for (int i = 0; i < ItemObjects.Length; i++)
        {
            if (ItemObjects[i].data.Id != i)
                ItemObjects[i].data.Id = i;
        }
    }    
    public void OnAfterDeserialize()        //加入物品陣列中的物品及設定其id
    {
        //for (int i = 0; i < Items.Length; i++)
        //{
        //    Items[i].data.Id = i;
        //    GetItem.Add(i, Items[i]);
        //}
        UpdateID();
    }

    public void OnBeforeSerialize()         //初始化字典
    {
        //GetItem = new Dictionary<int, ItemObject>();
    }
}
