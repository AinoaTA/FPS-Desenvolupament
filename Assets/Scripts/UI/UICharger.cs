using TMPro;
using UnityEngine;

public class UICharger : MonoBehaviour
{
    private TMP_Text tmp;
    private int current, max;

    private void Awake()
    {
        tmp = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        Shoot.delegateUI += UpdateText;
    }

    private void OnDisable()
    {
        Shoot.delegateUI -= UpdateText;
    }

    private void UpdateText(int current, int max, int forCharger)
    {
        if (forCharger * 0.8 < current)
            tmp.color = Color.white;
        else if (forCharger * 0.8 >= current && current >= forCharger * 0.5)
            tmp.color = Color.yellow;
        else if (forCharger * 0.5 > current && current >= forCharger * 0.3)
            tmp.color = new Color(1.0f, 0.64f, 0.0f);
        else if (forCharger * 0.3 > current && current >= 0)
            tmp.color = Color.red;


        
        tmp.text = current + "/" + max;
    }
}
