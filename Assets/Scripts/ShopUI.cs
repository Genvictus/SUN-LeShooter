using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private Transform petItem;
    private Transform container;
    private Transform goldAmount;
    private IShopCustomer customer;

    private void Awake()
    {
        container = transform.Find("container");
        petItem = container.Find("petItem");
        goldAmount = container.Find("GoldAmount");

    }

    private void Start()
    {
        // fill with pet prices
        CreateItemButton("Healing Tortoise", 50, 0);
        CreateItemButton("Attacking Tortoise", 50, 1);
        petItem.gameObject.SetActive(false);
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

        petItemTransform.gameObject.GetComponent<Button>().onClick.AddListener(delegate
        {
            if (BuyItem(itemCost))
            {
                Debug.Log("Buy Item Successful");
                goldAmount.gameObject.GetComponent<TextMeshProUGUI>().SetText(customer.GetGoldAmount().ToString());
            }
        });
    }

    public bool BuyItem(int price)
    {
        Debug.Log("Try Buying Item");
        return customer.BuyItem(price);
    }

    public void Show(IShopCustomer customer)
    {
        CursorHandler.ShowCursor();

        this.customer = customer;
        goldAmount.gameObject.GetComponent<TextMeshProUGUI>().SetText(customer.GetGoldAmount().ToString());
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        CursorHandler.HideCursor();

        gameObject.SetActive(false);
    }
}
