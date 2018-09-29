using System;
using System.Collections.Generic;
using System.Text;

namespace Demos.AnotherNamespace
{
    public static class DemoExtensionMethod
    {
        public static string ThisIsAnExtensionMethodToString(this string s)
        {
            return s + " comes from the extension method";
        }
    }
}
