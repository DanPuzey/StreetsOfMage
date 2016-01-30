using UnityEngine;

namespace WizardDuel.UI
{
    public class ComboDisplay : MonoBehaviour
    {
        public SpriteRenderer[] Alphabet;
        public int[] Combo;
        public float GlyphSpacing = 1f;

        public void DrawCombo()
        {
            foreach (Transform c in transform)
            {
                Destroy(c.gameObject);
            }

            for (var i = 0; i < Combo.Length; i++)
            {
                var glyphIndex = Combo[i];
                var glyphPrefab = Alphabet[glyphIndex];

                var thisGlyph = Instantiate(glyphPrefab);
                thisGlyph.transform.parent = transform;
                thisGlyph.transform.localPosition = new Vector3(i * GlyphSpacing, 0, 0);
            }
        }
    }
}
