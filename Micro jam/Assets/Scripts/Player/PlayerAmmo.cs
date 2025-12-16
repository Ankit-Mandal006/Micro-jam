using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAmmo : MonoBehaviour
{
    public int maxAmmo = 100;
    public int currentAmmo = 0;
    public TMP_Text ammoText;         

    void Start()
    {
        UpdateAmmoUI();
    }

    public bool TryShoot()
    {
        if (currentAmmo <= 0)
            return false;

        currentAmmo--;
        UpdateAmmoUI();
        return true;
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo)
            currentAmmo = maxAmmo;

        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
            ammoText.text = "Ammo : "+currentAmmo.ToString();
    }
}
