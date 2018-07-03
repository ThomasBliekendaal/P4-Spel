using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour {

	public enum TowerType { minigun,rocket,frost};
    public Text InputName;
    public Text InputInfo;
    public TowerType tower;
    public GameObject location;
    public Towers towerInfo;
    public Currency currency;
    public Text currencyInput;

    public void OnEnable()
    {
        if(currency.currency < towerInfo.minigun.cost)
        {
            towerInfo.minigun.allowBuy = false;
            towerInfo.minigun.disabled.SetActive(true);
        }
        else
        {
            towerInfo.minigun.allowBuy = true;
            towerInfo.minigun.disabled.SetActive(false);
        }
        if (currency.currency < towerInfo.rocket.cost)
        {
            towerInfo.rocket.allowBuy = false;
            towerInfo.rocket.disabled.SetActive(true);
        }
        else
        {
            towerInfo.rocket.allowBuy = true;
            towerInfo.rocket.disabled.SetActive(false);
        }
        if (currency.currency < towerInfo.frost.cost)
        {
            towerInfo.frost.allowBuy = false;
            towerInfo.frost.disabled.SetActive(true);
        }
        else
        {
            towerInfo.frost.allowBuy = true;
            towerInfo.frost.disabled.SetActive(false);
        }
    }

    public void Awake()
    {
        tower = TowerType.minigun;
        SetInfo();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            CloseUi();
        }
    }

    public void CloseUi()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }

    public void SetInfo()
    {
        if (tower == TowerType.minigun)
        {
            InputName.text = towerInfo.minigun.towerName;
            InputInfo.text = towerInfo.minigun.towerInfo;
            currencyInput.text = towerInfo.minigun.cost.ToString();
        }
        else if (tower == TowerType.rocket)
        {
            InputName.text = towerInfo.rocket.towerName;
            InputInfo.text = towerInfo.rocket.towerInfo;
            currencyInput.text = towerInfo.rocket.cost.ToString();
        }
        else if (tower == TowerType.frost)
        {
            InputName.text = towerInfo.frost.towerName;
            InputInfo.text = towerInfo.frost.towerInfo;
            currencyInput.text = towerInfo.frost.cost.ToString();
        }
    }

    public void Finish()
    {
        if(tower == TowerType.minigun && towerInfo.minigun.allowBuy)
        {
            location.GetComponent<TurretDeploy>().SetTurret(towerInfo.minigun.tower);
            currency.currency -= towerInfo.minigun.cost;
            CloseUi();
        }
        else if (tower == TowerType.rocket && towerInfo.rocket.allowBuy)
        {
            location.GetComponent<TurretDeploy>().SetTurret(towerInfo.rocket.tower);
            currency.currency -= towerInfo.rocket.cost;
            CloseUi();
        }
        else if (tower == TowerType.frost && towerInfo.frost.allowBuy)
        {
            location.GetComponent<TurretDeploy>().SetTurret(towerInfo.frost.tower);
            currency.currency -= towerInfo.frost.cost;
            CloseUi();
        }
    }

    public void Minigun()
    {
        tower = TowerType.minigun;
        SetInfo();
    }
    public void Rocket()
    {
        tower = TowerType.rocket;
        SetInfo();
    }
    public void Frost()
    {
        tower = TowerType.frost;
        SetInfo();
    }

    public void HoverExit()
    {
        InputName.text = "";
        InputInfo.text = "";
        currencyInput.text = "";
    }
}

[System.Serializable]
public class Towers
{
    public TurretInfo minigun;
    public TurretInfo rocket;
    public TurretInfo frost;
}

[System.Serializable]
public class TurretInfo
{
    public string towerName;
    [TextArea]
    public string towerInfo;
    public GameObject tower;
    public int cost;
    public GameObject disabled;
    public bool allowBuy;
}