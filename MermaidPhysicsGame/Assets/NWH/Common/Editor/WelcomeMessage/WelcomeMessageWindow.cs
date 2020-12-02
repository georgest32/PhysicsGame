using UnityEditor;
using UnityEngine;
using NWH.Common.AssetInfo;

namespace NWH.Common.WelcomeMessage
{
    public class WelcomeMessageWindow : EditorWindow
    {
        public AssetInfo.AssetInfo assetInfo;
        
        public WelcomeMessageWindow Init(AssetInfo.AssetInfo info)
        {
            this.assetInfo = info;
            WelcomeMessageWindow window = (WelcomeMessageWindow)EditorWindow.GetWindowWithRect(typeof(WelcomeMessageWindow), new Rect(0, 0, 300, 326));
            window.Show();
            return window;
        }

        void OnGUI()
        {
            if (assetInfo == null)
            {
                return;
            }
            
            GUILayout.BeginVertical(GUILayout.Width(280)); 
            GUILayout.Space(8);
            GUILayout.Label($"Welcome to {assetInfo.assetName}", EditorStyles.boldLabel);
            GUILayout.Label($"Version {assetInfo.version}", EditorStyles.miniLabel);
            GUILayout.Space(8);
            GUILayout.Label($"Thank you for purchasing {assetInfo.assetName}.\n" +
                            "Check out the following useful links:");
            GUILayout.Space(5);
            GUILayout.Label("Existing customer?", EditorStyles.centeredGreyMiniLabel);
            if (GUILayout.Button("Upgrade Notes"))
            {
                Application.OpenURL(assetInfo.upgradeNotesURL);
            }

            if (GUILayout.Button("Changelog"))
            {
                Application.OpenURL(assetInfo.changelogURL);
            }
            GUILayout.Space(5);
            GUILayout.Label("New to the asset?", EditorStyles.centeredGreyMiniLabel);
            if (GUILayout.Button("Quick Start"))
            {
                Application.OpenURL(assetInfo.quickStartURL);
            }
            
            
            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL(assetInfo.documentationURL);
            }
            
            GUILayout.Space(15);
            GUILayout.Label("Also, don't forget to join us at Discord:", EditorStyles.centeredGreyMiniLabel);
            if (GUILayout.Button("Discord Server"))
            {
                Application.OpenURL(assetInfo.discordURL);
            }
            
            GUILayout.Space(15);
            GUILayout.Label("Don't have Discord?", EditorStyles.centeredGreyMiniLabel);
            if (GUILayout.Button("Forum"))
            {
                Application.OpenURL(assetInfo.forumURL);
            }

            if (GUILayout.Button("Email"))
            {
                Application.OpenURL(assetInfo.emailURL);
            }
        }
    }
}

