using UnityEngine;

public class DiceSound : MonoBehaviour
{
    public void OnTriggerEnter() {
        SoundManager.Instance.PlaySound("Dice");
    }  
}
