using UnityEngine;

namespace IMBT {
    public class PlaySound : BTNode {
        private readonly AudioClip clip = default;
        private readonly AudioSource source = default;

        public PlaySound(AudioClip clip, GameObject agent) {
            this.clip = clip;
            source = agent.GetComponent<AudioSource>();
            if (source == null) source = agent.gameObject.AddComponent<AudioSource>();
        }

        public override BTTaskStatus Tick(BlackBoard bb) {
            if(!source.isPlaying) source.PlayOneShot(clip);
            return BTTaskStatus.Success;
        }
    }
}