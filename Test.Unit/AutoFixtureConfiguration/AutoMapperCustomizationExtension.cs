namespace Test.Unit.AutoFixtureConfiguration
{
    public static class AutoMapperCustomizationExtension
    {
        /// <summary>
        /// Add Mappers profiles.
        /// </summary>
        /// <param name="configurationExpression">Interface of type IMapperConfigurationExpression</param>
        /// <param name="profiles">List of type Profile</param>
        /// <returns>IMapperConfigurationExpression object type</returns>
        public static IMapperConfigurationExpression AddListProfiles(this IMapperConfigurationExpression configurationExpression, List<Profile> profiles)
        {
            foreach (Profile item in profiles)
            {
                configurationExpression.AddProfile(item);
            }

            return configurationExpression;
        }
    }
}
