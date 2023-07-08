using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour //May Combine this with Level Manager
{
    public static MoneyManager instance;
    [SerializeField] private int playerMoney = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        if (instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetMoney()
    {
        return playerMoney;
    }
    public void SetMoney(int money)
    {
        playerMoney += money;
    }
}
