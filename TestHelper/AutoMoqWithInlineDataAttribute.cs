using AutoFixture.Xunit2;

namespace TestHelper
{
    public class AutoMoqWithInlineDataAttribute : InlineAutoDataAttribute
    {
        public AutoMoqWithInlineDataAttribute(params object[] objects) : base(new AutoMoqDataAttribute(), objects)
        {
        }
    }
}