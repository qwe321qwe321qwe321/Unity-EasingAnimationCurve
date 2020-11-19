using UnityEngine;

namespace EasingCurve.Example {
	public class SimpleCurveComparer : MonoBehaviour {
		public EasingFunctions.Ease easingFuction;

		public Transform objectForResultBezier;
		public Transform objectForEasingFunction;
		public TextMesh displayText;

		public AnimationCurve animationCurve;

		[Range(0.01f, 10f)] public float duration = 1f;
		[Range(0f, 1f)] public float padding = 0.1f;
		public Vector3 startLocalPosition = Vector3.zero;
		public Vector3 moveLength = Vector3.up;

		private float m_Timer;
		private Vector3 m_StartPosition;

		private void Start() {
			ConvertEaseToAnimationCurve();
			m_Timer = 0;
			m_StartPosition = transform.TransformPoint(startLocalPosition);
			if (objectForResultBezier) {
				objectForResultBezier.position = m_StartPosition + new Vector3(-padding / 2, 0, 0);
            }
			if (objectForEasingFunction) {
				objectForEasingFunction.position = m_StartPosition + new Vector3(padding / 2, 0, 0);
            }
		}

		private void Update() {
			m_Timer += Time.deltaTime;

			float value = Mathf.PingPong(m_Timer / duration, 1f);
			if (objectForResultBezier) {
				Vector3 startPosition = m_StartPosition + new Vector3(-padding / 2, 0, 0);
				Vector3 endPosition = startPosition + moveLength;
				objectForResultBezier.position = Vector3.LerpUnclamped(startPosition, endPosition, animationCurve.Evaluate(value));
			}
			if (objectForEasingFunction) {
				Vector3 startPosition = m_StartPosition + new Vector3(padding / 2, 0, 0);
				Vector3 endPosition = startPosition + moveLength;
				objectForEasingFunction.position = Vector3.LerpUnclamped(startPosition, endPosition, EasingFunctions.GetEasingFunction(easingFuction)(0, 1, value));
			}
		}

        public void ConvertEaseToAnimationCurve() {
			animationCurve = EasingAnimationCurve.EaseToAnimationCurve(easingFuction);
		}
	}
}
