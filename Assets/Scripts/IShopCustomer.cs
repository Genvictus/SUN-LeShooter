using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
    void BoughItem(int index);

    int GetGoldAmount();

    void SpendGold(int spend);
}
