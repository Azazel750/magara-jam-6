using System.Collections.Generic;
using UnityEngine;

namespace NOK
{
    public static partial class Collections
    {
        public static string RandomText(params string[] texts)
        {
            var rnd = Random.Range(0, texts.Length);
            return texts[rnd];
        }
    }
}
