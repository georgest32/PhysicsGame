using NWH.NUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;


namespace DWP2
{
    [CustomEditor(typeof(WaterObject))]
    [CanEditMultipleObjects]
    [ExecuteAlways]
    public class WaterObjectEditor : DWP_NUIEditor
    {
        private Texture2D _originalMeshPreviewTexture;
        private Texture2D _simMeshPreviewTexture;
        private Texture2D _greyTexture;
        private GUIStyle _centeredStyle;
        private bool _editorHasWarnings;

        private WaterObject _waterObject;
        private float _previewHeight;

        private void OnEnable()
        {
            _greyTexture = new Texture2D(10, 10);
            EditorUtils.FillInTexture(ref _greyTexture, new Color32(66, 66, 66, 255));
        }

        public override bool OnInspectorNUI()
        {
            if (!base.OnInspectorNUI())
            {
                return false;
            }
            
            // Get water object and make sure it is initialized
            _waterObject = (WaterObject) target;
            if(!_waterObject.Initialized)
            {
                _waterObject.Initialize();
            }

            // Draw logo texture
            Rect logoRect = drawer.positionRect;
            logoRect.height = 60f;
            drawer.DrawEditorTexture(logoRect, "Logos/WaterObjectLogo");
            drawer.AdvancePosition(logoRect.height);

            // Simulation mesh
            drawer.BeginSubsection("Simulation Mesh Settings");
            if (drawer.Field("simplifyMesh").boolValue)
            {
                drawer.Field("targetTriangleCount");   
            }
            drawer.Field("convexifyMesh");
            drawer.Field("weldColocatedVertices");

            if (drawer.Button("Update Simulation Mesh"))
            {
                UpdateSimulationMesh();
            }

            if (drawer.Button("Toggle In-Scene Preview"))
            {
                ToggleInScenePreview();
            }
            
            if (targets.Length == 1)
            {
                drawer.Label("Simulation Mesh Preview:");
                drawer.Space(5);
                if (Event.current.type == EventType.Repaint)
                {
                    DrawPreviewTexture(_waterObject, drawer.positionRect, out _previewHeight);
                }
                drawer.Space(_previewHeight);
            }
            drawer.EndSubsection();
            
            // Warnings
            drawer.BeginSubsection("Messages");
            DrawWarnings();
            drawer.Info($"Forces are being applied to '{_waterObject.TargetRigidbody}'");
            drawer.EndSubsection();
            
            drawer.EndEditor(this);
            return true;
        }

        void DrawWarnings()
        {
            if (WaterObject.ShowEditorWarnings && targets.Length == 1)
            {
                _editorHasWarnings = false;
                
                // Missing rigidbody
                if (_waterObject.TargetRigidbody == null)
                {
                    _waterObject.TargetRigidbody = _waterObject.transform.FindRootRigidbody();
                    if (_waterObject.TargetRigidbody == null)
                    {
                        drawer.Info($"WaterObject requires a rigidbody attached to the object or its parent(s) to " +
                                                $"function. Add a rigidbody to object {_waterObject.name} or one of its parents.", MessageType.Error);
                        _editorHasWarnings = true;
                        
                        if(drawer.Button("Add a Rigidbody"))
                        {
                            foreach (WaterObject wo in targets)
                            {
                                wo.gameObject.AddComponent<Rigidbody>();
                            }
                        }
                    }
                }

                // Collider count
                if (_waterObject.TargetRigidbody != null)
                {
                    int colliderCount = _waterObject.TargetRigidbody.transform.GetComponentsInChildren<Collider>()
                        .Length;
                    if (_waterObject.TargetRigidbody.transform.GetComponentsInChildren<Collider>().Length == 0)
                    {
                        drawer.Info($"Found {colliderCount} colliders attached to rigidbody {_waterObject.TargetRigidbody.name} " +
                                    $"and its children. At least one collider is required for a rigidbody to work properly.", MessageType.Error);
                        _editorHasWarnings = true;
                        
                        if(drawer.Button("Add a MeshCollider"))
                        {
                            foreach (WaterObject wo in targets)
                            {
                                MeshCollider mc = wo.gameObject.AddComponent<MeshCollider>();
                                mc.convex = true;
                                mc.isTrigger = false;
                            }
                        }
                    }
                }

                // Excessive triangle count
                if (_waterObject.TriangleCount > 150)
                {
                    drawer.Info($"Possible excessive number of triangles detected ({_waterObject.TriangleCount})." +
                                $" Use simplify mesh option to reduce the number of triangles, or if this is intentional ignore this message." +
                                $" Recommended number is 16-128.", MessageType.Warning);
                }

                // Scale error
                if (_waterObject.transform.localScale.x <= 0 
                    || _waterObject.transform.localScale.y <= 0 
                    || _waterObject.transform.localScale.z <= 0)
                {
                    drawer.Info($"Scale of this object is negative or zero on one or more of axes. Scale less than or equal to zero is not supported." +
                                $" WaterObject will still be calculated but with unpredictable results. ", MessageType.Error);
                }

                if (!_editorHasWarnings)
                {
                    drawer.Info($"No warnings or errors for object {_waterObject.name}.");
                }
            }
        }

