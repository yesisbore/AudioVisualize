using UnityEngine;

namespace Assets.ProjectFiles._01Scripts.BasicScripts
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPeer : MonoBehaviour
    {
        #region Variables

        // Original Audio Samples
        private static float[] _samples = new float[512];
        public static int GetSampleLength => _samples.Length;
        public static float GetSampleValue(int index) => _samples[index];

        // Frequency Band
        private static float[] _freqBend = new float[8];
        public static int GetFrequencyLength => _freqBend.Length;
        public static float GetFrequencyBendValue(int index) => _freqBend[index];
        
        // Buffer
        private static float[] _buffer = new float[8];
        public static float GetBufferValue(int index) => _buffer[index];
        private float _bufferDecrease = 0f;

        private AudioSource _audioSource;

        #endregion Variables

        #region Unity Methods

        private void Start()
        {
            Initialize();
        } // End of Unity - Start

        private void Update()
        {
            GetSpectrumAudioSource();
            SetFrequencyBend();
            SetBuffer();
        } // End of Unity - Update

        #endregion Unity Methods

        #region Help Methods

        private void Initialize()
        {
            GetComponents();
        } // End of Initialize

        private void GetComponents()
        {
            _audioSource = GetComponent<AudioSource>();
        } // End of GetComponents

        private void GetSpectrumAudioSource()
        {
            _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
        } // End of GetSpectrumAudioSource

        private void SetFrequencyBend()
        {
            var count = 0;

            for (int i = 0; i < _freqBend.Length; i++)
            {
                float average = 0f;
                var sampleCount = (int) Mathf.Pow(2, i) * 2;

                if (i == _freqBend.Length - 1)
                {
                    sampleCount += 2;
                }

                for (int j = 0; j < sampleCount; j++)
                {
                    average += _samples[count] * (count * 1);
                    count++;
                }

                average /= count;
                _freqBend[i] = average * 10f;
            }
        } // End of SetFrequencyBend

        private void SetBuffer()
        {
            for (int i = 0; i < _buffer.Length; i++)
            {
                var higherThanBuffer = _freqBend[i] > _buffer[i];
                var lowerThanBuffer = _freqBend[i] < _buffer[i];

                if (higherThanBuffer)
                {
                    _buffer[i] = _freqBend[i];
                    _bufferDecrease = 0.005f;
                }
                else if (lowerThanBuffer)
                {
                    _buffer[i] -= _bufferDecrease;
                    _bufferDecrease *= 1.2f;
                }
            }
        } // End of SetBuffer

        #endregion Help Methods
    }
}