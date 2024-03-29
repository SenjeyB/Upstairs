using GameCont;
using UnityEngine;

namespace EnemyAI.Bat
{
    public class BatMovement : MonoBehaviour
    {
        private float _levitationHeight;
        private float _speed;
        private GameObject _player;
        private bool _isDiving;
        private bool _canDiving;
        private PlayerInfo _gameInfo;
        private int _direction = 1;
        private Vector3 _diveDirection;

        private void Start()
        {
            _levitationHeight = Random.value * 2 + 2.5f;
            _gameInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
            _speed = Mathf.Min(_gameInfo.GetCoefficient() / 2, 3.2f);
            _player = GameObject.FindGameObjectWithTag("Player");
            _isDiving = false;
            _canDiving = false;
        }

        private void Update()
        {
            if (!_isDiving)
            {
                if (transform.position.y < _levitationHeight)
                {
                    transform.position += new Vector3(0, _speed * Time.deltaTime, 0);
                }
                else if (transform.position.y > _levitationHeight)
                {
                    transform.position -= new Vector3(0, _speed * Time.deltaTime, 0);
                }
                if (Mathf.Abs(transform.position.y - _levitationHeight) < 0.4f)
                {
                    _canDiving = true;
                }
                transform.position += new Vector3(_speed * Time.deltaTime * _direction, 0, 0);
                if (transform.position.x > 7) _direction = -1;
                if (transform.position.x < -7) _direction = 1;
                if (Random.Range(0, 55000 * Time.deltaTime) < 1 && _canDiving)
                {
                    _isDiving = true;
                    _diveDirection = _player.transform.position;
                }
            }
            else
            {
                _canDiving = false;
                Vector3 direction = (_diveDirection - transform.position).normalized;
                transform.position += direction * _speed * Time.deltaTime * 4;
                if (!(Vector3.Distance(transform.position, _diveDirection) < 0.3f)) return;
                _isDiving = false;
                _levitationHeight = Random.value * 2 + 3f;
            }
        }

        public void Slow()
        {
            _speed /= 2;
        }
    }
}