        /*
        private void OnValidate()
        {
            if (waterObject == null) return;
            //waterObject.Init();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            waterObject = (WaterObject) target;
            if(!waterObject.Initialized) waterObject.Init();

            EditorUtils.DrawLogo("WaterObjectLogo");

            if (centeredStyle == null)
            {            
                centeredStyle = GUI.skin.GetStyle("Label");
                centeredStyle.alignment = TextAnchor.MiddleCenter;
                centeredStyle.normal.textColor = Color.white;
                centeredStyle.fontStyle = FontStyle.Bold;
            }

            if (!Application.isPlaying)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField("Simulation Mesh Settings", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(simplifyMesh, 
                new GUIContent("Simplify", "Should the simulation mesh be simplified? If the mesh has >50 triangles this option" +
                                                " is recommended. You can adjust simplification strength through 'Simplification Ratio' field."));

                if (waterObject.simplifyMesh)
                {
                    EditorGUILayout.PropertyField(targetTriangleCount, 
                        new GUIContent("Target Triangle Count", "Quality of the generated simplified / decimated mesh. Lower setting will result in a simulation mesh " +
                                                          "with lower number of triangles and therefore better performance - O(n). Use lowest acceptable setting."));
                }
            
                EditorGUILayout.PropertyField(convexifyMesh, 
                    new GUIContent("Convexify", "Should the simulation mesh be made convex? " +
                                                     "This must be used if the mesh is not closed (missing one of its surfaces, e.g. only bottom of the hull has triangles)."));


                GUILayout.EndVertical();
                

                // Material settings
                if (waterObject.TargetRigidbody != null)
                {
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    EditorGUILayout.LabelField("Material Settings", EditorStyles.boldLabel);
                    
                    if (waterObject.TargetRigidbody.GetComponent<MassFromChildren>() == null)
                    {
                        EditorGUILayout.HelpBox($"If you want to set mass of parent rigidbody {waterObject.TargetRigidbody.name} through its children, " +
                                                $"attach 'MassFromChildren' component to it.", MessageType.Info);
                        if(GUILayout.Button("Add MassFromChildren Component to Rigidbody"))
                        {
                            foreach (WaterObject wo in targets)
                            {
                                if(wo.TargetRigidbody != null && wo.TargetRigidbody.GetComponent<MassFromChildren>() == null)
                                {
                                    MassFromChildren rmfc = wo.TargetRigidbody.gameObject.AddComponent<MassFromChildren>();
                                    wo.WaterObjectMaterialIndex = 0;
                                    rmfc.Calculate();
                                }
                            }
                        }
                    }
                    else
                    {
                        int prevIndex = waterObject.WaterObjectMaterialIndex;
                        waterObject.WaterObjectMaterialIndex = EditorGUILayout.Popup(new GUIContent("Material Preset", 
                            "Sets the density of the object from one of the presets. If you want to set your own density, select 'Custom' option."), 
                            prevIndex, WaterObjectMaterials.MaterialNames);

                        if(prevIndex != waterObject.WaterObjectMaterialIndex)
                        {
                            foreach (WaterObject wo in targets) wo.WaterObjectMaterialIndex = waterObject.WaterObjectMaterialIndex;
                        }

                        EditorGUI.BeginDisabledGroup(waterObject.WaterObjectMaterialIndex != 0);

                        // Density
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(density, 
                            new GUIContent("Density", "Density in kg/m3 of the material. Mass will be calculated from this and the volume of the simulation mesh, " +
                            "which is auto-calculated."));
                        if(EditorGUI.EndChangeCheck())
                        {
                            foreach (WaterObject wo in targets)
                            {
                                wo.SetMaterialDensity(density.floatValue);
                            }
                            serializedObject.Update();
                        }

                        // Mass
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(mass, 
                            new GUIContent("Mass", "Mass in kg of this object. When multiple objects that are children of the same rigidbody have this field set, mass" +
                                                   " of the parent rigidbody will be calculated as a sum of all of children's masses."));
                        if (EditorGUI.EndChangeCheck())
                        {
                            foreach (WaterObject wo in targets)
                            {
                                wo.SetMaterialMass(mass.floatValue);
                            }
                            serializedObject.Update();
                        }
                        EditorGUI.EndDisabledGroup();

                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.FloatField(new GUIContent("Volume", "Auto-calculated."), waterObject.Volume);
                        EditorGUI.EndDisabledGroup();

                        if (GUILayout.Button("Update Volume"))
                        {
                            foreach (WaterObject wo in targets)
                            {
                                wo.UpdateVolume();
                                Undo.RecordObject(wo, "Calculate Volume");
                                EditorUtility.SetDirty(wo);
                            }
                        }

                        if (waterObject.Density < 80f)
                        {
                            EditorGUILayout.HelpBox(
                                $"Density of the object might be on the low side (<80 km/m3). Avoid this if you are using low-poly simulation mesh or have large triangles.", MessageType.Warning);
                        }
                        else if (WaterObjectManager.Instance != null && waterObject.Density > WaterObjectManager.Instance.fluidDensity)
                        {
                            EditorGUILayout.HelpBox(
                                $"This object has higher density than the fluid density setting on WaterObjectManager. It will sink.", MessageType.Info);
                        }
                    }
                    
                    EditorGUILayout.EndVertical();
                }

                
                
                
                GUILayout.Space(3);
                if (targets.Length == 1)
                {
                    if(waterObject.TargetRigidbody != null)
                        EditorGUILayout.HelpBox($"Forces are being applied to '{waterObject.TargetRigidbody}' rigidbody.", MessageType.Info);  
                }
                else
                {
                    EditorGUILayout.HelpBox(
                        $"In-editor preview and warnings are only available when a single WaterObject is selected.", MessageType.Info);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Exit play mode to make changes.", MessageType.Info);
            }

            
            GUILayout.Space(10);
            if (serializedObject.hasModifiedProperties)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
        */

