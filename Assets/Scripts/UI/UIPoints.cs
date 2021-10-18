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
        tmp.text = value.ToString() + "/" + max.ToString();
    }
}
