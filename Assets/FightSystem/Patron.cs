using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.FightSystem
{
    public class Patron : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private GameObject _hex;
        [SerializeField] private float _speed;
        [SerializeField] private bool _isFly = false;
        [SerializeField] private float _distanceForBang;
        [SerializeField] private IDamage _damageObject;

        public void Setup(float damage, GameObject hex, IDamage damageObject)
        {
            _damage = damage;
            _damageObject = damageObject;
            _hex = hex;
            _isFly = true;
        }

        public void FlyPatron()
        {
            transform.position = Vector2.MoveTowards(transform.position, _hex.transform.position, _speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _hex.transform.position) < _distanceForBang)
            {
                _isFly = false;
                _damageObject.TakeDamage(_damage);
                _damageObject.CheckDeath();
                
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (_isFly)
                FlyPatron();
        }
    }
}
