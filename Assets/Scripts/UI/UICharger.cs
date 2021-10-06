using TMPro;
using UnityEngine;

public class UICharger : MonoBehaviour
{
    private TMP_Text tmp;

    private void Awake()
    {
        tmp = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        ShootGun.delegateUI += UpdateText;
    }

    private void OnDisable()
    {
        ShootGun.delegateUI -= UpdateText;
    }

    private void UpdateText(string bulletText)
    {
        tmp.text = bulletText;
    }
}
