/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: AssetBundleBuilder.cs
 *  Author: Mogoson   Version: 1.0   Date: 8/4/2017
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
 *     1.     Mogoson     8/4/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.AssetBundleBuilder
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Editor for build asset bundle.
    /// </summary>
    public class AssetBundleBuilder : ScriptableWizard
    {
        #region Property and Field
        [Tooltip("Path of save asset bundles.")]
        public string path = "Assets/AssetBundles";

        [Tooltip("Options of build asset bundles.")]
        public BuildAssetBundleOptions options = BuildAssetBundleOptions.None;

        [Tooltip("Target build platform.")]
        public BuildTarget platform = BuildTarget.Android;

        private const string pathKey = "AssetBundleBuildPath";
        private const string optionsKey = "AssetBundleBuildOptions";
        private const string platformKey = "AssetBundleBuildPlatform";
        #endregion

        #region Private Method
        [MenuItem("Tool/AssetBundleBuilder &A")]
        static void ShowEditor()
        {
            DisplayWizard("AssetBundleBuilder", typeof(AssetBundleBuilder), "Build");
        }

        void OnEnable()
        {
            path = EditorPrefs.GetString(pathKey, path);
            options = (BuildAssetBundleOptions)EditorPrefs.GetInt(optionsKey, (int)options);
            platform = (BuildTarget)EditorPrefs.GetInt(platformKey, (int)platform);
        }

        void OnWizardUpdate()
        {
            if (path == string.Empty)
                isValid = false;
            else
                isValid = true;
        }

        void OnWizardCreate()
        {
            EditorPrefs.SetString(pathKey, path);
            EditorPrefs.SetInt(optionsKey, (int)options);
            EditorPrefs.SetInt(platformKey, (int)platform);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            BuildPipeline.BuildAssetBundles(path, options, platform);
            AssetDatabase.Refresh();
        }
        #endregion
    }//class_end
}//namespace_end