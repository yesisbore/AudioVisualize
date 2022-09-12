using Unity.Mathematics;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Assets.ProjectFiles._01Scripts.BasicScripts
{
    public enum AudioVisualType
    {
        Detail,
        Simple,
        All
    }
    [RequireComponent(typeof(AudioPeer))]
    public class AudioVisualManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private AudioVisualType audioVisualType = AudioVisualType.All;
        [SerializeField] private GameObject _spawnPrefab;

        private AudioVisualDetail _audioVisualDetail;
        private AudioVisualSimple _audioVisualSimple;
        
        #endregion Variables

        #region Unity Methods

        private void Start()
        {
            Initialize();
        } // End of Unity - Start

        // private void Update()
        // {
        // } // End of Unity - Update

        #endregion Unity Methods

        #region Help Methods

        private void Initialize()
        {
            GetComponents();
            SelectVisualType();
        } // End of Initialize

        private void GetComponents()
        {

        } // End of GetComponents

        private void SelectVisualType()
        {
            switch (audioVisualType)
            {
                case AudioVisualType.Detail :
                    _audioVisualDetail = gameObject.AddComponent<AudioVisualDetail>();
                    _audioVisualDetail.Initialize(_spawnPrefab);
                    return;
                case  AudioVisualType.Simple :
                    _audioVisualSimple = gameObject.AddComponent<AudioVisualSimple>();
                    _audioVisualSimple.Initialize(_spawnPrefab);
                    return;
                case  AudioVisualType.All : 
                    _audioVisualDetail = gameObject.AddComponent<AudioVisualDetail>();
                    _audioVisualDetail.Initialize(_spawnPrefab);
                    _audioVisualSimple = gameObject.AddComponent<AudioVisualSimple>();
                    _audioVisualSimple.Initialize(_spawnPrefab);
                    return;
            }
        }

        
        #endregion Help Methods
    }
}