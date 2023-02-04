using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Bar;
    
    public void OnTakeDamage(HealthChangedEventArgs args)
    {
        Bar.fillAmount = (float)args.NewValue / args.MaxHealth;
    }
}