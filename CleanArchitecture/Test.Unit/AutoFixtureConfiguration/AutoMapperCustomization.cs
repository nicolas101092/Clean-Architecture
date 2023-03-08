using System;
using System.Linq;

namespace Test.Unit.AutoFixtureConfiguration
{
    /// <summary>
    /// AutoMapper customization.
    /// </summary>
    public class AutoMapperCustomization : ICustomization
    {
        private const string MAPPER_FOLDER = "Application.Mapper";

        /// <summary>
        /// Add new Mapper configuration
        /// </summary>
        /// <param name="fixture">Fixture</param>
        public void Customize(IFixture fixture)
        {
            MapperConfiguration configuration = new(cfg =>
            {
                cfg.AddListProfiles(GerProfiles());
            });

            fixture.Register<IMapper>(() => new Mapper(configuration));
        }

        /// <summary>
        /// Get all mapper configurators.
        /// </summary>
        /// <returns>A list of all profiles in the application</returns>
        public static List<Profile> GerProfiles()
        {
            var mappers = from asm in AppDomain.CurrentDomain.GetAssemblies()
                            from type in asm.GetTypes()
                            where type.FullName is not null && type.FullName.StartsWith(MAPPER_FOLDER) && type.IsClass && type.IsSubclassOf(typeof(Profile))
                            select type;

            List<Profile> list = new();

            foreach (var mapper in mappers)
            {
                var obj = Activator.CreateInstance(mapper);

                if(obj is not null)
                {
                    list.Add((Profile)obj);
                }
            }

            return list;
        }
    }
}
