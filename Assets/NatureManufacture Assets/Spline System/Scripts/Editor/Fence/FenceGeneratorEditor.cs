﻿using System.Collections.Generic;

namespace NatureManufacture.RAM.Editor
{
    using UnityEditor;
    using UnityEngine;


    [CustomEditor(typeof(FenceGenerator))]
    public class FenceGeneratorEditor : Editor
    {
        private bool _showPositions;

        private FenceGenerator _fenceGenerator;
        public string[] optionsPost = new string[] { "Random Post", "Post in Sequence" };
        public string[] optionsSpan = new string[] { "Random Span", "Span in Sequence" };

        public string[] toolbarStrings = new[]
        {
            "Basic",
            "Points"
        };

        private SerializedObject _serializedBaseProfile;
        private SerializedProperty _firstSpan;
        private SerializedProperty _lastSpan;
        private SerializedProperty _posts;
        private SerializedProperty _spans;


        private bool _dragged;

        private NmSplineManager NmSplineManager { get; set; }

        private int _postCount;
        private int _spansCount;

        private NmSplineDataFenceScale _fenceScaleData;

        private void OnEnable()
        {
            _fenceGenerator = (FenceGenerator)target;

            if (_fenceGenerator.BaseProfile == null)
            {
                _fenceGenerator.GenerateBaseProfile();
            }

            if (NmSplineManager == null)
            {
                NmSplineManager = new NmSplineManager(_fenceGenerator.NmSpline, "Fence");
                _fenceGenerator.NmSpline.NmSplineChanged.AddListener(OnNmSplineChange);
            }

            SceneView.duringSceneGui -= OnSceneGUIInvoke;
            SceneView.duringSceneGui += OnSceneGUIInvoke;

            _serializedBaseProfile = new SerializedObject(_fenceGenerator.BaseProfile);
            _firstSpan = _serializedBaseProfile.FindProperty("firstSpan");
            _lastSpan = _serializedBaseProfile.FindProperty("lastSpan");
            _posts = _serializedBaseProfile.FindProperty("posts");
            _spans = _serializedBaseProfile.FindProperty("spans");

            _postCount = _posts.arraySize;
            _spansCount = _spans.arraySize;

            _fenceScaleData = _fenceGenerator.NmSpline.GetData<NmSplineDataFenceScale>();
        }

        private void OnNmSplineChange()
        {
            _dragged = true;
            GenerateSplineAndPointList(true);
        }


        private void OnDisable()
        {
            SceneView.duringSceneGui -= this.OnSceneGUIInvoke;

            if (_fenceGenerator != null && _fenceGenerator.gameObject != null && _fenceGenerator.gameObject.activeInHierarchy)
            {
                _fenceGenerator.NmSpline.NmSplineChanged.RemoveListener(OnNmSplineChange);
            }
        }

        [MenuItem("GameObject/3D Object/NatureManufacture/Create Fence Generator")]
        public static void CreateFenceGenerator()
        {
            Selection.activeGameObject = FenceGenerator.CreateFenceGenerator().gameObject;
        }

        public override void OnInspectorGUI()
        {
            PostAndSpansCheck();

            _fenceGenerator = (FenceGenerator)target;
            EditorGUILayout.Space();

            LogoRamUi.UILogo();

            int toolbarNew = GUILayout.SelectionGrid(_fenceGenerator.ToolbarInt, toolbarStrings, 2, GUILayout.Height(40));

            if (toolbarNew == 0)
            {
                EditorGUI.BeginChangeCheck();

                ProfileManage();

                UISplineSettings();


                EditorGUILayout.Space();
                GUILayout.Label("Fence settings:", EditorStyles.boldLabel);


                UISpansAndPosts();

                UIOtherSpline();

                UIBendMesh();


                EditorGUILayout.Space();


                if (GUILayout.Button("Generate Fence"))
                {
                    GenerateSplineAndPointList(false, false);
                }

                _fenceGenerator.AutoGenerate = EditorGUILayout.Toggle("Auto generate objects", _fenceGenerator.AutoGenerate);
                //_fenceGenerator.combine = EditorGUILayout.Toggle("Combine generated objects", _fenceGenerator.combine);


                UIFenceCleaning();


                _fenceGenerator.BaseDebug = EditorGUILayout.Toggle("Debug", _fenceGenerator.BaseDebug);
            }

            if (toolbarNew == 1)
            {
                UISplinePoints();
            }


            _fenceGenerator.ToolbarInt = toolbarNew;
        }

