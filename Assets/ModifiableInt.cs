using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public delegate void ModifiedEvent();   //修改事件
    
[System.Serializable]
public class ModifiableInt
{
    /*
    可修改整數類別：
        功能：
            
     */
    [SerializeField]    //基本值
    private int baseValue;
    public int BaseValue { get { return baseValue; } set { baseValue = value; UpdateModifiedValue(); } }
        
    [SerializeField]    //修改值
    private int modifiedValue;
    public int ModifiedValue { get { return modifiedValue; } private set { modifiedValue = value; } }

    public List<IModifier> modifiers = new List<IModifier>();   //修改器界面清單

    public event ModifiedEvent ValueModified;   //值修改事件

    public ModifiableInt(ModifiedEvent method = null)   //可修改整數建構元，傳入修改事件(預設為null)
    {
        modifiedValue = BaseValue;
        if (method != null)
            ValueModified += method;    //值修改事件+傳入的修改事件
    }
    public void RegsiterModEvent(ModifiedEvent method)  //註冊修改事件到值修改事件
    {
        ValueModified += method;
    }
    public void UnregsiterModEvent(ModifiedEvent method)    //從值修改事件 解除註冊 修改事件
    {
        ValueModified -= method;
    }
    public void UpdateModifiedValue()   //更新修改值函數
    {
        var valueToAdd = 0;
        for(int i = 0; i < modifiers.Count; i++)
        {
            modifiers[i].AddValue(ref valueToAdd);  //逐一把修改器界面清單中的修改器(ItemBuff)的值加到valueToAdd
        }
        ModifiedValue = baseValue + valueToAdd; 
        if (ValueModified != null)  //如果值修改事件不為空
            ValueModified.Invoke(); //執行值修改事件
    }
    public void AddModifier(IModifier _modifier)    //新增修改器到修改器清單，並更新修改值
    {
        modifiers.Add(_modifier);
        UpdateModifiedValue();
    }
    public void RemoveModifier(IModifier _modifier) //從修改器清單刪除修改器，並更新修改值
    {
        modifiers.Remove(_modifier);
        UpdateModifiedValue();
    }
}
