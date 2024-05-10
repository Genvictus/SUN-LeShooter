using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

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

 /*       GameObject player = GameObject.FindGameObjectWithTag("player");
        customer = player.GetComponent<IShopCustomer>();*/
    }

    private void Start()
    {
        CreateItemButton("Healing Tortoise", 50, 0);
        CreateItemButton("Attacking Tortoise", 50, 1);
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

        /*petItemTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            TryBuyItem();
        };*/
    }

    public void TryBuyItem()
    {
        Debug.LogError("masuk beli");
        customer.BoughItem();
    }

    public void Show(IShopCustomer customer)
    {
        CursorHandler.ShowCursor();

        this.customer = customer;
        goldAmount.GetComponent<TextMeshProUGUI>().SetText(customer.getGoldAmount().ToString());
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        CursorHandler.HideCursor();

        gameObject.SetActive(false);
    }
}
