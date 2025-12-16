using UnityEngine;

public enum BulletType
{
    Gift,
    Coal
}

public class Bullet : MonoBehaviour
{
    public BulletType bulletType;

    public Sprite giftSprite;
    public Sprite coalSprite;

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        UpdateSprite();
    }

    public void SetType(BulletType type)
    {
        bulletType = type;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if (sr == null) return;

        switch (bulletType)
        {
            case BulletType.Gift:
                sr.sprite = giftSprite;
                break;
            case BulletType.Coal:
                sr.sprite = coalSprite;
                break;
        }
    }
}
