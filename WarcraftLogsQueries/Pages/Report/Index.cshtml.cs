using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarcraftLogsQueries.Repo;

namespace WarcraftLogsQueries.Pages.Report
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IApi _api;

        public IndexModel(ILogger<IndexModel> logger, IApi api)
        {
            _logger = logger;
            _api = api;
        }

        public Models.Report? Report { get; set; }

        public async Task OnGetAsync(string id, string analysis)
        {
            if(string.IsNullOrEmpty(id))
            {
                Response.Redirect("/");
                return;
            }

            if(!_api.Initialized) await _api.Initialize();

            Report = await _api.GetReportMetaData(id);
        }
    }
}
