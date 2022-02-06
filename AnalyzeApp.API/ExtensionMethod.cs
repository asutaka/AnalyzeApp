using PhoneNumbers;
using Quartz;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AnalyzeApp.API
{
    public static class ExtensionMethod
    {
        public static void AddJobAndTrigger<T>(
       this IServiceCollectionQuartzConfigurator quartz,
       IConfiguration config)
       where T : IJob
        {
            // Use the name of the IJob as the appsettings.json key
            string jobName = typeof(T).Name;

            // Try and load the schedule from configuration
            var configKey = $"Quartz:{jobName}";
            var cronSchedule = config[configKey];

            // Some minor validation
            if (string.IsNullOrEmpty(cronSchedule))
            {
                throw new Exception($"No Quartz.NET Cron schedule found for job in configuration at {configKey}");
            }

            // register the job as before
            var jobKey = new JobKey(jobName);
            quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));

            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity(jobName + "-trigger")
                .WithCronSchedule(cronSchedule)); // use the schedule from configuration
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            try
            {
                return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string PhoneFormat(this string input, bool isCharacter = true)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            var region = string.Empty;
            if (input.Substring(0, 1) != "+")
                region = "VN";
            var val = GetPhone(input, region);
            return isCharacter ? val.Item2 : val.Item2.Replace("+", "");
        }

        private static (int, string) GetPhone(string input, string region = "")
        {
            try
            {
                var phoneUtil = PhoneNumberUtil.GetInstance();
                if (input.Length >= 9 && input.Length <= 20)
                {
                    var numberProto = phoneUtil.Parse(input.Trim(), region);
                    int countryCode = numberProto.CountryCode;
                    var formattedPhone = phoneUtil.Format(numberProto, PhoneNumberFormat.INTERNATIONAL).Replace(" ", "");
                    return (countryCode, formattedPhone);
                }
                return (0, string.Empty);
            }
            catch (NumberParseException ex)
            {
                NLogLogger.PublishException(ex, $"ExtensionMethod|GetPhone: {ex.Message}");
                return (0, string.Empty);
            }
        }
    }
}
