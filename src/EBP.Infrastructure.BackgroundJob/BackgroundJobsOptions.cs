namespace EBP.Infrastructure.BackgroundJob
{
    public class BackgroundJobsOptions
    {
        public const string BackgroundJobs = "BackgroundJobs";

        public TimeSpan AllowedExpirationBookedPeriod { get; set; }
    }
}
