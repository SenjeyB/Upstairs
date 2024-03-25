using UnityEngine;

namespace TowerStaff.Taser
{
    public class TaserShot : BasicShot
    {
        private bool _repairing;
        private void Start()
        {
            Invoke(nameof(Ending), 0.6f);
        }

        public void Upgrade()
        {
            _repairing = true;
        }
        
        private void Ending()
        {
            Destroy(gameObject);
        }
        
        public bool IsRepairing()
        {
            return _repairing;
        }
    }
}
