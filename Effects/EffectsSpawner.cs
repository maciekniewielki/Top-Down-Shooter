using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EffectsSpawner : MonoBehaviour
{
    //TODO add wave countdown

    public static EffectsSpawner instance;
    public FadingText fadeText;
    public Text nextWaveCountdownText;
    public Text nextWaveCountdownNumber;
    public Text currentWaveText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
	}

    void Start()
    {

        GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
        if (spawner == null)
            return;
        ZombieSpawner script = spawner.GetComponent<ZombieSpawner>();
        if (script == null)
            return;
        script.waveTimerTick += SetCountDownNumber;
        script.beginningNextWave += ShowCurrentWaveText;
        script.endOfWave += SpawnCountDown;
    }

    public void SpawnFadeTextAtPos(Vector2 position, string text="", Color color=new Color())
    {
        FadingText fadingText = Instantiate(fadeText, position, Quaternion.identity) as FadingText;
        fadingText.SetText(text);
        fadingText.GetComponent<Renderer>().material.color = color;
    }

    public void SpawnCountDown(int time)
    {
        nextWaveCountdownText.gameObject.SetActive(true);
    }

    public void SetCountDownNumber(int number)
    {
        nextWaveCountdownNumber.text = number.ToString();
    }

    void ShowCurrentWaveText(int number)
    {
        nextWaveCountdownText.gameObject.SetActive(false);
        currentWaveText.text = string.Format("Wave {0}!", number);
        StartCoroutine("Fade", new MoveInformation(currentWaveText.rectTransform, Vector2.zero, 2f));
    }

    IEnumerator MoveTo(MoveInformation info)
    {
        Debug.Log(info.destination);
        Debug.Log(info.whatToMove.anchoredPosition);
        Debug.Log(Vector2.Distance(info.destination, info.whatToMove.anchoredPosition));
        
        RectTransform thingToMove = info.whatToMove;
        Vector2 moveDirection = (info.destination - thingToMove.anchoredPosition).normalized;
        float moveSpeed = Vector2.Distance(thingToMove.anchoredPosition, info.destination)*Time.deltaTime / info.timeToReach;
        Debug.Log(moveSpeed);
        while (Vector2.Distance(thingToMove.anchoredPosition, info.destination) > moveSpeed)
        {
            thingToMove.anchoredPosition+=(moveDirection * moveSpeed);
            yield return null;
        }
        thingToMove.anchoredPosition = info.destination;
    }

    IEnumerator Fade(MoveInformation info)
    {
        float timeElapsed = 0;
        Graphic target = info.whatToMove.GetComponent<Graphic>();
        while (timeElapsed < info.timeToReach)
        {
            target.color = new Color(target.color.r, target.color.g, target.color.b, 1-timeElapsed / info.timeToReach);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        target.color = new Color(target.color.r, target.color.g, target.color.b, 0f);
    }
	
}
