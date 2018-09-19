using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "AnimatorParameterReference", menuName = "Animator/ParameterReference")]
public class AnimatorParameterReference : ScriptableObject
{
    [SerializeField] private string parameterName;
    public string ParameterName { get { return parameterName; } set { parameterName = value; } }

    [SerializeField] private int hash;
    public int Hash { get { if (D_Debug) Debug.LogFormat(this, "Get hash {0} of {1}", hash, this); return hash; } private set { hash = value; } }

    public static bool D_Debug = false;


    public static implicit operator int(AnimatorParameterReference reference)
    {
        if (reference == null) throw new System.NullReferenceException();
        return reference.Hash;
    }


    public override string ToString() { return ParameterName.Split('/').Last(); }


#if UNITY_EDITOR
    void OnValidate()
    {
        Hash = Animator.StringToHash(ParameterName);
    }
#endif
}