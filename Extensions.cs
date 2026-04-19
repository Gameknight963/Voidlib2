using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VoidLib2
{
    public static class Extensions
    {
        public static GameObject FindFirstChild(this GameObject g, string name)
            => g.transform.Find(name).gameObject;
    }
}
