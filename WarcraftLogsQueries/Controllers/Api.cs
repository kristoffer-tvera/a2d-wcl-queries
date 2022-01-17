using Microsoft.AspNetCore.Mvc;
using WarcraftLogsQueries.Repo;

namespace WarcraftLogsQueries.Controllers
{
    [ApiController]
    public class Api : ControllerBase
    {
        private readonly ILogger<Api> _logger;
        private readonly IApi _api;
        public Api(ILogger<Api> logger, IApi api)
        {
            _logger = logger;
            _api = api;
        }

        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> GetEvents(string fightCode, string filterExpression, int startTime, int endTime)
        {
            if(!_api.Initialized) await _api.Initialize();
            var events = await _api.GetFilteredEvents(fightCode, filterExpression, startTime, endTime);
            return new JsonResult(events);
        }
    }
}
