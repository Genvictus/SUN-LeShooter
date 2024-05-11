using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
    bool BuyItem(int index, int price);

    int GetGoldAmount();

    bool SpendGold(int spend);
}
