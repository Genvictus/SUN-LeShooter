using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class ShopUI : MonoBehaviour
{
    private Transform petItem;
    private Transform container;
    private IShopCustomer customer;
    
    private void Awake()
    {
        container = transform.Find("container");
        petItem = container.Find("petItem");
    }

    private void Start()
    {
        CreateItemButton("Healing Tortoise", 200, 0);
        CreateItemButton("Attacking Tortoise", 200, 1);
        Hide();
    }

    private void CreateItemButton(string itemName, int itemCost, int positionIndex)
    {
        Transform petItemTransform = Instantiate(petItem, container);
        RectTransform petItemRectTransform = petItemTransform.GetComponent<RectTransform>();

        float petItemHeight = 100f;
        petItemRectTransform.anchoredPosition = new Vector3(0, -petItemHeight * positionIndex, 0);

        petItemTransform.Find("Petname").GetComponent<TextMeshProUGUI>().SetText(itemName);
        petItemTransform.Find("Price").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        petItemTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            TryBuyItem();
        };
    }

    private void TryBuyItem()
    {
        customer.BoughItem();
    }

    public void Show(IShopCustomer customer)
    {
        this.customer = customer;
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
