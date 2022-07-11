using UnityEngine;
using Object = UnityEngine.Object;

public class FireAnimationController : MonoBehaviour
{
    private Object[] fireAnimations;
    
    private Animator _animator;
    
    private void Start()
    {
        fireAnimations = Resources.LoadAll<Object>("FireAnimations");
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = fireAnimations[PlayerPrefs.GetInt("gunID")] as RuntimeAnimatorController;
    }
}
