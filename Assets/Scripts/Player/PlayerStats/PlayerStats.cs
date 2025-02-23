using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private Image hpBar;

    [Space]
    [SerializeField] private float maxHunger;
    [SerializeField] private float hungerLostPerSec;
    [SerializeField] private Image hungerBar;

    private float curHp;
    private float curHunger;

    [Header("Money")]
    [SerializeField] private int startMoney;
    [SerializeField] private TMP_Text moneyText;
    public int CurMoney => curMoney;
    private int curMoney;

    void Start()
    {
        curHp = maxHp;
        hpBar.fillAmount = curHp / maxHp;

        curHunger = maxHunger;
        hungerBar.fillAmount = curHunger / maxHunger;

        curMoney = startMoney;
        moneyText.text = curMoney.ToString();
    }

    public void TakeDamage(float dmg)
    {
        curHp -= dmg;
        hpBar.fillAmount = curHp / maxHp;

        if (curHp <= 0)
        {
            Die();
        }
    }

    public void GetHungry(float hunger)
    {
        curHunger -= hunger;
        hungerBar.fillAmount = curHunger / maxHunger;

        if (curHunger <= 0)
        {
            Die();
        }
    }

    public void GetMoney(int gold)
    {
        curMoney += gold;
        moneyText.text = curMoney.ToString();
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    void Update()
    {
        GetHungry(hungerLostPerSec * Time.deltaTime);
    }
}
