using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EasingCurve.Example {
    [CustomEditor(typeof(EasingAnimationCurveExample))]
    public class EasingAnimationCurveExampleEditor : Editor {
        EasingAnimationCurveExample m_MonoTarget;

        SerializedProperty m_SerialziedEasingFuction;
        SerializedProperty m_SerialziedResultBezier;
        SerializedProperty m_SerialziedResultBruteForce;
        SerializedProperty m_SerialziedResultBruteForceBezier;
        SerializedProperty m_SerialziedSegementsInBruteForce;
        SerializedProperty m_SerialziedFitCurveError;
        // Move test
        SerializedProperty m_SerialziedEnableMoveTest;
        SerializedProperty m_SerialziedObjectForResultBezier;
        SerializedProperty m_SerialziedObjectForEasingFunction;
        SerializedProperty m_SerialziedObjectForResultBruteForceBezier;
        SerializedProperty m_SerialziedDuration;
        SerializedProperty m_SerialziedPadding;
        SerializedProperty m_SerialziedStartLocalPosition;
        SerializedProperty m_SerialziedMoveLength;
        // Comparer Generator
        SerializedProperty m_SerializedCurveComparerPrefab;
        private void OnEnable() {
            m_MonoTarget = target as EasingAnimationCurveExample;

            m_SerialziedEasingFuction = serializedObject.FindProperty("easingFuction");
            m_SerialziedResultBezier = serializedObject.FindProperty("resultBezier");
            m_SerialziedResultBruteForce = serializedObject.FindProperty("resultBruteForce");
            m_SerialziedResultBruteForceBezier = serializedObject.FindProperty("resultBruteForceBezier");
            m_SerialziedSegementsInBruteForce = serializedObject.FindProperty("segementsInBruteForce");
            m_SerialziedFitCurveError = serializedObject.FindProperty("fitCurveError");

            m_SerialziedEnableMoveTest = serializedObject.FindProperty("enableMoveTest");
            m_SerialziedObjectForResultBezier = serializedObject.FindProperty("objectForResultBezier");
            m_SerialziedObjectForEasingFunction = serializedObject.FindProperty("objectForEasingFunction");
            m_SerialziedObjectForResultBruteForceBezier = serializedObject.FindProperty("objectForResultBruteForceBezier");
            m_SerialziedDuration = serializedObject.FindProperty("duration");
            m_SerialziedPadding = serializedObject.FindProperty("padding");
            m_SerialziedStartLocalPosition = serializedObject.FindProperty("startLocalPosition");
            m_SerialziedMoveLength = serializedObject.FindProperty("moveLength");
            m_SerializedCurveComparerPrefab = serializedObject.FindProperty("curveComparerPrefab");

        }
        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();

            // Use CurveField to force to refresh the preview curve in inspector.
            // SerializedProperty of AnimationCurve won't refresh preview when it is changed by method(such as the button invoke).
            m_SerialziedResultBezier.animationCurveValue = EditorGUILayout.CurveField("Result Bezier", m_SerialziedResultBezier.animationCurveValue);
            m_SerialziedResultBruteForce.animationCurveValue = EditorGUILayout.CurveField("Result Brute Force", m_SerialziedResultBruteForce.animationCurveValue);
            m_SerialziedResultBruteForceBezier.animationCurveValue = EditorGUILayout.CurveField("Result Brute Force + Bezier", m_SerialziedResultBruteForceBezier.animationCurveValue);

            EditorGUILayout.Space(10);
            EditorGUILayout.PropertyField(m_SerialziedEasingFuction);
            if (GUILayout.Button("Convert Ease To Curve (Bezier Approximation)")) {
                m_MonoTarget.ConvertEaseToCurveBeizer();
            }

            if (GUILayout.Button("Convert Ease To Curve (Brute Force)")) {
                m_MonoTarget.ConvertEaseToCurveBruteForce();
            }

            EditorGUILayout.PropertyField(m_SerialziedSegementsInBruteForce);
            if (GUILayout.Button("Convert Ease To Curve (Brute Force + Bezier Fit)")) {
                m_MonoTarget.ConvertEaseToCurveBruteForceApprox();
            }
            EditorGUILayout.PropertyField(m_SerialziedFitCurveError);
            EditorGUILayout.LabelField("Bezier coefficents");
            EditorGUILayout.TextArea(EasingAnimationCurveExample.BezierFitterCoefficentLog);

            EditorGUILayout.Space(20);
            EditorGUILayout.PropertyField(m_SerialziedEnableMoveTest);
            if (m_SerialziedEnableMoveTest.boolValue) {
                EditorGUILayout.PropertyField(m_SerialziedObjectForResultBezier);
                EditorGUILayout.PropertyField(m_SerialziedObjectForEasingFunction);
                EditorGUILayout.PropertyField(m_SerialziedObjectForResultBruteForceBezier);
                EditorGUILayout.PropertyField(m_SerialziedDuration);
                EditorGUILayout.PropertyField(m_SerialziedPadding);
                EditorGUILayout.PropertyField(m_SerialziedStartLocalPosition);
                EditorGUILayout.PropertyField(m_SerialziedMoveLength);
            }

            EditorGUILayout.Space(20);
            if (GUILayout.Button("Generate curve comparers for all easing function")) {
                m_MonoTarget.GenerateComparersForAllEasingFunctions();
            }
            EditorGUILayout.PropertyField(m_SerializedCurveComparerPrefab);

            if (EditorGUI.EndChangeCheck()) {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private string ExportEasingFunctionPoints(int segements, EasingFunctions.Ease ease, float error, out List<Vector2> coefficent) {
            string content = "";

            Vector2[] points = new Vector2[segements];
            for (int i = 0; i < segements; i++) {
                float time = (float)i / (segements - 1);
                float value = EasingFunctions.GetEasingFunction(ease)(0, 1, time);
                Vector2 point = new Vector2(time, value);
                points[i] = point;
            }

            coefficent = CubicBezierFitter.FitCurve(points, error);
            foreach (var item in coefficent) {
                content += string.Format("new Vector2({0:f6}f, {1:f6}f),\n", item.x, item.y);
            }

            return content;
        }

    }
}
