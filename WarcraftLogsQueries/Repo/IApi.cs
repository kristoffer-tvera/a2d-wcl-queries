using WarcraftLogsQueries.Models;

namespace WarcraftLogsQueries.Repo
{
    public interface IApi
    {
        bool Initialized { get; set; }

        Task<List<Event>> GetFilteredEvents(string fightCode, string filterExpression, int startTime, int endTime);
        Task<Reports> GetPagedGuildReports(string guildId, int page, int limit = 10);
        Task<Report> GetReportMetaData(string fightCode);
        Task Initialize();
    }
}