        private void UIFenceCleaning()
        {
            EditorGUILayout.Space();
            if (GUILayout.Button("Clear Fence"))
            {
                _fenceGenerator.DestroyPrefabs();
            }

            if (GUILayout.Button("Clear All GameObject child's"))
            {
                do
                {
                    foreach (Transform item in _fenceGenerator.transform)
                    {
                        DestroyImmediate(item.gameObject);
                    }
                } while (_fenceGenerator.transform.childCount > 0);
            }
        }

        private void PostAndSpansCheck()
        {
            if (_postCount == 0 && _posts.arraySize == 1 && _fenceGenerator.BaseProfile.Posts[0].gameObject == null)
            {
                Debug.Log($"resetting post 0");
                _fenceGenerator.BaseProfile.Posts[0].Reset();

                _serializedBaseProfile.ApplyModifiedProperties();
                _serializedBaseProfile.Update();
            }

            if (_spansCount == 0 && _spans.arraySize == 1 && _fenceGenerator.BaseProfile.Spans[0].gameObject == null)
            {
                Debug.Log($"resetting spans 0");
                _fenceGenerator.BaseProfile.Spans[0].Reset();

                _serializedBaseProfile.ApplyModifiedProperties();
                _serializedBaseProfile.Update();
            }

            _postCount = _posts.arraySize;
            _spansCount = _spans.arraySize;
        }

        private void UISplinePoints()
        {
            EditorGUI.BeginChangeCheck();

            NmSplineManager.PointsUI();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(_fenceGenerator, "Points change");
                GenerateSplineAndPointList();
            }
        }

        private void UIBendMesh()
        {
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            _fenceGenerator.BaseProfile.BendMeshes = EditorGUILayout.Toggle("Bend Meshes", _fenceGenerator.BaseProfile.BendMeshes);
            if (_fenceGenerator.BaseProfile.BendMeshes)
            {
                EditorGUI.indentLevel++;
                _fenceGenerator.BendMeshesPreview = EditorGUILayout.Toggle("Bend Meshes Preview (CPU heavy)", _fenceGenerator.BendMeshesPreview);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();
            var originalFontStyle = EditorStyles.label.fontStyle;
            EditorStyles.label.fontStyle = FontStyle.Bold;
            _fenceGenerator.baseProfile.RandomSeed = EditorGUILayout.Toggle("Random seed", _fenceGenerator.baseProfile.RandomSeed);
            EditorStyles.label.fontStyle = originalFontStyle;

            EditorGUI.indentLevel++;
            if (!_fenceGenerator.baseProfile.RandomSeed)
                _fenceGenerator.baseProfile.Seed = EditorGUILayout.IntField("Seed", _fenceGenerator.baseProfile.Seed);
            else
                EditorGUILayout.LabelField($"Seed: {_fenceGenerator.baseProfile.Seed}");
            EditorGUI.indentLevel--;


            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(_fenceGenerator, "Points change");
                Undo.RegisterCompleteObjectUndo(_fenceGenerator.baseProfile, "Points change");

                _serializedBaseProfile.ApplyModifiedProperties();
                _serializedBaseProfile.Update();

                GenerateSplineAndPointList();
            }
        }

        private void UIOtherSpline()
        {
            _fenceGenerator.OtherSpline = (NmSpline)EditorGUILayout.ObjectField("Other spline", _fenceGenerator.OtherSpline, typeof(NmSpline), true);
            if (_fenceGenerator.OtherSpline != null)
            {
                EditorGUI.indentLevel++;
                _fenceGenerator.SplineLerp = EditorGUILayout.FloatField("Spline offset", _fenceGenerator.SplineLerp);
                EditorGUI.indentLevel--;
            }


            if (_fenceGenerator.OtherSpline != null && GUILayout.Button("Get Points"))
            {
                Undo.RegisterCompleteObjectUndo(_fenceGenerator, "Points change");
                _fenceGenerator.NmSpline.RemoveAllPoints();
                for (int i = 0; i < _fenceGenerator.OtherSpline.MainControlPoints.Count; i++)
                {
                    _fenceGenerator.NmSpline.AddPoint(_fenceGenerator.transform.InverseTransformPoint(Vector3.LerpUnclamped(
                        _fenceGenerator.OtherSpline.transform.TransformPoint(_fenceGenerator.OtherSpline.ControlPointsPositionUp[i]),
                        _fenceGenerator.OtherSpline.transform.TransformPoint(_fenceGenerator.OtherSpline.ControlPointsPositionDown[i]), _fenceGenerator.SplineLerp)));
                }

                GenerateSplineAndPointList();
            }
        }

