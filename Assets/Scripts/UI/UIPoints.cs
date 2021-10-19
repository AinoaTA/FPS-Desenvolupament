using TMPro;
using UnityEngine;

public class UIPoints : MonoBehaviour
{

    private TMP_Text tmp;

    private void Awake()
    {
        tmp = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        ShooterPoints.delegateUIPoints += UpdateText;
    }

    private void OnDisable()
    {
        ShooterPoints.delegateUIPoints -= UpdateText;
    }

    private void UpdateText(int value, int max)
    {
        if (value >= max)
            tmp.color = Color.green;
        else
            tmp.color = Color.white;

        if (gameObject != null)
        tmp.text = "Points: "+ value.ToString() + "/" + max.ToString();
    }
}
