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
        private PlayerInfo _gameInfo;
        private int _direction = 1;
        private Vector3 _diveDirection;

        private void Start()
        {
            _levitationHeight = Random.value * 2 + 3f;
            _gameInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
            _speed = _gameInfo.GetCoefficient();
            _player = GameObject.FindGameObjectWithTag("Player");
            _isDiving = false;
        }

        private void Update()
        {
            if (!_isDiving)
            {
                if (transform.position.y < _levitationHeight)
                {
                    transform.position += new Vector3(0, _speed * Time.deltaTime, 0);
                }
                if (transform.position.y > _levitationHeight)
                {
                    transform.position -= new Vector3(0, _speed * Time.deltaTime, 0);
                }
                transform.position += new Vector3(_speed * Time.deltaTime * _direction, 0, 0);
                if (transform.position.x > 7) _direction = -1;
                if (transform.position.x < -7) _direction = 1;
                if (Random.Range(0, 300000 / Time.time) < 1)
                {
                    _isDiving = true;
                    _diveDirection = _player.transform.position;
                }
            }
            else
            {
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
