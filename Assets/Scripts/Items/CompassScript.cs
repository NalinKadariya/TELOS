using UnityEngine;
using UnityEngine.UI;

public class CompassScript : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private RectTransform _compassNeedle;

    void Update()
    {
        float _heading = _player.eulerAngles.y;
        _compassNeedle.localRotation = Quaternion.Euler(0, 0, -_heading);
    }
}
