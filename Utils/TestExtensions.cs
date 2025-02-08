using NUnit.Framework.Interfaces;
using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Reflection;

namespace PlanNotePlaywrite.Utils
{
    public static class TestExtensions
    {
        public static void AddRetryLogic(this MethodInfo method)
        {
            var retryAttribute = method.GetCustomAttribute<RetryAttribute>();
            if (retryAttribute == null)
            {
                var attributes = method.GetCustomAttributes(true).ToList();
                attributes.Add(new RetryAttribute(3)); // Set your desired retry count
                var newAttributes = attributes.ToArray();
                typeof(MethodBase).GetMethod("SetCustomAttributes", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(method, new object[] { newAttributes });
            }
        }
    }
}
