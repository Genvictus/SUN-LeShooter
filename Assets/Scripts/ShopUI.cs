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

 /*       GameObject player = GameObject.FindGameObjectWithTag("player");
        customer = player.GetComponent<IShopCustomer>();*/
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
            /*if(itemName == "Healing Tortoise")
            {
                TryBuyItem("HealingTortoise");
            }else if(itemName == "Attacking Tortoise")
            {
                TryBuyItem("AttackTortoise");
            }*/
            TryBuyItem(positionIndex);
            
        };
    }

    public void TryBuyItem(int index)
    {
        pets = GameObject.FindGameObjectsWithTag("Pet");


        if(customer.getGoldAmount() < 200)
        {
            Debug.Log("can't buy pet because money");
            noMoney.gameObject.SetActive(true);
            StartCoroutine(HideNoMoneyAfterDelay());
        }else if (IsPetAlreadyHas(index))
        {
            Debug.Log("can't buy pet because already has");
            havePet.gameObject.SetActive(true);
            StartCoroutine(HideHavePetAfterDelay());
        }
        else
        {
            customer.BoughItem(index);
        }
        
    }

    bool IsPetAlreadyHas(int index)
    {
        if(index == 0)
        {
            name = "HealingTortoise";
        }else if(index == 1)
        {
            name = "AttackTortoise";
        }
        Debug.Log("nama pet yg coba dibeli1: " + name);
        foreach (var pet in pets)
        {
            Debug.Log("nama pet yg coba dibeli2: " + pet.name);
            if(name == pet.name)
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

        // Nonaktifkan noMoney game object
        havePet.gameObject.SetActive(false);
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
