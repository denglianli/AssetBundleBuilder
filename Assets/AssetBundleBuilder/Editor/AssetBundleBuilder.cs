/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *  FileName: AssetBundleBuilder.cs
 *  Author: Mogoson   Version: 0.1.0   Date: 8/4/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.       AssetBundleBuilder         Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson      8/4/2017      0.1.0        Create this file.
 *************************************************************************/

namespace Developer.AssetBundleBuilder
{
    using System;
    using UnityEditor;
    using UnityEngine;

    public class AssetBundleBuilder : EditorWindow
    {
        #region Property and Field
        private static AssetBundleBuilder instance;
        private const float rightAlignment = 80;

        private string path = "Assets";
        private BuildAssetBundleOptions options = BuildAssetBundleOptions.None;
        private BuildTarget platform = BuildTarget.Android;

        private const string pathKey = "AssetBundleBuildPath";
        private const string optionsKey = "AssetBundleBuildOptions";
        private const string platformKey = "AssetBundleTargetPlatform";
        #endregion

        #region Private Method
        [MenuItem("Tool/Asset Bundle Builder &B")]
        private static void ShowEditor()
        {
            instance = GetWindow<AssetBundleBuilder>("Asset Bundle");
            instance.Show();
        }

        private void OnEnable()
        {
            GetEditorPreferences();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical("Window");

            EditorGUILayout.BeginHorizontal();
            path = EditorGUILayout.TextField("Path", path);
            if (GUILayout.Button("Browse", GUILayout.Width(rightAlignment)))
                SelectBuildPath();
            EditorGUILayout.EndHorizontal();

            options = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup("Options", options);
            platform = (BuildTarget)EditorGUILayout.EnumPopup("Platform", platform);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Build", GUILayout.Width(rightAlignment)))
                BuildAssetBundles();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        private void SelectBuildPath()
        {
            var selectPath = EditorUtility.OpenFolderPanel("Select Build Path", "Assets", string.Empty);
            if (selectPath == string.Empty)
                return;
            try { path = selectPath.Substring(selectPath.IndexOf("Assets")); }
            catch { path = selectPath; }
        }

        private void BuildAssetBundles()
        {
            try
            {
                BuildPipeline.BuildAssetBundles(path, options, platform);
                AssetDatabase.Refresh();

                SetEditorPreferences();
            }
            catch (Exception e)
            {
                ShowNotification(new GUIContent(e.Message));
            }
        }

        private void SetEditorPreferences()
        {
            EditorPrefs.SetString(pathKey, path);
            EditorPrefs.SetInt(optionsKey, (int)options);
            EditorPrefs.SetInt(platformKey, (int)platform);
        }

        private void GetEditorPreferences()
        {
            path = EditorPrefs.GetString(pathKey, path);
            options = (BuildAssetBundleOptions)EditorPrefs.GetInt(optionsKey, (int)options);
            platform = (BuildTarget)EditorPrefs.GetInt(platformKey, (int)platform);
        }
        #endregion
    }
}