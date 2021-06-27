using AutoFixture;
using AutoFixture.Xunit2;
using TestHelper.Extensions;

namespace TestHelper
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(() => new Fixture().AddAutoMoqDataCustomizations())
        {
        }
    }
}