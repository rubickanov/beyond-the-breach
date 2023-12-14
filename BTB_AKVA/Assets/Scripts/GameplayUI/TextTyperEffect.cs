using AKVA.Player;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextTyperEffect : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private string sourceText;

    private float delay = 0.1f;
    private float animationTime = 0.6f;

    private float delayTimer;
    private float animationTimer;
    private float delayBetweenCharsTimer;

    //private readonly string allChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890";
    private readonly string allChars = "01";
    private int index = 0;
    private int indexCount = -2;

    [SerializeField] private BooleanReference useUIAnimation;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        sourceText = tmp.text;

        delayTimer = delay;
        delayBetweenCharsTimer = animationTime;
        if (sourceText.Length != 0)
        {
            animationTime /= sourceText.Length;
        }
    }

    private void Update()
    {
        if (useUIAnimation.value)
        {
            delayBetweenCharsTimer -= Time.unscaledDeltaTime;
            if (delayBetweenCharsTimer <= 0)
            {
                indexCount++;
                delayBetweenCharsTimer = animationTime;
            }


            delayTimer -= Time.unscaledDeltaTime;
            for (int i = 0; i < sourceText.Length; i++)
            {
                if (delayTimer <= 0)
                {
                    tmp.text = RandomChars();
                    delayTimer = delay;
                }
            }
        }
        
        
    }

    private string RandomChars()
    {
        char[] randomChars = new char[sourceText.Length];

        for (int i = 0; i < sourceText.Length; i++)
        {
            randomChars[i] = RandomChar();

            if (i <= indexCount)
            {
                randomChars[i] = sourceText[i];
            }
        }

        return new string(randomChars);
    }

    private char RandomChar()
    {
        int randomIndex = Random.Range(0, allChars.Length);
        char randomChar = allChars[randomIndex];

        return randomChar;
    }

    private void OnEnable()
    {
        indexCount = -2;
    }
}