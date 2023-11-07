using System.Collections;
using AKVA.Interaction;
using UnityEngine;
using UnityEngine.UI;


namespace AKVA.UI
{
    public class ReticleUI : MonoBehaviour
    {
        [Header("PLAYER")] 
        [SerializeField] private GameObject player;
        private MindControl mindControl;
        private Interaction.Interaction interaction;
        private Picking picking;
        
        [Header("RETICLE IMAGE HOLDER")] 
        [SerializeField] private Image imageHolder;

        [Header("RETICLE TYPES")] 
        [SerializeField] private Sprite defaultReticle;
        [SerializeField] private Sprite interactReticle;
        [SerializeField] private Sprite grabReticle;
        [SerializeField] private Sprite mindControlReticle;
        [SerializeField] private Sprite returnToBodyReticle;
        
        [Header("TIMINGS")]
        [SerializeField] private float timeToEnable;
        [SerializeField] private float timeToDisable;
        [SerializeField] private AnimationCurve curveToEnable;
        [SerializeField] private AnimationCurve curveToDisable;

        private bool isEnabled;


        private const int MAX_ALPHA = 255;
        private const int MIN_ALPHA = 0;

        private void Awake()
        {
            mindControl = player.GetComponent<MindControl>();
            interaction = player.GetComponent<Interaction.Interaction>();
            picking = player.GetComponent<Picking>();
        }

        private void Start()
        {
            Enable();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (isEnabled)
                {
                    Disable();
                }
                else
                {
                    Enable();
                }
            }
        }

        private void SetDefaultUI()
        {
            imageHolder.sprite = defaultReticle;
        }

        private void SetInteractionUI()
        {
            imageHolder.sprite = interactReticle;
        }

        private void SetMindControlUI()
        {
            imageHolder.sprite = mindControlReticle;
        }
        
        private void Enable()
        {
            StartCoroutine(EnableReticle());
        }

        private void Disable()
        {
            StartCoroutine(DisableReticle());
        }

        private IEnumerator EnableReticle()
        {
            StopCoroutine(DisableReticle());
            isEnabled = true;

            Debug.Log("ENABLE");
            float f = 0f;
            Color tempColor = imageHolder.color;

            float fPerTick = 1 / timeToEnable;
            while (f < 1f)
            {
                f += Time.deltaTime * fPerTick;
                float a = curveToEnable.Evaluate(f);
                tempColor.a = a;
                imageHolder.color = tempColor;
                yield return null;

            }
        }

        private IEnumerator DisableReticle()
        {
            StopCoroutine(EnableReticle());
            isEnabled = false;

            Debug.Log("DISABLE");
            float f = 1f;
            Color tempColor = imageHolder.color;

            float fPerTick = 1 / timeToDisable;
            while (f > 0f)
            {
                f -= Time.deltaTime * fPerTick;
                float a = curveToDisable.Evaluate(f);
                tempColor.a = a;
                imageHolder.color = tempColor;
                yield return null;
            }
        }
    }
}
