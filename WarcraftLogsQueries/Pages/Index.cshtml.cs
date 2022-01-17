using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using WarcraftLogsQueries.Repo;

namespace WarcraftLogsQueries.Pages
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

        public async Task OnGetAsync()
        {
            if (!_api.Initialized) await _api.Initialize();
            var result = await _api.GetReportMetaData("jnAH2aYQw38PRrv4");
        }

        public IActionResult OnPost(string link, string analysis)
        {
            var uri = new Uri(link);

            if (uri.AbsolutePath.StartsWith("/guild/", StringComparison.InvariantCultureIgnoreCase))
            {
                var regex = Regex.Match(uri.AbsolutePath, @"\/(\d+)");

                if (!regex.Success) return new RedirectToPageResult("/Index", new { error = "UnableToFindGuildIdInUrl" });

                return new RedirectToPageResult("/Guild/Index", new { id = regex.Groups[1].Value });
            }


            var lastSegment = uri.Segments.Last();
            return new RedirectToPageResult("/Report/Index", new { id = lastSegment, analysis });
        }
    }
}