using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifier  //修改器介面，用於提供方法
{
    void AddValue(ref int baseValue);   //定義方法AddValue傳入基本值的參照(在ItemBuff類別中實作)
}
