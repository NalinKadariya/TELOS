using UnityEngine;
using CharacterControl.Manager;

namespace CharacterControl.SimpleFootsteps
{
    public class SimpleFootsteps : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Terrain _terrain;

        [Header("Footstep Sounds")]
        [SerializeField] private AudioClip[] _grassClips;
        [SerializeField] private AudioClip[] _dirtClips;
        [SerializeField] private AudioClip[] _woodClips;
        [SerializeField] private AudioClip[] _rockClips;
        [SerializeField] private AudioClip[] _defaultClips;

        [Header("Settings")]
        [SerializeField] private float _raycastDistance = 2f;
        [SerializeField] private float _pitchVariation = 0.05f;

        public void PlayFootstepSound()
        {
            AudioClip _clip = GetFootstepClip();
            if (_clip == null) return;

            _audioSource.pitch = Random.Range(1f - _pitchVariation, 1f + _pitchVariation);
            _audioSource.PlayOneShot(_clip);
            _audioSource.pitch = 1f;
        }

        private AudioClip GetFootstepClip()
        {
            if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit _hit, _raycastDistance))
            {
                Terrain _hitTerrain = _hit.collider.GetComponent<Terrain>();
                if (_hitTerrain != null)
                {
                    if (_terrain == null)
                    {
                        GameObject terrainObj = GameObject.FindGameObjectWithTag("Terrain");
                        if (terrainObj != null)
                        {
                            _terrain = terrainObj.GetComponent<Terrain>();
                        }
                    }

                    if (_terrain != null)
                    {
                        int _textureIndex = GetMainTexture(_hit.point);

                        if (_textureIndex >= 0 && _textureIndex < _terrain.terrainData.terrainLayers.Length)
                        {
                            string _layerName = _terrain.terrainData.terrainLayers[_textureIndex].diffuseTexture.name.ToLower();

                            if (_layerName.Contains("grass"))
                                return GetRandomClip(_grassClips);

                            if (_layerName.Contains("dirt"))
                                return GetRandomClip(_dirtClips);
                        }
                    }

                    return GetRandomClip(_defaultClips);
                }

                string _tag = _hit.collider.tag;

                switch (_tag)
                {
                    case "Wood": return GetRandomClip(_woodClips);
                    case "Rock": return GetRandomClip(_rockClips);
                    default: return GetRandomClip(_defaultClips);
                }
            }

            return null;
        }


        private int GetMainTexture(Vector3 _worldPos)
        {
            TerrainData _terrainData = _terrain.terrainData;
            Vector3 _terrainLocalPos = _worldPos - _terrain.transform.position;

            int _mapX = Mathf.FloorToInt((_terrainLocalPos.x / _terrainData.size.x) * _terrainData.alphamapWidth);
            int _mapZ = Mathf.FloorToInt((_terrainLocalPos.z / _terrainData.size.z) * _terrainData.alphamapHeight);

            float[,,] _splatmapData = _terrainData.GetAlphamaps(_mapX, _mapZ, 1, 1);

            int _maxIndex = 0;
            float _maxMix = 0f;

            for (int i = 0; i < _splatmapData.GetLength(2); i++)
            {
                if (_splatmapData[0, 0, i] > _maxMix)
                {
                    _maxIndex = i;
                    _maxMix = _splatmapData[0, 0, i];
                }
            }

            return _maxIndex;
        }

        private AudioClip GetRandomClip(AudioClip[] _clips)
        {
            if (_clips == null || _clips.Length == 0) return null;
            return _clips[Random.Range(0, _clips.Length)];
        }
    }
}
