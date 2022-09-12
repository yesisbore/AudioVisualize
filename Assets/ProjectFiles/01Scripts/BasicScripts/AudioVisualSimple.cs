using UnityEngine;

namespace Assets.ProjectFiles._01Scripts.BasicScripts
{
    public class AudioVisualSimple : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private float _maxScale = 10f;
        
        private GameObject _spawnPrefab;
        private GameObject[] _sampleGroup ;
        private int _sampleLength;
        private float _prefabDistance = 10f;
        
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
            _sampleLength = AudioPeer.GetFrequencyLength;
        } // End of GetComponents

        private void SpawnSamplePrefabs()
        {
            _sampleGroup = new GameObject[_sampleLength];

            for (int i = 0; i < _sampleLength; i++)
            {
                var go = Instantiate(_spawnPrefab, transform);

                go.name = "Sample" + i;
                go.transform.position = transform.position + new Vector3(_prefabDistance * i,0f,0f);

                _sampleGroup[i] = go;
            }
        } // End of SpawnSamplePrefabs


        private void Visualize()
        {
            if (!_isInitialized) return;
            
            for (int i = 0; i < _sampleLength; i++)
            {
                if (_sampleGroup[i] == null) continue;

                _sampleGroup[i].transform.localScale =
                    new Vector3(1f, 1f + AudioPeer.GetBufferValue(i) * _maxScale , 1f);
            }
        } // End of Visualize

        #endregion Help Methods
    }
}