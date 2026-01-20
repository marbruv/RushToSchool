using UnityEngine;

[RequireComponent(typeof(Animation))]
public class SlowAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [Range(0.1f, 2f)]
    public float speed = 0.5f; // 0.5 = half speed, 1 = normal, 2 = doble

    public Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();

        if (anim.clip != null)
        {
            // Defoalt speed for the clip
            anim[anim.clip.name].speed = speed;
            anim.Play(anim.clip.name);
        }
        else if (anim.GetClipCount() > 0)
        {
            // If there are several clips, use the first one
            foreach (AnimationState state in anim)
            {
                state.speed = speed;
                anim.Play(state.name);
                break; 
            }
        }
        else
        {
            Debug.LogWarning("No animation clips found on this Animation component.");
        }
    }
}
