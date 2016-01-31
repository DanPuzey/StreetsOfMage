using System;
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
            Clear();

            var offset = Combo.Length * GlyphSpacing / 2;

            for (var i = 0; i < Combo.Length; i++)
            {
                var glyphIndex = Combo[i];
                var glyphPrefab = Alphabet[glyphIndex];

                var thisGlyph = Instantiate(glyphPrefab);
                thisGlyph.transform.parent = transform;

                var xPosition = (i * GlyphSpacing) - offset;
                thisGlyph.transform.localPosition = new Vector3(xPosition, 0, 0);
            }
        }

        public void Clear()
        {
            foreach (Transform c in transform)
            {
                Destroy(c.gameObject);
            }
        }
    }
}
