using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Shield : MonoBehaviour {

    //SHIELD FUNCIONALITY HAS TO BE MOVED TO SHIELD.CS LATER ON
    //ONLY FOR DEVELOPMENT IN THIS BEHAVIOUR
    public delegate void ShieldEvent();
    public static event ShieldEvent OnShieldActivated;
    public static event ShieldEvent OnShieldHit;
    public static event ShieldEvent OnShieldDestroyed;


    [SerializeField] private int _shieldDuration = 99;
    [SerializeField] private int _shieldPower = 20;
    [SerializeField] private List<string> _collisionTags;
    [SerializeField] private GameObject _destroyParticles;

    private int _currentShieldPower;
    private float _currentShieldDuration;
    private float _currentShieldPowerPercentage;
    private const float _smoothness = .012f;
    private SpriteRenderer _childSprite;
    private Material _shieldMaterial;


    private void Awake()
    {
        _shieldMaterial = GetComponent<MeshRenderer>().material;
        _currentShieldPower = _shieldPower;
        _currentShieldDuration = _shieldDuration;
        if(GetComponentInChildren<SpriteRenderer>() != null)
        {
            _childSprite = GetComponentInChildren<SpriteRenderer>();
        }
        if(OnShieldActivated != null)
        {
            OnShieldActivated();
        }
    }

    private void FixedUpdate()
    {
        if(_currentShieldDuration > 0)
        {
            _currentShieldDuration -= _smoothness;
        }
        if(_currentShieldDuration <= 0 || _currentShieldPower == 0)
        {
            DestroyShield();
        }
    }

    private void DestroyShield()
    {
        if (OnShieldDestroyed != null)
        {
            OnShieldDestroyed();
        }
        GameObject particles = Instantiate(_destroyParticles, transform.position, Quaternion.identity, null);
        Destroy(this.gameObject);
    }

    private void ShieldHit()
    {
        StopAllCoroutines();
        _currentShieldPower--;
        _currentShieldPowerPercentage = (float)_currentShieldPower / (float)_shieldPower;
        _shieldMaterial.SetFloat("_Damage", (1 - _currentShieldPowerPercentage));
        Color newColor = Color.Lerp(_shieldMaterial.GetColor("_ForceFieldColor"), _shieldMaterial.GetColor("_ForceFieldDamagedColor"), (1 - _currentShieldPower));
        _childSprite.color = newColor;
        StartCoroutine(MaterialImpactEffect());
    }

    private IEnumerator MaterialImpactEffect()
    {
        float t = 0;
        float increment = _smoothness / 0.25f;

        _shieldMaterial.SetFloat("_ForceFieldPower", 0);

        while(t < 1)
        {
            _shieldMaterial.SetFloat("_ForceFieldPower", t);
            t += increment;
            yield return new WaitForSecondsRealtime(_smoothness);
        }
        if(t >= 1)
        {
            yield break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGo = collision.gameObject;
        if(_collisionTags.Contains(otherGo.tag))
        {
            ShieldHit();
            Debug.Log("HIT BY + " + otherGo.name);
            Destroy(otherGo);
        }
    }

}
