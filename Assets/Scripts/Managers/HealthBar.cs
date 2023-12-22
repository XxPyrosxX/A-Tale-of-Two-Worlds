using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image health;
    public Gradient gradient;

    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Health").GetComponent<Image>();
    }

    public void SetCurrentHealth(float amount)
    {
        health.fillAmount = amount;
        health.color = gradient.Evaluate(amount);
    }

    public void SetMaxHealth(float amount)
    {
        health.fillAmount = amount;
        health.color = gradient.Evaluate(1f);
    }

}
