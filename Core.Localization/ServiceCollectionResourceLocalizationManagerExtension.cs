using Core.Localization.Abstraction;
using Core.Localization.Resource.Yaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Localization;

public static class ServiceCollectionResourceLocalizationManagerExtension
{
    public static IServiceCollection AddYamlResourceLocalization(this IServiceCollection services)
    {
        services.AddScoped<ILocalizationService, ResourceLocalizationManager>(_ =>
        {
            // <locale, <featureName, resourceDir>>
            Dictionary<string, Dictionary<string, string>> resources = [];
            try
            {
                var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
     
                string[] featureDirs = Directory.GetDirectories(path!, "Features");
                foreach (string featureDir in featureDirs)
                {
                    string featureName = Path.GetFileName(featureDir);
                    string localeDir = Path.Combine(featureDir, "Resources", "Locales");
                    if (Directory.Exists(localeDir))
                    {
                        string[] localeFiles = Directory.GetFiles(localeDir);
                        foreach (string localeFile in localeFiles)
                        {
                            string localeName = Path.GetFileNameWithoutExtension(localeFile);
                            int separatorIndex = localeName.IndexOf('.');
                            string localeCulture = localeName[(separatorIndex + 1)..];

                            if (File.Exists(localeFile))
                            {
                                if (!resources.ContainsKey(localeCulture))
                                    resources.Add(localeCulture, []);
                                resources[localeCulture].Add(featureName, localeFile);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

               // throw;
            }
  

            return new ResourceLocalizationManager(resources);
        });

        return services;
    }
}