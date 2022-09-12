using UnityEngine;

namespace Assets.ProjectFiles._01Scripts.BasicScripts
{
    public class AudioVisualDetail : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float _radius = 200f;
        [SerializeField] private float _maxScale = 10000f;
        
        private GameObject _spawnPrefab;
        private GameObject[] _sampleGroup ;
        private int _sampleLength;
        private float _sampleAngle;

        private bool _isInitialized = false;
        
        #endregion Variables

        #region Unity Methods

        // private void Start()
        // {
        // } // End of Unity - Start

        private void Update()
        {
            Visualize();
        } // End of Unity - Update

        #endregion Unity Methods

        #region Help Methods

        public void Initialize(GameObject prefab)
        {
            _spawnPrefab = prefab;
            GetComponents();
            SpawnSamplePrefabs();
            
            _isInitialized = true;
        } // End of Initialize

        private void GetComponents()
        {
            _sampleLength = AudioPeer.GetSampleLength;
            SetSampleAngle();
        } // End of GetComponents

        private void SetSampleAngle()
        {
            _sampleAngle = 360f / _sampleLength ;
        } // End of SetSampleAngle
        
        private void SpawnSamplePrefabs()
        {
            _sampleGroup = new GameObject[_sampleLength];
            
            for (int i = 0; i < _sampleLength; i++)
            {
                var go = Instantiate(_spawnPrefab,transform);
                
                go.name = "Sample" + i;
                go.transform.position = transform.position + GetPositionBySampleAngle() ;
                
                _sampleGroup[i] = go;
            }
        } // End of SpawnSamplePrefabs

        private float _theta = 0f;
        private float ConvertThetaToRadian => _theta * Mathf.Deg2Rad;
        private Vector3 GetPositionBySampleAngle()
        {
            var radian = ConvertThetaToRadian;
            _theta += _sampleAngle;

            var x = Mathf.Sin(radian);
            var z = Mathf.Cos(radian);

            return new Vector3(x, 0f, z) * _radius;
        } // End of GetPositionBySampleAngle

        private void Visualize()
        {
            if (!_isInitialized) return;
            
            for (int i = 0; i < _sampleLength; i++)
            {
                if(_sampleGroup[i] == null) continue;
                
                _sampleGroup[i].transform.localScale = new Vector3(1f, 1f + AudioPeer.GetSampleValue(i) * _maxScale, 1f);
            }
        } // End of Visualize

        #endregion Help Methods
    }
}
