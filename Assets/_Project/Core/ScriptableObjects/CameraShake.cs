using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraShake : MonoBehaviour
{
    [System.Serializable]
    private class NoiseSettings
    {
        public Vector2 XAxisNoiseStartPosition = new Vector2();
        public Vector2 XAxisNoiseScrollSpeed = new Vector2(20.0f, 20.0f);
        [Space]
        public Vector2 YAxisNoiseStartPosition = new Vector2(10.0f, 10.0f);
        public Vector2 YAxisNoiseScrollSpeed = new Vector2(20.0f, 20.0f);
        [Space]
        public Vector2 ZAxisNoiseStartPosition = new Vector2(20.0f, 20.0f);
        public Vector2 ZAxisNoiseScrollSpeed = new Vector2(20.0f, 20.0f);
    }

    [Tooltip("These settings are usually fine as default, but they're here if you'd like manual control over the noise")]
    [SerializeField] private NoiseSettings _noiseSettings;

    [Space]
    [Tooltip("Maximum distance the camera will translate on each axis at maximum trauma")]
    [SerializeField] private Vector3 _maximumTranslationalOffset = new Vector3(0.3f, 0.3f, 0.0f);

    [Tooltip("Maximum distance the camera will rotate on each axis at maximum trauma")]
    [SerializeField] private Vector3 _maximumRotationalOffset = new Vector3(10.0f, 10.0f, 10.0f);

    [Space]
    [Tooltip("How fast should the camera shake")]
    [SerializeField, Range(0.01f, 3)] private float _frequency = 1;

    [Tooltip("How hard should the camera shake")]
    [SerializeField, Range(0.01f, 3)] private float _amplitude = 1;

    [Tooltip("Higher values will require the trauma to be higher before any intense shake is performed")]
    [SerializeField] private float _traumaExponent = 2.0f;

    [Tooltip("Should trauma decay at all")]
    [SerializeField] private bool _traumaShouldDecay = true;

    [Tooltip("Multiplier for trauma decay speed")]
    [SerializeField] private float _traumaDecaySpeedMultiplier = 1.0f;

    [Tooltip("How quickly should trauma decay from a certain value")]
    [SerializeField] private AnimationCurve _traumaDecayCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private float _trauma = 0;
    private Vector2 _noiseScrollX;
    private Vector2 _noiseScrollY;
    private Vector2 _noiseScrollZ;

    public float Trauma { get => _trauma; set => _trauma = value; }
    public bool TraumaShouldDecay { get => _traumaShouldDecay; set => _traumaShouldDecay = value; }
    public float Amplitude { get => _amplitude; set => _amplitude = value; }
    public float Frequency { get => _frequency; set => _frequency = value; }

    private void Start()
    {
        _noiseScrollX = _noiseSettings.XAxisNoiseStartPosition;
        _noiseScrollY = _noiseSettings.YAxisNoiseStartPosition;
        _noiseScrollZ = _noiseSettings.ZAxisNoiseStartPosition;
    }

    /// <summary>
    /// Traumatize the camera. Trauma is clamped between 0 and 1. For a light shake, 0.3. For a heavy shake, 0.6.
    /// </summary>
    /// <param name="trauma"></param>
    public void AddTrauma(float trauma)
    {
        Trauma = Mathf.Clamp01(Trauma + trauma);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            AddTrauma(0.3f);
        else if (Input.GetKeyDown(KeyCode.R))
            AddTrauma(0.6f);

        // No need to process if there is no trauma
        if (Trauma <= 0)
            return;

        // Apply translational shake
        if (_maximumTranslationalOffset != Vector3.zero)
        {
            // Sample noise
            Vector3 noiseSample = SampleNoise();
            
            // Calculate positions
            float translationX = _maximumTranslationalOffset.x * Amplitude * Mathf.Pow(Trauma, _traumaExponent) * noiseSample.x;
            float translationY = _maximumTranslationalOffset.y * Amplitude * Mathf.Pow(Trauma, _traumaExponent) * noiseSample.y;
            float translationZ = _maximumTranslationalOffset.z * Amplitude * Mathf.Pow(Trauma, _traumaExponent) * noiseSample.z; 

            // Apply translation
            transform.localPosition = new Vector3(translationX, translationY, translationZ); ;
        }

        // Apply rotational shake
        if (_maximumRotationalOffset != Vector3.zero)
        {
            // Sample noise
            Vector3 noiseSample = SampleNoise();

            // Calculate positions
            Quaternion rotationX = Quaternion.AngleAxis(_maximumRotationalOffset.x * Amplitude * Mathf.Pow(Trauma, _traumaExponent) * noiseSample.x, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(_maximumRotationalOffset.y * Amplitude * Mathf.Pow(Trauma, _traumaExponent) * noiseSample.y, Vector3.up);
            Quaternion rotationZ = Quaternion.AngleAxis(_maximumRotationalOffset.z * Amplitude * Mathf.Pow(Trauma, _traumaExponent) * noiseSample.z, Vector3.forward);
            
            transform.localRotation = rotationX * rotationY * rotationZ;
        }

        // Trauma decay
        if (TraumaShouldDecay)
            Trauma = Mathf.Max(0, Trauma - _traumaDecayCurve.Evaluate(Trauma) * _traumaDecaySpeedMultiplier * Time.deltaTime);
    }

    private Vector3 SampleNoise()
    {
        // Sample noise
        float noiseSampleX = Mathf.PerlinNoise(_noiseScrollX.x, _noiseScrollX.y) * 2 - 1;
        float noiseSampleY = Mathf.PerlinNoise(_noiseScrollY.x, _noiseScrollY.y) * 2 - 1;
        float noiseSampleZ = Mathf.PerlinNoise(_noiseScrollZ.x, _noiseScrollZ.y) * 2 - 1;

        // Scroll noise
        _noiseScrollX += _noiseSettings.XAxisNoiseScrollSpeed * Frequency * Mathf.Pow(Trauma, _traumaExponent) * Time.deltaTime;
        _noiseScrollY += _noiseSettings.YAxisNoiseScrollSpeed * Frequency * Mathf.Pow(Trauma, _traumaExponent) * Time.deltaTime;
        _noiseScrollZ += _noiseSettings.ZAxisNoiseScrollSpeed * Frequency * Mathf.Pow(Trauma, _traumaExponent) * Time.deltaTime;

        return new Vector3(noiseSampleX, noiseSampleY, noiseSampleZ);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CameraShake))]
public class CameraShakeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        string text = "This camera shake script uses a trauma system to give both rotational and translational shake to an object based on Perlin Noise." +
            "\n\n" +
            "Place directly on the object you want shaken. Object will be reset to its local origin when no trauma is applied. " +
            "Child the object to an empty game object and affect that object with any other scripts instead.";
        EditorGUILayout.HelpBox(text, MessageType.Info);

        base.OnInspectorGUI();
    }
}
#endif