        private void UISpansAndPosts()
        {
            EditorGUI.BeginChangeCheck();


            EditorGUILayout.PropertyField(_firstSpan, new GUIContent("Beginning span"), true);
            EditorGUILayout.PropertyField(_lastSpan, new GUIContent("Ending span"), true);

            EditorGUILayout.PropertyField(_posts, new GUIContent("Post Objects"), true);


            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Post randomize:");
            _fenceGenerator.BaseProfile.PostRandomType = EditorGUILayout.Popup(_fenceGenerator.BaseProfile.PostRandomType, optionsPost);

            EditorGUILayout.EndHorizontal();


            EditorGUILayout.PropertyField(_spans, new GUIContent("Span Objects"), true);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Span randomize:");
            _fenceGenerator.BaseProfile.SpanRandomType = EditorGUILayout.Popup(_fenceGenerator.BaseProfile.SpanRandomType, optionsSpan);

            EditorGUILayout.EndHorizontal();

            //fenceGenerator.span = (GameObject)EditorGUILayout.ObjectField("Span Object", fenceGenerator.span, typeof(GameObject), false);


            UIBaseFenceSettings();

            EditorGUILayout.Space();


            if (EditorGUI.EndChangeCheck())
            {
                _serializedBaseProfile.ApplyModifiedProperties();
                Undo.RegisterCompleteObjectUndo(_fenceGenerator, "Points change");
                _dragged = true;
                GenerateSplineAndPointList(true);
            }
        }

        private void UIBaseFenceSettings()
        {
            EditorGUILayout.Space();
            _fenceGenerator.BaseProfile.ScaleMesh = EditorGUILayout.Toggle("Scale Mesh", _fenceGenerator.BaseProfile.ScaleMesh);
            _fenceGenerator.BaseProfile.HoldUp = EditorGUILayout.Toggle("Hold up", _fenceGenerator.BaseProfile.HoldUp);
            EditorGUILayout.Space();
            _fenceGenerator.NmSpline.IsLooping = EditorGUILayout.Toggle("Loop", _fenceGenerator.NmSpline.IsLooping);
            _fenceGenerator.NmSpline.IsSnapping = EditorGUILayout.Toggle("Collision Snap", _fenceGenerator.NmSpline.IsSnapping);

            _fenceGenerator.BaseProfile.DistanceMultiplier = EditorGUILayout.FloatField("Span scale distance", _fenceGenerator.BaseProfile.DistanceMultiplier);
            if (_fenceGenerator.BaseProfile.DistanceMultiplier <= 0)
                _fenceGenerator.BaseProfile.DistanceMultiplier = 1;
            _fenceGenerator.BaseProfile.AdditionalDistance = EditorGUILayout.FloatField("Span offset distance", _fenceGenerator.BaseProfile.AdditionalDistance);
        }

