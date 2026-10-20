using MCPForUnity.Editor.Helpers;
using MCPForUnity.Editor.Windows.Components.ClientConfig;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.UIElements;

namespace MCPForUnityTests.Editor.Windows
{
    public class ClientConfigSectionTests
    {
        [Test]
        public void PackageLayout_InitializesClientCallbacks()
        {
            string path = AssetPathUtility.GetMcpPackageRootPath()
                + "/Editor/Windows/Components/ClientConfig/McpClientConfigSection.uxml";
            var tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            Assert.IsNotNull(tree);
            var root = tree.CloneTree();

            Assert.IsNotNull(root.Q<Button>("configure-all-button"));
            Assert.IsNotNull(root.Q<Foldout>("client-details-foldout"));
            Assert.DoesNotThrow(() => new McpClientConfigSection(root));
            Assert.IsNotNull(root.Q<BaseField<string>>("client-dropdown"));
        }
    }
}