        void UpdateSimulationMesh()
        {
            foreach (WaterObject wo in targets)
            {
                wo.StopSimMeshPreview();
                wo.Initialize();
                wo.GenerateSimMesh();
                Undo.RecordObject(wo, "Updated Simulation Mesh");
                EditorUtility.SetDirty(wo);
            }
        }

        void ToggleInScenePreview()
        {
            foreach (WaterObject wo in targets)
            {
                if (wo.PreviewEnabled)
                {
                    wo.StopSimMeshPreview();
                }
                else
                {
                    wo.StartSimMeshPreview();
                }
            }
        }
        
        void DrawPreviewTexture(WaterObject waterObject, Rect rect, out float previewHeight)
        {
            previewHeight = 0;
            
            if (!waterObject.Initialized)
            {
                drawer.Info("WaterObject not initialized.");
                return;
            }
            
            // Tri count
            int originalCount = waterObject.OriginalMesh == null ? 0 : waterObject.OriginalMesh.triangles.Length / 3;
            int simulationTriCount = waterObject.SerializedSimulationMesh.triangles.Length / 3;

            _originalMeshPreviewTexture = AssetPreview.GetAssetPreview(waterObject.OriginalMesh);
            _simMeshPreviewTexture = waterObject.SimulationMesh == null ? 
                AssetPreview.GetAssetPreview(waterObject.OriginalMesh) 
                : AssetPreview.GetAssetPreview(waterObject.SimulationMesh);
            
            float startY = rect.y;
            float previewWidth = rect.width;
            float maxPreviewWidth = 480f;
            previewWidth = Mathf.Clamp(previewWidth, 240f, maxPreviewWidth);
            float margin = (rect.width - previewWidth) * 0.5f;
            float halfWidth = previewWidth * 0.5f;
            
            Rect leftRect = new Rect(rect.x + margin, startY, halfWidth, halfWidth);
            Rect rightRect = new Rect(rect.x + halfWidth + margin, startY, halfWidth, halfWidth);

            Material previewMaterial = new Material(Shader.Find("UI/Default"));

            GUI.DrawTexture(leftRect, _originalMeshPreviewTexture == null ? _greyTexture : _originalMeshPreviewTexture);
            GUI.DrawTexture(rightRect, _simMeshPreviewTexture == null ? _greyTexture : _simMeshPreviewTexture);

            GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            centeredStyle.normal.textColor = Color.white;
            
            Rect leftLabelRect = leftRect;
            leftLabelRect.height = 20f;
            GUI.Label(leftLabelRect, "ORIGINAL", centeredStyle);

            Rect rightLabelRect = rightRect;
            rightLabelRect.height = 20f;
            GUI.Label(rightLabelRect, "SIMULATION", centeredStyle);

            Rect leftBottomLabelRect = leftRect;
            leftBottomLabelRect.y = leftRect.y + halfWidth - 20f;
            leftBottomLabelRect.height = 20f;
            GUI.Label(leftBottomLabelRect, originalCount + " tris");
            
            Rect rightBottomLabelrect = rightRect;
            rightBottomLabelrect.y = rightRect.y + halfWidth - 20f;
            rightBottomLabelrect.height = 20f;
            GUI.Label(rightBottomLabelrect, simulationTriCount + " tris");

            previewHeight = halfWidth;
        }
    }
}

