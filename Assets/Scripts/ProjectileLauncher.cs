using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _refProjectile;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private Transform _refGunPivotL, _refGunPivotR;
    private GameObject _projectile;
    private Rigidbody _projectileRbody, _spaceCraftRbody;
    private bool _isLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        _spaceCraftRbody = GetComponent<Rigidbody>();
        
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                if(_isLeft)
                {
                    _projectile = Instantiate(_refProjectile);
                    _projectile.transform.position = _refGunPivotL.transform.position;
                    _projectile.transform.rotation = _refGunPivotL.transform.rotation;

                    _projectileRbody = _projectile.GetComponent<Rigidbody>();
                    _projectileRbody.velocity = transform.forward * _projectileSpeed + _spaceCraftRbody.velocity;
                }

                if(!_isLeft)
                {
                    _projectile = Instantiate(_refProjectile);
                    _projectile.transform.position = _refGunPivotR.transform.position;
                    _projectile.transform.rotation = _refGunPivotR.transform.rotation;

                    _projectileRbody = _projectile.GetComponent<Rigidbody>();
                    _projectileRbody.velocity = transform.forward * _projectileSpeed + _spaceCraftRbody.velocity;
                }

                _isLeft = !_isLeft;

                yield return new WaitForSeconds(0.1f);
            }

            yield return null;
        }
    }
}
