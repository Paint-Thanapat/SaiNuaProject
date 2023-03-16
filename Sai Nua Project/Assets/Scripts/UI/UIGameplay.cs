using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameplay : MonoBehaviour
{
    [Header("Default Setting")]
    public bool isHideInfo;
    public GameObject playerInfo;
    public GameObject playerHealth;

    [Header("Player Health")]
    public TMP_Text playerHealthText;
    public Image playerHealthBarFill;

    [Header("Player Info")]
    public TMP_Text attackDamageText;
    public TMP_Text criticalRateText;
    public TMP_Text projectileSpeedText;
    public TMP_Text attackRangeText;
    public TMP_Text fireRateText;

    [Header("Player Bonus Info")]
    public TMP_Text bonusAttackDamageText;
    public TMP_Text bonusAttackRangeText;
    public TMP_Text bonusFireRateText;

    [Header("Player Dash")]
    public Image playerDashFill;
    public GameObject particleOnEnableDash;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UIGameplay = this;
        SetGamePlayUI(false);
    }

    public void SetPlayerHealth(Health health)
    {
        playerHealthText.text = health.currentHealth + " / " + health.maxHealth;
        playerHealthBarFill.fillAmount = health.currentHealth / health.maxHealth;
    }
    public void SetInfoText(IWeapon weapon)
    {
        attackDamageText.text = weapon.Damage.ToString();
        criticalRateText.text = weapon.CriticalRate.ToString();
        projectileSpeedText.text = weapon.ProjectileSpeed.ToString();
        attackRangeText.text = weapon.Range.ToString();
        fireRateText.text = weapon.Rate.ToString();
    }

    public void SetBonusInfoText(PlayerWeaponHolder playerWeaponHolder)
    {
        if (playerWeaponHolder.bonusDamageMultiplier > 1)
        {
            bonusAttackDamageText.text = "x " + playerWeaponHolder.bonusDamageMultiplier.ToString();
        }
        else
        {
            bonusAttackDamageText.text = " ";
        }

        if (playerWeaponHolder.bonusAttackRange > 0)
        {
            bonusAttackRangeText.text = "+ " + playerWeaponHolder.bonusAttackRange.ToString();
        }
        else
        {
            bonusAttackRangeText.text = " ";
        }

        if (playerWeaponHolder.bonusFireRate > 0)
        {
            bonusFireRateText.text = "+ " + playerWeaponHolder.bonusFireRate.ToString();
        }
        else
        {
            bonusFireRateText.text = " ";
        }
    }

    public void SetGamePlayUI(bool set)
    {
        float colorAlpha = 0f;
        if (set)
        {
            colorAlpha = 1f;
        }

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        foreach (TMP_Text text in texts)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, colorAlpha);
        }

        Image[] images = GetComponentsInChildren<Image>();

        foreach (Image img in images)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, colorAlpha);
        }
    }

    public void ToggleShowInfo()
    {
        TMP_Text[] texts = playerInfo.GetComponentsInChildren<TMP_Text>();

        isHideInfo = !isHideInfo;

        foreach (TMP_Text text in texts)
        {
            if (isHideInfo)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            }
            else
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            }
        }
    }
}
