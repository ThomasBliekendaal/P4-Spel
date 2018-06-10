using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour {

	public enum TowerType { minigun,rocket,frost,fire};
    public Text InputName;
    public Text InputInfo;
    public TowerType tower;
    public GameObject location;
    public Towers towerInfo;

    public void Awake()
    {
        tower = TowerType.minigun;
        SetInfo();
    }

    public void SetInfo()
    {
        if (tower == TowerType.minigun)
        {
            InputName.text = towerInfo.minigun.towerName;
            InputInfo.text = towerInfo.minigun.towerInfo;
        }
        else if (tower == TowerType.rocket)
        {
            InputName.text = towerInfo.rocket.towerName;
            InputInfo.text = towerInfo.rocket.towerInfo;
        }
        else if (tower == TowerType.frost)
        {
            InputName.text = towerInfo.frost.towerName;
            InputInfo.text = towerInfo.frost.towerInfo;
        }
        else if (tower == TowerType.fire)
        {
            InputName.text = towerInfo.fire.towerName;
            InputInfo.text = towerInfo.fire.towerInfo;
        }
    }

    public void Finish()
    {
        if(tower == TowerType.minigun)
        {
            location.GetComponent<TurretDeploy>().SetTurret(towerInfo.minigun.tower);
        }
        else if (tower == TowerType.rocket)
        {
            location.GetComponent<TurretDeploy>().SetTurret(towerInfo.rocket.tower);
        }
        else if (tower == TowerType.frost)
        {
            location.GetComponent<TurretDeploy>().SetTurret(towerInfo.frost.tower);
        }
        else if (tower == TowerType.fire)
        {
            location.GetComponent<TurretDeploy>().SetTurret(towerInfo.fire.tower);
        }
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
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
    public void Fire()
    {
        tower = TowerType.fire;
        SetInfo();
    }
}

[System.Serializable]
public class Towers
{
    public TurretInfo minigun;
    public TurretInfo rocket;
    public TurretInfo frost;
    public TurretInfo fire;
}

[System.Serializable]
public class TurretInfo
{
    public string towerName;
    public string towerInfo;
    public GameObject tower;
}