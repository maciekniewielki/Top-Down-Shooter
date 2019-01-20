using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    public Slider healthBar;
    public Slider scrapBar;
    public Text healthBarText;
    public Text scrapBarText;
    public Button restartButton;
    public Text turretTip;

    public float attackSpeed;
    public float maxHealth;
    public PlayerCannon weapon;
    public GameObject[] turretPrefabs;
    public int[] turretCosts;
    public static Player instance;

    private float health;
    private int scrapAmount;
    private int maxScrapValue;

    public int ScrapAmount
    {
        get
        {
            return scrapAmount;
        }

        set
        {
            value = Mathf.Clamp(value, 0, maxScrapValue);
            scrapAmount = value;
            scrapBar.value = value;
            scrapBarText.text = value.ToString();
            if (value >= 100)
                turretTip.gameObject.SetActive(true);
            else
                turretTip.gameObject.SetActive(false);
        }
    }

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            value = Mathf.Clamp(value, 0, maxHealth);
            health = value;
            healthBar.value = value;
            healthBarText.text = string.Format("{0:0.0}", value);
        }
    }

    void Awake()
    {
        ScrapAmount = 0;
        maxScrapValue = 200;
        instance = this;
        weapon = GetComponentInChildren<PlayerCannon>();
        healthBar.maxValue = maxHealth;
        scrapBar.maxValue = maxScrapValue;
        Health = maxHealth;
        weapon.TimeBetweenShots = 1 / (attackSpeed + float.Epsilon);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PlaceTurret(Random.Range(0,2));
    }

    void PlaceTurret(int which)
    {
        if (ScrapAmount < turretCosts[which])
            return;
        Instantiate(turretPrefabs[which], transform.position, turretPrefabs[which].transform.rotation);
        ScrapAmount -= turretCosts[which];
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Scrap"))
        {
            ScrapAmount += 10;
            EffectsSpawner.instance.SpawnFadeTextAtPos(transform.position, string.Format("+{0}", 10), new Color(54f / 255, 106f / 255, 1f));
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("MoreScrap"))
        {
            ScrapAmount += 50;
            EffectsSpawner.instance.SpawnFadeTextAtPos(transform.position, string.Format("+{0}", 50), new Color(54f / 255, 106f / 255, 1f));
            Destroy(collision.gameObject);
        }
    }


    #region IDamageable implementation

    public void TakeDamage(float amount)
    {
        Health -= amount;
        EffectsSpawner.instance.SpawnFadeTextAtPos(transform.position, string.Format("-{0:0.0}", amount), new Color(1f, 0f, 0f));
        if (Health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
        Game.state = GameState.GAME_OVER;
        restartButton.gameObject.SetActive(true);
        Game.PlayerDied();
    }

    #endregion
}