        private void UISplineSettings()
        {
            EditorGUILayout.Space();

            GUILayout.Label("Spline settings:", EditorStyles.boldLabel);
            _fenceGenerator.BaseProfile.TriangleDensity = EditorGUILayout.IntSlider("Spline density", (int)(_fenceGenerator.BaseProfile.TriangleDensity), 1, 100);


            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(_fenceGenerator, "Points change");
                _dragged = true;
                GenerateSplineAndPointList(true);
            }
        }

        private void ProfileManage()
        {
            EditorGUILayout.Space();
            _fenceGenerator.currentProfile = (FenceProfile)EditorGUILayout.ObjectField("Fence profile",
                _fenceGenerator.currentProfile, typeof(FenceProfile), false);


            if (GUILayout.Button("Create profile from settings"))
            {
                FenceProfile asset = CreateInstance<FenceProfile>();
                asset.SetProfileData(_fenceGenerator.BaseProfile);


                string path = EditorUtility.SaveFilePanelInProject("Save new spline profile",
                    _fenceGenerator.gameObject.name + ".asset", "asset",
                    "Please enter a file name to save the spline profile to");

                if (!string.IsNullOrEmpty(path))
                {
                    AssetDatabase.CreateAsset(asset, path);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    _fenceGenerator.currentProfile = asset;
                }
            }

            if (_fenceGenerator.currentProfile != null && GUILayout.Button("Save profile from settings"))
            {
                _fenceGenerator.currentProfile.SetProfileData(_fenceGenerator.BaseProfile);
                EditorUtility.SetDirty(_fenceGenerator.currentProfile);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }


            if (_fenceGenerator.currentProfile != null && _fenceGenerator.currentProfile != _fenceGenerator.oldProfile)
            {
                ResetToProfile();
                EditorUtility.SetDirty(_fenceGenerator);
            }

            if (CheckProfileChange())
                EditorGUILayout.HelpBox("Profile data changed.", MessageType.Info);

            if (_fenceGenerator.currentProfile != null && GUILayout.Button("Reset to profile"))
                if (EditorUtility.DisplayDialog("Reset to profile", "Are you sure you want to reset spline to profile?", "Reset", "Do Not Reset"))
                    ResetToProfile();
        }

        private void CheckMeshColoring()
        {
            MeshColoring[] meshColorings = _fenceGenerator.GetComponentsInChildren<MeshColoring>();
            if (meshColorings.Length > 0)
            {
                MeshColoringEditor.GetSceneObjects();
                for (int i = 0; i < meshColorings.Length; i++)
                {
                    MeshColoringEditor.ClearColors(meshColorings[i]);
                    MeshColoringEditor.ColorMesh(meshColorings[i]);
                }
            }
        }

        private void GenerateSplineAndPointList(bool quick = false, bool checkAuto = true)
        {
            _fenceGenerator.GenerateSplineAndPointList(quick, checkAuto);
            if (!quick)
                CheckMeshColoring();
        }

        private void OnSceneGUIInvoke(SceneView sceneView)
        {
            if (_dragged && Event.current.type == EventType.MouseUp)
            {
                _dragged = false;
                GenerateSplineAndPointList();
            }

            _fenceGenerator = (FenceGenerator)target;


            if (_fenceGenerator.NmSpline.Points == null)
                GenerateSplineAndPointList();

            Handles.color = Color.red;

            if (_fenceGenerator.NmSpline.Points != null)
            {
                int end = _fenceGenerator.NmSpline.Points.Count;

                if (!_fenceGenerator.NmSpline.IsLooping)
                    end -= 1;


                var position = _fenceGenerator.transform.position;
                for (int i = 0; i < end; i++)
                {
                    Vector3 one = _fenceGenerator.NmSpline.Points[i].Position + position;
                    Vector3 two = _fenceGenerator.NmSpline.Points[(i + 1) % _fenceGenerator.NmSpline.Points.Count].Position + position;
                    Handles.color = Color.green;
                    Handles.DrawLine(one, two);

                    if (!_fenceGenerator.BaseDebug) continue;

                    Handles.color = Color.blue;
                    Handles.DrawLine(one, one + _fenceGenerator.NmSpline.Points[i].Normal);
                    Handles.color = Color.yellow;
                    Handles.DrawLine(one, one + _fenceGenerator.NmSpline.Points[i].Binormal);
                    Handles.color = Color.green;
                    Handles.DrawLine(one, one + _fenceGenerator.NmSpline.Points[i].Tangent);
                }
            }

            NmSplineManager.SceneGUI(_fenceGenerator);

            if (Tools.current == Tool.Scale && _fenceScaleData != null)
                _fenceScaleData.ShowSceneGUI(_fenceGenerator.NmSpline);
        }

        private bool CheckProfileChange()
        {
            return _fenceGenerator.currentProfile != null && _fenceGenerator.BaseProfile.CheckProfileChange(_fenceGenerator.currentProfile);
        }

        private void ResetToProfile()
        {
            if (_fenceGenerator == null)
                _fenceGenerator = (FenceGenerator)target;

            _fenceGenerator.BaseProfile.SetProfileData(_fenceGenerator.currentProfile);

            _serializedBaseProfile.Update();

            _fenceGenerator.oldProfile = _fenceGenerator.currentProfile;

            GenerateSplineAndPointList();
            CheckMeshColoring();
        }
    }
}