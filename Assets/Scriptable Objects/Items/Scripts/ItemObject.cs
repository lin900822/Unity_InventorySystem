using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType    //enum建立物品分類
{
    Food,
    Helmet,
    Weapon,
    Shield,
    Boots,
    Chest,
    Default
}

public enum Attributes  //建立物品buff屬性
{
    Agility,
    Intellect,
    Stamina,
    Strengh
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
public class ItemObject : ScriptableObject
{
    /*
    功能：
    -物品物件，用於建立Prefab
    -可掛在不同遊戲物件上以表示其功能
     */
    public Sprite uiDisplay;    //物品Icon
    public bool stackable;      //是否可堆疊
    public ItemType type;       //物品類型（一般，武器，食物等等）
    [TextArea(15, 20)]          
    public string description;  //物品詳細資訊
    public Item data = new Item();  //物品資料，用於顯示UI

    public Item CreateItem()    //建立物品資料，回傳值為物品資料
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    /*
    功能：
    -物品基本資料，用於顯示UI
     */
    public string Name;         //物品名稱
    public int Id = -1;         //物品ID
    public ItemBuff[] buffs;    //物品加成
    public Item()   //預設建構元，預設名稱為 "" Id 為-1，用於建立空物件
    {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item)    //建構元，引數為ItemObject，用於建立以創建物品的資料
    {
        Name = item.name;
        Id = item.data.Id;
        buffs = new ItemBuff[item.data.buffs.Length];
        for(int i = 0; i < buffs.Length; i++)
        {     
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
            {
                attribute = item.data.buffs[i].attribute
            };
        }
    }
}


[System.Serializable]
public class ItemBuff : IModifier   
{
    /*
    功能：
    -物品加成，可選擇哪一種加成效果並給予隨機值
     */
    public Attributes attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }
    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }

    public void AddValue(ref int baseValue) //傳入基本值的 參照 ，並加上此ItemBuff的值
    {
        baseValue += value; 
    }
}