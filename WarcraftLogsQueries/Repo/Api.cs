using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Text;
using WarcraftLogsQueries.Models;

namespace WarcraftLogsQueries.Repo
{
    public class Api : IApi
    {
        private readonly ILogger<Api> _logger;
        private readonly IConfiguration _config;
        private readonly GraphQLHttpClient _graphClient;
        private readonly IMemoryCache _cache;
        public bool Initialized { get; set; } = false;

        public Api(ILogger<Api> logger, IConfiguration config, IMemoryCache cache)
        {
            _graphClient = new GraphQLHttpClient("https://www.warcraftlogs.com/api/v2/client", new SystemTextJsonSerializer());
            _logger = logger;
            _cache = cache;
            _config = config;
        }

        public async Task Initialize()
        {
            System.Diagnostics.Debug.WriteLine("Initialize was called");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_config["WarcraftLogs:ClientId"]}:{_config["WarcraftLogs:ClientSecret"]}")));

            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.warcraftlogs.com/oauth/token");

            var form = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };

            request.Content = new FormUrlEncodedContent(form);

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode) return;

            var responseJson = await response.Content.ReadAsStringAsync();
            var token = System.Text.Json.JsonSerializer.Deserialize<OAuth>(responseJson);

            _graphClient.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);
            Initialized = true;
        }

        public async Task<List<Event>> GetFilteredEvents(string fightCode, string filterExpression, int startTime, int endTime)
        {
            var cacheKey = $"{fightCode}:{filterExpression}:{startTime}:{endTime}";
            try
            {
                if (_cache.TryGetValue(cacheKey, out List<Event> events))
                {
                    return events;
                }

                var request = new GraphQL.GraphQLRequest
                {
                    Query = @"
query GetFilteredEvents($fightCode: String, $filterExpression: String, $startTime: Float, $endTime: Float){
	reportData {
		report(code: $fightCode) {
			events(filterExpression: $filterExpression, startTime: $startTime, endTime: $endTime){
				data
			}
		}
	}
}
",
                    OperationName = "",
                    Variables = new { fightCode, filterExpression, startTime, endTime }
                };

                var result = await _graphClient.SendQueryAsync<Data>(request);
                events = result.Data.ReportData.Report.Events.Data;

                _cache.Set(cacheKey, events);

                return events;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"GetFilteredEvents({fightCode},{filterExpression},{startTime},{endTime})");
                await Initialize();
                return null;
            }

        }

        /// <summary>
        /// This method should not be cached
        /// </summary>
        /// <param name="guildId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<Reports> GetPagedGuildReports(string guildId, int page, int limit = 10)
        {
            try
            {
                var request = new GraphQL.GraphQLRequest
                {
                    Query = @"
query GetFilteredEvents($guildId: Int, $limit: Int, $page: Int) {
	reportData {
		reports(guildID: $guildId, limit: $limit, page: $page) {
			current_page
			last_page
			data {
				title
				owner {
					name
				}
				code
				startTime
				endTime
				fights(killType: Encounters) {
					name
					kill
					fightPercentage
				}
			}
		}
	}
}
",
                    OperationName = "",
                    Variables = new { guildId, limit, page}
                };

                var result = await _graphClient.SendQueryAsync<Data>(request);

                var reports = result.Data.ReportData.Reports;

                return reports;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"GetPagedGuildReports({guildId}, {page}, {limit})");
                await Initialize();
                return null;
            }

        }


        public async Task<Report> GetReportMetaData(string fightCode)
        {
            var cacheKey = $"{fightCode}";

            try
            {
                if (_cache.TryGetValue(cacheKey, out Report report))
                {
                    return report;
                }

                var request = new GraphQL.GraphQLRequest
                {
                    Query = @"
query ReportMetaData($fightCode: String, $actorFilter: String) {
	reportData {
		report(code: $fightCode) {
			masterData {
				actors(type: $actorFilter) {
					id
					gameID
					name
					server
					type
					subType
				}
			}
			fights(killType: Encounters) {
				name
				kill
				fightPercentage
				startTime
				endTime
			}
		}
	}
}
",
                    OperationName = "",
                    Variables = new { fightCode, actorFilter = "Player" }
                };

                var result = await _graphClient.SendQueryAsync<Data>(request);
                report = result.Data.ReportData.Report;

                _cache.Set(cacheKey, report);

                return report;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"GetReportMetaData({fightCode})");
                await Initialize();
                return null;
            }

        }
    }
}
