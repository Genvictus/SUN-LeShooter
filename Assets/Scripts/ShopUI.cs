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
    private bool flag = false;
    private Transform havePet;
    private Transform noMoney;
    GameObject[] pets;

    private void Awake()
    {
        container = transform.Find("container");
        petItem = container.Find("petItem");
        goldAmount = container.Find("GoldAmount");
        havePet = container.Find("havePet");
        noMoney = container.Find("noMoney");

    }

    private void Start()
    {
        // fill with pet prices
        CreateItemButton("Healing Tortoise", 50, 0);
        CreateItemButton("Attacking Tortoise", 50, 1);
        petItem.gameObject.SetActive(false);

        EventManager.StartListening("enableShop", SetFlag);
        Hide();
    }

    private void OnDestroy()
    {
        EventManager.StopListening("enableShop", SetFlag);
    }

    public void SetFlag(bool isQuestClear)
    {
        flag = isQuestClear;
        Debug.Log("flag: " + isQuestClear);
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
            if (TryBuyItem(positionIndex, itemCost))
            {
                Debug.Log("Buy item successful");
                goldAmount.gameObject.GetComponent<TextMeshProUGUI>().SetText(customer.GetGoldAmount().ToString());
            }
        });
    }

    public bool TryBuyItem(int index, int price)
    {
        pets = GameObject.FindGameObjectsWithTag("Pet");

        if (IsPetAlreadyHas(index))
        {
            Debug.Log("can't buy pet because already has");
            havePet.gameObject.SetActive(true);
            StartCoroutine(HideHavePetAfterDelay());
            return false;
        }

        bool Success = customer.BuyItem(index, price);

        if (!Success)
        {
            if (customer.GetGoldAmount() < price)
            {
                Debug.Log("can't buy pet because money");
                noMoney.gameObject.SetActive(true);
                StartCoroutine(HideNoMoneyAfterDelay());
            }
        }

        return Success;
    }

    bool IsPetAlreadyHas(int index)
    {
        if (index == 0)
        {
            name = "HealingTortoise";
        }
        else if (index == 1)
        {
            name = "AttackTortoise";
        }
        Debug.Log("nama pet yg coba dibeli1: " + name);
        foreach (var pet in pets)
        {
            Debug.Log("nama pet yg coba dibeli2: " + pet.name);
            if (name == pet.name)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator HideNoMoneyAfterDelay()
    {
        yield return new WaitForSeconds(1);

        // Nonaktifkan noMoney game object
        noMoney.gameObject.SetActive(false);
    }

    private IEnumerator HideHavePetAfterDelay()
    {
        yield return new WaitForSeconds(1);

        // Nonaktifkan havePet game object
        havePet.gameObject.SetActive(false);
    }
    public void Show(IShopCustomer customer)
    {
        if (!flag)
        {
            Debug.Log("can't show shop because quest isn't over");
            return;
        }


        CursorHandler.ShowCursor();

        this.customer = customer;
        goldAmount.gameObject.GetComponent<TextMeshProUGUI>().SetText(customer.GetGoldAmount().ToString());
        gameObject.SetActive(true);
    }

    public void ContinueGame()
    {
        CustomEvents.DisableShop();
        EventManager.TriggerEvent("AdvanceLevel");
        Hide();
    }

    public void Hide()
    {
        CursorHandler.HideCursor();
        gameObject.SetActive(false);
    }
}
