using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteTimer : MonoBehaviour
{
    public Sprite sprite1, sprite2;
    public float cooldown1 = 1f; 
    public float cooldown2 = 2f; 

    private float timer;
    private bool isSprite1 = true;
    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
        img.sprite = sprite1;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float currentCooldown = isSprite1 ? cooldown1 : cooldown2;

        if (timer >= currentCooldown)
        {
            timer = 0;

            if (isSprite1)
            {
                img.sprite = sprite2;
            }
            else
            {
                img.sprite = sprite1;
            }

            isSprite1 = !isSprite1;
        }
    }
}
