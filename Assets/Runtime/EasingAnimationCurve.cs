/*
 * Created by PeDev 2020
 * https://github.com/qwe321qwe321qwe321/Unity-EasingAnimationCurve
 * 
 */
using UnityEngine;

namespace EasingCurve {
	public static class EasingAnimationCurve {
		private static bool s_ThrowException = true;

		// The data from the paper: 
		//		Easing Functions in the New Form Based on Bézier Curves 
		//			by Dariusz Sawicki
		//		https://www.researchgate.net/publication/308007569_Easing_Functions_in_the_New_Form_Based_on_Bezier_Curves
		#region EasingFunction to BezierCuvre
		private static Vector2[] EaseInQuad = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.333333f, 0.0f),
			new Vector2(0.666667f, 0.333333f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseOutQuad = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.333333f, 0.666667f),
			new Vector2(0.666667f, 1.0f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInOutQuad = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.166667f, 0.0f),
			new Vector2(0.333333f, 0.166667f),
			new Vector2(0.5f, 0.5f),
			new Vector2(0.666667f, 0.833333f),
			new Vector2(0.833333f, 1.0f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInCubic = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.333333f, 0.0f),
			new Vector2(0.666667f, 0.0f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseOutCubic = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.333333f, 1.0f),
			new Vector2(0.666667f, 1.0f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInOutCubic = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.166667f, 0.0f),
			new Vector2(0.333333f, 0.0f),
			new Vector2(0.5f, 0.5f),
			new Vector2(0.666667f, 1.0f),
			new Vector2(0.833333f, 1.0f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInQuart = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.434789f, 0.006062f),
			new Vector2(0.730901f, -0.07258f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseOutQuart = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.269099f, 1.072581f),
			new Vector2(0.565211f, 0.993938f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInOutQuart = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.217394f, 0.003031f),
			new Vector2(0.365451f, -0.036291f),
			new Vector2(0.5f, 0.5f),
			new Vector2(0.634549f, 1.036290f),
			new Vector2(0.782606f, 0.996969f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInQuint = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.519568f, 0.012531f),
			new Vector2(0.774037f, -0.118927f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseOutQuint = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.225963f, 1.11926f),
			new Vector2(0.481099f, 0.987469f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInOutQuint = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.259784f, 0.006266f),
			new Vector2(0.387018f, -0.059463f),
			new Vector2(0.5f, 0.5f),
			new Vector2(0.612982f, 1.059630f),
			new Vector2(0.740549f, 0.993734f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInSine = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.360780f, -0.000436f),
			new Vector2(0.673486f, 0.486554f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseOutSine = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.330931f, 0.520737f),
			new Vector2(0.641311f, 1.000333f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInOutSine = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.180390f, -0.000217f),
			new Vector2(0.336743f, 0.243277f),
			new Vector2(0.5f, 0.5f),
			new Vector2(0.665465f, 0.760338f),
			new Vector2(0.820656f, 1.000167f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInExpo = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.636963f, 0.0199012f),
			new Vector2(0.844333f, -0.0609379f),
			new Vector2(1.0f, 1.0f),
		};
		private static Vector2[] EaseOutExpo = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.155667f, 1.060938f),
			new Vector2(0.363037f, 0.980099f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInOutExpo = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.318482f, 0.009951f),
			new Vector2(0.422167f, -0.030469f),
			new Vector2(0.5f, 0.5f),
			new Vector2(0.577833f, 1.0304689f),
			new Vector2(0.681518f, 0.9900494f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInCirc = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.55403f, 0.001198f),
			new Vector2(0.998802f, 0.449801f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseOutCirc = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.001198f, 0.553198f),
			new Vector2(0.445976f, 0.998802f),
			new Vector2(1.0f, 1.0f)
		};
		private static Vector2[] EaseInOutCirc = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.277013f, 0.000599f),
			new Vector2(0.499401f, 0.223401f),
			new Vector2(0.5f, 0.5f),
			new Vector2(0.500599f, 0.776599f),
			new Vector2(0.722987f, 0.999401f),
			new Vector2(1.0f, 1.0f)
		};

		// This made by brute force + cubic bezier fitter.
		private static Vector2[] Spring = new Vector2[] {
			new Vector2(0.000000f, 0.000000f),
			new Vector2(0.080285f, 0.287602f),
			new Vector2(0.189354f, 0.568038f),
			new Vector2(0.336583f, 0.828268f),
			new Vector2(0.384005f, 0.912086f),
			new Vector2(0.450141f, 1.048536f),
			new Vector2(0.550666f, 1.079651f),
			new Vector2(0.645743f, 1.109080f),
			new Vector2(0.697447f, 0.993654f),
			new Vector2(0.779498f, 0.974607f),
			new Vector2(0.822437f, 0.964639f),
			new Vector2(0.858526f, 0.992624f),
			new Vector2(0.897999f, 1.003668f),
			new Vector2(0.931730f, 1.013104f),
			new Vector2(0.966372f, 1.006806f),
			new Vector2(1.000000f, 1.000000f)
		};
		private static Vector2[] EaseInBounce = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.030303f, 0.020833f),
			new Vector2(0.060606f, 0.020833f),
			new Vector2(0.0909f, 0.0f),

			new Vector2(0.151515f, 0.083333f),
			new Vector2(0.212121f, 0.083333f),
			new Vector2(0.2727f, 0.0f),

			new Vector2(0.393939f, 0.333333f),
			new Vector2(0.515152f, 0.333333f),
			new Vector2(0.6364f, 0.0f),

			new Vector2(0.757576f, 0.666667f),
			new Vector2(0.878788f, 1.0f),
			new Vector2(1.0f, 1.0f),
		};
		private static Vector2[] EaseOutBounce = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.121212f, 0.0f),
			new Vector2(0.242424f, 0.333333f),
			new Vector2(0.3636f, 1.0f),

			new Vector2(0.484848f, 0.666667f),
			new Vector2(0.606060f, 0.666667f),
			new Vector2(0.7273f, 1.0f),

			new Vector2(0.787879f, 0.916667f),
			new Vector2(0.848485f, 0.916667f),
			new Vector2(0.9091f, 1.0f),

			new Vector2(0.939394f, 0.9791667f),
			new Vector2(0.969697f, 0.9791667f),
			new Vector2(1.0f, 1.0f),
		};
		private static Vector2[] EaseInOutBounce = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.015152f, 0.010417f),
			new Vector2(0.030303f, 0.010417f),
			new Vector2(0.0455f, 0.0f),

			new Vector2(0.075758f, 0.041667f),
			new Vector2(0.106061f, 0.041667f),
			new Vector2(0.1364f, 0.0f),

			new Vector2(0.196970f, 0.166667f),
			new Vector2(0.257576f, 0.166667f),
			new Vector2(0.3182f, 0.0f),

			new Vector2(0.378788f, 0.333333f),
			new Vector2(0.439394f, 0.5f),
			new Vector2(0.5f, 0.5f),

			new Vector2(0.560606f, 0.5f),
			new Vector2(0.621212f, 0.666667f),
			new Vector2(0.6818f, 1.0f),

			new Vector2(0.742424f, 0.833333f),
			new Vector2(0.803030f, 0.833333f),
			new Vector2(0.8636f, 1.0f),

			new Vector2(0.893939f, 0.958333f),
			new Vector2(0.924242f, 0.958333f),
			new Vector2(0.9550f, 1.0f),

			new Vector2(0.969697f, 0.989583f),
			new Vector2(0.984848f, 0.989583f),
			new Vector2(1.0f, 1.0f),
		};
		private static Vector2[] EaseInBack = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.333333f, 0.0f),
			new Vector2(0.666667f, -0.567193f),
			new Vector2(1.0f, 1.0f),
		};
		private static Vector2[] EaseOutBack = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.333333f, 1.567193f),
			new Vector2(0.666667f, 1.0f),
			new Vector2(1.0f, 1.0f),
		};
		private static Vector2[] EaseInOutBack = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.166667f, 0.0f),
			new Vector2(0.333333f, -0.432485f),
			new Vector2(0.5f, 0.5f),

			new Vector2(0.666667f, 1.432485f),
			new Vector2(0.833333f, 1.0f),
			new Vector2(1.0f, 1.0f)
		};

		// Only Elastic series have larger errors with the original easing function. 
		// But it still works like an elastic function.
		// If you feel the error is too large, you can make precise points via BruteForce + CubicBezierFitter like the Spring function did.
		private static Vector2[] EaseInElastic = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.175f, 0.00250747f),
			new Vector2(0.173542f, 0.0f),
			new Vector2(0.175f, 0.0f),

			new Vector2(0.4425f, -0.0184028f),
			new Vector2(0.3525f, 0.05f),
			new Vector2(0.475f, 0.0f),

			new Vector2(0.735f, -0.143095f),
			new Vector2(0.6575f, 0.383333f),
			new Vector2(0.775f, 0.0f),

			new Vector2(0.908125f, -0.586139f),
			new Vector2(0.866875f, -0.666667f),
			new Vector2(1.0f, 1.0f),
		};
		private static Vector2[] EaseOutElastic = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.133125f, 1.666667f),
			new Vector2(0.091875f, 1.586139f),
			new Vector2(0.225f, 1.0f),

			new Vector2(0.3425f, 0.616667f),
			new Vector2(0.265f, 1.143095f),
			new Vector2(0.525f, 1.0f),

			new Vector2(0.6475f, 0.95f),
			new Vector2(0.5575f, 1.0184028f),
			new Vector2(0.8250f, 1.0f),

			new Vector2(0.826458f, 1.0f),
			new Vector2(0.825f, 0.9974925f),
			new Vector2(1.0f, 1.0f),
		};
		private static Vector2[] EaseInOutElastic = new Vector2[] {
			new Vector2(0.0f, 0.0f),
			new Vector2(0.0875f, 0.001254f),
			new Vector2(0.086771f, 0.0f),
			new Vector2(0.0875f, 0.0f),

			new Vector2(0.22125f, -0.009201f),
			new Vector2(0.17625f, 0.025f),
			new Vector2(0.2375f, 0.0f),

			new Vector2(0.3675f, -0.071548f),
			new Vector2(0.32875f, 0.191667f),
			new Vector2(0.3875f, 0.0f),

			new Vector2(0.454063f, -0.293070f),
			new Vector2(0.433438f, -0.333334f),
			new Vector2(0.5f, 0.5f),

			new Vector2(0.5665625f, 1.333334f),
			new Vector2(0.5459375f, 1.293070f),
			new Vector2(0.6125f, 1.0f),

			new Vector2(0.67125f, 0.808334f),
			new Vector2(0.6325f, 1.071548f),
			new Vector2(0.7625f, 1.0f),

			new Vector2(0.82375f, 0.975f),
			new Vector2(0.77875f, 1.009201f),
			new Vector2(0.9125f, 1.0f),

			new Vector2(0.913229f, 1.0f),
			new Vector2(0.9125f, 0.9987463f),
			new Vector2(1.0f, 1.0f),
		};
		#endregion

		/// <summary>
		/// It's special case because Step function cannot be performed by Bezier curve.
		/// </summary>
		private static AnimationCurve StepAnimationCurve {
			get {
				AnimationCurve curve = new AnimationCurve();
				Keyframe[] keyFrames = new Keyframe[]{
					new Keyframe(0, 0) {
						outTangent = 0,
					},
					new Keyframe(0.5f, 0f) {
						inTangent = 0,
						outTangent = 0,
					},
					new Keyframe(0.5f, 1.0f) {
						inTangent = 0,
						outTangent = 0,
					},
					new Keyframe(1.0f, 1.0f) {
						inTangent = 0,
					}
				};
				curve.keys = keyFrames;
				return curve;
			}
		}
		public static AnimationCurve EaseToAnimationCurve(EasingFunctions.Ease ease) {
			switch (ease) {
				case EasingFunctions.Ease.EaseInQuad:
					return BezierToAnimationCurve(EaseInQuad);
				case EasingFunctions.Ease.EaseOutQuad:
					return BezierToAnimationCurve(EaseOutQuad);
				case EasingFunctions.Ease.EaseInOutQuad:
					return BezierToAnimationCurve(EaseInOutQuad);
				case EasingFunctions.Ease.EaseInCubic:
					return BezierToAnimationCurve(EaseInCubic);
				case EasingFunctions.Ease.EaseOutCubic:
					return BezierToAnimationCurve(EaseOutCubic);
				case EasingFunctions.Ease.EaseInOutCubic:
					return BezierToAnimationCurve(EaseInOutCubic);
				case EasingFunctions.Ease.EaseInQuart:
					return BezierToAnimationCurve(EaseInQuart);
				case EasingFunctions.Ease.EaseOutQuart:
					return BezierToAnimationCurve(EaseOutQuart);
				case EasingFunctions.Ease.EaseInOutQuart:
					return BezierToAnimationCurve(EaseInOutQuart);
				case EasingFunctions.Ease.EaseInQuint:
					return BezierToAnimationCurve(EaseInQuint);
				case EasingFunctions.Ease.EaseOutQuint:
					return BezierToAnimationCurve(EaseOutQuint);
				case EasingFunctions.Ease.EaseInOutQuint:
					return BezierToAnimationCurve(EaseInOutQuint);
				case EasingFunctions.Ease.EaseInSine:
					return BezierToAnimationCurve(EaseInSine);
				case EasingFunctions.Ease.EaseOutSine:
					return BezierToAnimationCurve(EaseOutSine);
				case EasingFunctions.Ease.EaseInOutSine:
					return BezierToAnimationCurve(EaseInOutSine);
				case EasingFunctions.Ease.EaseInExpo:
					return BezierToAnimationCurve(EaseInExpo);
				case EasingFunctions.Ease.EaseOutExpo:
					return BezierToAnimationCurve(EaseOutExpo);
				case EasingFunctions.Ease.EaseInOutExpo:
					return BezierToAnimationCurve(EaseInOutExpo);
				case EasingFunctions.Ease.EaseInCirc:
					return BezierToAnimationCurve(EaseInCirc);
				case EasingFunctions.Ease.EaseOutCirc:
					return BezierToAnimationCurve(EaseOutCirc);
				case EasingFunctions.Ease.EaseInOutCirc:
					return BezierToAnimationCurve(EaseInOutCirc);
				case EasingFunctions.Ease.Linear:
					return AnimationCurve.Linear(0, 0, 1, 1);
				case EasingFunctions.Ease.Spring:
					return BezierToAnimationCurve(Spring);
				case EasingFunctions.Ease.EaseInBounce:
					return BezierToAnimationCurve(EaseInBounce);
				case EasingFunctions.Ease.EaseOutBounce:
					return BezierToAnimationCurve(EaseOutBounce);
				case EasingFunctions.Ease.EaseInOutBounce:
					return BezierToAnimationCurve(EaseInOutBounce);
				case EasingFunctions.Ease.EaseInBack:
					return BezierToAnimationCurve(EaseInBack);
				case EasingFunctions.Ease.EaseOutBack:
					return BezierToAnimationCurve(EaseOutBack);
				case EasingFunctions.Ease.EaseInOutBack:
					return BezierToAnimationCurve(EaseInOutBack);
				case EasingFunctions.Ease.EaseInElastic:
					return BezierToAnimationCurve(EaseInElastic);
				case EasingFunctions.Ease.EaseOutElastic:
					return BezierToAnimationCurve(EaseOutElastic);
				case EasingFunctions.Ease.EaseInOutElastic:
					return BezierToAnimationCurve(EaseInOutElastic);
				case EasingFunctions.Ease.Step:
					return StepAnimationCurve;
			}

			throw new System.ArgumentException("Undefined Easing Function: " + ease.ToString());
		}

		public static AnimationCurve EaseToAnimationCurveBruteForce(EasingFunctions.Ease ease, int segements = 20) {
			AnimationCurve animationCurve = new AnimationCurve();
			Keyframe[] keyFrames = new Keyframe[segements];
			for (int i = 0; i < keyFrames.Length; i++) {
				float time = (float)i / (keyFrames.Length - 1);
				float value = EasingFunctions.GetEasingFunction(ease).Invoke(0, 1, time);
				keyFrames[i] = new Keyframe(time, value);
			}
			animationCurve.keys = keyFrames;
			for (int i = 1; i < keyFrames.Length - 1; i++) {
				animationCurve.SmoothTangents(i, 0);
			}
			return animationCurve;
		}


		/// <summary>
		/// To convert bezier curve to animation curve.
		/// </summary>
		/// <param name="controlPointStrips">Points strips are the series of the connected control points. 
		/// For example, there are two bezier curve p11, p12, p13, and p14 in the first, p21, p22, p23, and p24 in the second.
		/// And the p14 equals p21 so that they are connected, then the points strips is [p11, p12, p13, p14(p21), p22, p23, p24].</param>
		/// <returns></returns>
		public static AnimationCurve BezierToAnimationCurve(Vector2[] controlPointStrips) {
			AnimationCurve animationCurve = new AnimationCurve();
			BezierToAnimationCurve(animationCurve, controlPointStrips);
			return animationCurve;
		}

		private static void BezierToAnimationCurve(AnimationCurve outCurve, Vector2[] controlPointStrips) {
			if (s_ThrowException) {
				if (controlPointStrips.Length < 4) {
					throw new System.ArgumentException("The number of control point strips should more than 4!");
				}
				if ((controlPointStrips.Length - 4) % 3 != 0) {
					throw new System.ArgumentException("The number of control point strips N should be (N-4)%3==0");
				}
			}
			int bezierCount = 1 + (controlPointStrips.Length - 4) / 3;
			Keyframe[] keyframes = new Keyframe[bezierCount + 1];
			// Init the first keyframe with the first cp.
			keyframes[0] = new Keyframe(controlPointStrips[0].x, controlPointStrips[0].y) {
				// Weight affects the position of the control point.
				// By default, WeightedMode is None which makes every points has 1/3 weight.(because 4 cp makes 2 point with 2 inner points)
				// https://docs.unity3d.com/ScriptReference/Keyframe-inWeight.html
				weightedMode = WeightedMode.Both
			};
			for (int i = 0; i < bezierCount; i++) {
				int cp = i * 3;
				// Set the outTangent of cp1 which means cp2.
				keyframes[i].outTangent = Tangent(controlPointStrips[cp], controlPointStrips[cp + 1]);
				// Makes the weight as the x offset of (cp2 - cp1).
				float bezierLength = controlPointStrips[cp + 3].x - controlPointStrips[cp].x;	
				keyframes[i].outWeight = Weight(controlPointStrips[cp], controlPointStrips[cp + 1], bezierLength);
				// Create cp4 as keyframe and set its inTangent which means cp3.
				keyframes[i + 1] = new Keyframe(controlPointStrips[cp + 3].x, controlPointStrips[cp + 3].y) {
					inTangent = Tangent(controlPointStrips[cp + 2], controlPointStrips[cp + 3]),
					inWeight = Weight(controlPointStrips[cp + 2], controlPointStrips[cp + 3], bezierLength),
					weightedMode = WeightedMode.Both
				};
			}

			if (outCurve == null) {
				outCurve = new AnimationCurve();
			}
			outCurve.keys = keyframes;
		}

		private static float Tangent(in Vector2 from, in Vector2 to) {
			Vector2 vec = to - from;
			return vec.y / vec.x;
		}

		// Weight has to be normalized by the distance of a single bezier.
		private static float Weight(in Vector2 from, in Vector2 to, float length) {
			return (to.x - from.x) / length;
		}

	}
}