namespace Test.Unit.AutoFixtureConfiguration
{
    /// <summary>
    /// Attribute with fixture customizations.
    /// </summary>
    public class AutoFixtureAttribute : AutoDataAttribute
    {
        public AutoFixtureAttribute()
          : base(() => new Fixture()
          .Customize(new CompositeCustomization(new AutoMoqCustomization(),
                                                new AutoMapperCustomization(),
                                                new ValidatorCustomization())))
        {
        }
    }
}
