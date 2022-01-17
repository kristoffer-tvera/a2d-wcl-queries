using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarcraftLogsQueries.Repo;

namespace WarcraftLogsQueries.Pages.Guild
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

        public Models.Reports Reports { get; set; }
        public int PageNumber { get; set; }
        public IEnumerable<int> Pages { get; set; }

        public string GuildId { get; set; }

        public async Task OnGetAsync(string id, int pageNumber = 1)
        {
            if (string.IsNullOrEmpty(id))
            {
                Response.Redirect("/");
                return;
            }

            GuildId = id;

            if (!_api.Initialized) await _api.Initialize();

            Reports = await _api.GetPagedGuildReports(id, pageNumber);

            PageNumber = Reports.Page;
            Pages = Pagination(Reports.Page, Reports.LastPage);
        }
        private IEnumerable<int> Pagination(int page, int lastPage)
        {
            var list = new List<int>();

            var start = page - 2;
            if (start < 1) start = 1;

            var end = start + 5;
            if (end > lastPage) end = lastPage + 1;

            foreach (var i in Enumerable.Range(start, end - start))
            {
                list.Add(i);
            }

            return list;
        }
    }
}
