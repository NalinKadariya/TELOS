using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FootIKController : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float footRayDistance = 1.5f;
    [SerializeField] private float footOffset = 0.1f;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_animator == null) return;

        HandleFootIK(AvatarIKGoal.LeftFoot);
        HandleFootIK(AvatarIKGoal.RightFoot);
    }

    private void HandleFootIK(AvatarIKGoal foot)
    {
        Vector3 footPosition = _animator.GetIKPosition(foot);
        Ray ray = new Ray(footPosition + Vector3.up * 0.5f, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, footRayDistance, groundMask))
        {
            Vector3 targetPos = hit.point + Vector3.up * footOffset;
            Quaternion targetRot = Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation;

            _animator.SetIKPositionWeight(foot, 1f);
            _animator.SetIKRotationWeight(foot, 1f);
            _animator.SetIKPosition(foot, targetPos);
            _animator.SetIKRotation(foot, targetRot);
        }
        else
        {
            _animator.SetIKPositionWeight(foot, 0f);
            _animator.SetIKRotationWeight(foot, 0f);
        }
    }
}
