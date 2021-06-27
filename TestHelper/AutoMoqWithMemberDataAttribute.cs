using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace TestHelper
{
    [DataDiscoverer("AutoFixture.Xunit2.NoPreDiscoveryDataDiscoverer", "AutoFixture.Xunit2")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AutoMoqWithMemberAndInlineDataAttribute : DataAttribute
    {
        private readonly object[] _objects;
        private readonly MemberDataAttribute _memberDataAttribute;

        public AutoMoqWithMemberAndInlineDataAttribute(string memberName, params object[] objects)
        {
            _memberDataAttribute = new MemberDataAttribute(memberName);
            _objects = objects;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod is null)
            {
                throw new ArgumentNullException(nameof(testMethod));
            }

            var memberDataCollection = _memberDataAttribute.GetData(testMethod);
            foreach (var memberData in memberDataCollection)
            {
                yield return new AutoMoqWithInlineDataAttribute(memberData.Concat(_objects).ToArray()).GetData(testMethod).Single();
            }
        }
    }
}