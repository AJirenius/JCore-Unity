using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JCore.UI
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class AView : MonoBehaviour
    {
        protected virtual void OnParamsSet() { }
        protected virtual void OnAnimInStart() { }
        protected virtual void OnAnimInEnd() { }
        protected virtual void OnAnimOutStart() { }
        protected virtual void OnAnimOutEnd() { }

        private Animator animator;
        private CanvasGroup canvasGroup;
        public ViewState currentState;

        static private int h_AnimIn = Animator.StringToHash("AnimIn");
        static private int h_AnimOut = Animator.StringToHash("AnimOut");

        public abstract void SetParams(AViewParams param);

        public virtual void Initialize()
        {
            animator = GetComponent<Animator>();
            canvasGroup = GetComponent<CanvasGroup>();
            UpdateAnimClipTimes();
            SetState(ViewState.Inactive);
        }

        public void Open()
        {
            SetState(ViewState.AnimatingIn);
        }

        public void Close()
        {
            SetState(ViewState.AnimatingOut);
        }

        private void SetState(ViewState state)
        {
            if ((state == ViewState.AnimatingIn && currentState == ViewState.Active) ||
                (state == ViewState.AnimatingOut && currentState == ViewState.Inactive) ||
                currentState == state) return;
            currentState = state;
            Debug.Log(gameObject.name + " : Set state - " + currentState);
            switch (state)
            {
                case ViewState.Inactive:
                    gameObject.SetActive(false);
                    break;

                case ViewState.AnimatingIn:
                    gameObject.SetActive(true);
                    animator.enabled = true;
                    TriggerAnim(h_AnimIn);
                    canvasGroup.blocksRaycasts = false;
                    OnAnimInStart();
                    break;

                case ViewState.Active:
                    animator.enabled = false;
                    canvasGroup.blocksRaycasts = true;
                    break;

                case ViewState.AnimatingOut:
                    animator.enabled = true;
                    canvasGroup.blocksRaycasts = false;
                    TriggerAnim(h_AnimOut);
                    OnAnimOutStart();
                    break;
            }
        }

        private void TriggerAnim(int trigger)
        {
            animator.SetTrigger(trigger);
        }

        private void AnimInDone()
        {
            SetState(ViewState.Active);
            OnAnimInEnd();
        }

        private void AnimOutDone()
        {
            SetState(ViewState.Inactive);
            OnAnimOutEnd();
        }

        public void UpdateAnimClipTimes()
        {
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.length > 0f && clip.events.Length == 0)
                {
                    AnimationEvent evt = new AnimationEvent();
                    if (clip.name.Contains("AnimIn"))
                    {
                        evt.functionName = "AnimInDone";
                    }
                    else if (clip.name.Contains("AnimOut"))
                    {
                        evt.functionName = "AnimOutDone";
                    }
                    else
                    {
                        Debug.LogError("Clip name must contain phrase AnimIn or AnimOut if not single frame");
                        return;
                    }
                    evt.time = clip.length;
                    clip.AddEvent(evt);
                }
            }
        }
    }

    public enum ViewState { Init, Inactive, AnimatingIn, Active, AnimatingOut }
}