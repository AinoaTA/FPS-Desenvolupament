using UnityEngine;

public class ShootGun : MonoBehaviour
{
    private float maxBulletSaved = 100f;
    private float bulletForCharger = 50f;
    private float currentBullets = 50f;

    //public GameObject Player;
    //private FPS playerFPS;
    //private Shoot states;
    public delegate void DelegateUI(string text);
    public static DelegateUI delegateUI;


    private void Start()
    {
        //playerFPS = Player.GetComponent<FPS>();
        //states = Player.GetComponent<Shoot>();
        currentBullets = bulletForCharger;
        UpdateTextUI();
    }

    public void Charger()
    {
        if (maxBulletSaved > 0)
        {
            maxBulletSaved -= (bulletForCharger-currentBullets);
            if (maxBulletSaved <= 0)
                maxBulletSaved = 0;

            currentBullets = bulletForCharger;
            
            UpdateTextUI();

        }
        
    }
    public void UpdateBullets()
    {
        currentBullets -= 1;

        UpdateTextUI();
    }
    private void UpdateTextUI()
    {
        delegateUI?.Invoke(currentBullets.ToString() + " / " + maxBulletSaved.ToString());
    }
}
