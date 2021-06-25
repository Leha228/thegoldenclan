
using UnityEngine;
using System.Collections;
using Spine.Unity;

namespace Spine.Unity.Examples {
	public class Player : MonoBehaviour {

		#region Inspector
		[Header("Components")]
		public SpineboyBeginnerModel model;
		public SkeletonAnimation skeletonAnimation;

		public AnimationReferenceAsset run, idle, aim, shoot, jump;

		#endregion

		SpineBeginnerBodyState previousViewState;

		void Start () {
			if (skeletonAnimation == null) return;
			model.ShootEvent += PlayShoot;
			model.StartAimEvent += StartPlayingAim;
			model.StopAimEvent += StopPlayingAim;
		}

		void Update () {
			if (skeletonAnimation == null) return;
			if (model == null) return;

			if ((skeletonAnimation.skeleton.ScaleX < 0) != model.facingLeft) {	// Detect changes in model.facingLeft
				Turn(model.facingLeft);
			}

			var currentModelState = model.state;

			if (previousViewState != currentModelState) {
				PlayNewStableAnimation();
			}

			previousViewState = currentModelState;
		}

		void PlayNewStableAnimation () {
			var newModelState = model.state;
			Animation nextAnimation;

			if (newModelState == SpineBeginnerBodyState.Jumping) {
				nextAnimation = jump;
			} else {
				if (newModelState == SpineBeginnerBodyState.Running) {
					nextAnimation = run;
				} else {
					nextAnimation = idle;
				}
			}

			skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, true);
		}


		[ContextMenu("Check Tracks")]
		void CheckTracks () {
			var state = skeletonAnimation.AnimationState;
			Debug.Log(state.GetCurrent(0));
			Debug.Log(state.GetCurrent(1));
		}

		#region Transient Actions
		public void PlayShoot () {
			var shootTrack = skeletonAnimation.AnimationState.SetAnimation(1, shoot, false);
			shootTrack.AttachmentThreshold = 1f;
			shootTrack.MixDuration = 0f;
			var empty1 = skeletonAnimation.state.AddEmptyAnimation(1, 0.5f, 0.1f);
			empty1.AttachmentThreshold = 1f;

			var aimTrack = skeletonAnimation.AnimationState.SetAnimation(2, aim, false);
			aimTrack.AttachmentThreshold = 1f;
			aimTrack.MixDuration = 0f;
			var empty2 = skeletonAnimation.state.AddEmptyAnimation(2, 0.5f, 0.1f);
			empty2.AttachmentThreshold = 1f;
		}

		public void StartPlayingAim () {
			var aimTrack = skeletonAnimation.AnimationState.SetAnimation(2, aim, true);
			aimTrack.AttachmentThreshold = 1f;
			aimTrack.MixDuration = 0f;
		}

		public void StopPlayingAim () {
			var empty2 = skeletonAnimation.state.AddEmptyAnimation(2, 0.5f, 0.1f);
			empty2.AttachmentThreshold = 1f;
		}

		public void Turn (bool facingLeft) {
			skeletonAnimation.Skeleton.ScaleX = facingLeft ? -1f : 1f;
		}
		#endregion

		#region Utility
		public float GetRandomPitch (float maxPitchOffset) {
			return 1f + Random.Range(-maxPitchOffset, maxPitchOffset);
		}
		#endregion
	}

}
