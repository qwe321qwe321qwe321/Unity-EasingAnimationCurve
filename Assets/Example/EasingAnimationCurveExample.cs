using System;
using System.Collections.Generic;
using UnityEngine;

namespace EasingCurve.Example {
	public class EasingAnimationCurveExample : MonoBehaviour {
		public static string BezierFitterCoefficentLog;

		public EasingFunctions.Ease easingFuction;
		public AnimationCurve resultBezier;
		public AnimationCurve resultBruteForce;
		public AnimationCurve resultBruteForceBezier;
		[Range(3, 100000)] public int segementsInBruteForce;
		public float fitCurveError = 0.0001f;

		// Move test parameters.
		public bool enableMoveTest;
		public Transform objectForResultBezier;
		public Transform objectForEasingFunction;
		public Transform objectForResultBruteForceBezier;
		[Range(0.01f, 10f)] public float duration = 1f;
		[Range(0f, 1f)] public float padding = 0.1f;
		public Vector3 startLocalPosition = Vector3.zero;
		public Vector3 moveLength = Vector3.up;

		// Generator parameters
		public SimpleCurveComparer curveComparerPrefab;


		private float m_Timer;
		private Vector3 m_StartPosition;

		private void Start() {
			m_Timer = 0;
			m_StartPosition = transform.TransformPoint(startLocalPosition);
			if (objectForResultBezier) {
				objectForResultBezier.position = m_StartPosition + new Vector3(-padding, 0, 0);
            }
			if (objectForEasingFunction) {
				objectForEasingFunction.position = m_StartPosition;
            }
			if (objectForResultBruteForceBezier) {
				objectForResultBruteForceBezier.position = m_StartPosition + new Vector3(padding, 0, 0);
            }
		}

		private void Update() {
			if (enableMoveTest) {
				m_Timer += Time.deltaTime;

				float value = Mathf.PingPong(m_Timer / duration, 1f);
				if (objectForResultBezier) {
					Vector3 startPosition = m_StartPosition + new Vector3(-padding, 0, 0);
					Vector3 endPosition = startPosition + moveLength;
					objectForResultBezier.position = Vector3.LerpUnclamped(startPosition, endPosition, resultBezier.Evaluate(value));
				}
				if (objectForEasingFunction) {
					Vector3 startPosition = m_StartPosition;
					Vector3 endPosition = startPosition + moveLength;
					objectForEasingFunction.position = Vector3.LerpUnclamped(startPosition, endPosition, EasingFunctions.GetEasingFunction(easingFuction)(0, 1, value));
				}
				if (objectForResultBruteForceBezier) {
					Vector3 startPosition = m_StartPosition + new Vector3(padding, 0, 0);
					Vector3 endPosition = startPosition + moveLength;
					objectForResultBruteForceBezier.position = Vector3.LerpUnclamped(startPosition, endPosition, resultBruteForceBezier.Evaluate(value));
				}
			}
		}

        public void ConvertEaseToCurveBeizer() {
			resultBezier = EasingAnimationCurve.EaseToAnimationCurve(easingFuction);
		}

		public void ConvertEaseToCurveBruteForce() {
			resultBruteForce = EasingAnimationCurve.EaseToAnimationCurveBruteForce(easingFuction, segementsInBruteForce);
		}

		public void ConvertEaseToCurveBruteForceApprox() {
			Vector2[] beizerPoints = ConvertEaseToBezierApproxByFitter(easingFuction, segementsInBruteForce, fitCurveError);
			resultBruteForceBezier = EasingAnimationCurve.BezierToAnimationCurve(beizerPoints);
		}

		private Vector2[] ConvertEaseToBezierApproxByFitter(EasingFunctions.Ease ease, int segements, float error) {
			BezierFitterCoefficentLog = "";

			Vector2[] points = new Vector2[segements];
			for (int i = 0; i < segements; i++) {
				float time = (float)i / (segements - 1);
				float value = EasingFunctions.GetEasingFunction(ease)(0, 1, time);
				Vector2 point = new Vector2(time, value);
				points[i] = point;
			}

			List<Vector2> coefficent = CubicBezierFitter.FitCurve(points, error);

			foreach (var item in coefficent) {
				BezierFitterCoefficentLog += string.Format("new Vector2({0:f6}f, {1:f6}f),\n", item.x, item.y);
			}

			return coefficent.ToArray();
		}

		public void GenerateComparersForAllEasingFunctions() {
			if (!curveComparerPrefab) {
				return;
            }

			string[] easeNames = Enum.GetNames(typeof(EasingFunctions.Ease));
			Array easeValues = Enum.GetValues(typeof(EasingFunctions.Ease));
			int count = easeNames.Length;
			Vector2 generateBottomLeft = new Vector3(-5f, -1f);
			int generateColumns = 10;
			Vector2 generatePadding = new Vector2(1, 1.5f);

			GameObject newContainer = new GameObject("Easing Function Comparers");
			newContainer.transform.position = Vector2.zero;

			for (int i = 0; i < count; i++) {
				string easeName = easeNames[i];
				EasingFunctions.Ease easeValue = (EasingFunctions.Ease)easeValues.GetValue(i);
				var newObject = GameObject.Instantiate(curveComparerPrefab.gameObject);
				newObject.name = "Curve Comparer-" + easeName;
				newObject.transform.position = generateBottomLeft + new Vector2(generatePadding.x * (i % generateColumns), generatePadding.y * (i / generateColumns));
				newObject.transform.SetParent(newContainer.transform);
				var comparer = newObject.GetComponent<SimpleCurveComparer>();
				comparer.easingFuction = easeValue;
				comparer.displayText.text = easeName;
				comparer.duration = this.duration;
				comparer.padding = this.padding;
				comparer.startLocalPosition = this.startLocalPosition;
				comparer.moveLength = this.moveLength;
			}
		}
	}
}
