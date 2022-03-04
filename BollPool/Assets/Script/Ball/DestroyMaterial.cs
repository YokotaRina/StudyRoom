using UnityEngine;

namespace Script
{
    /// <summary>
    /// マテリアルを破棄する
    /// </summary>
    public class DestroyMaterial : MonoBehaviour
    {
        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            // 複製されたマテリアルを全て破棄する
            // ここコメントアウトしたらマテリアルリークするよ
            var thisRenderer = this.GetComponent<Renderer>();
            if (thisRenderer != null && thisRenderer.materials != null)
            {
                foreach (var material in thisRenderer.materials)
                {
                    Destroy(material);
                }
            }
        }
    }
}
