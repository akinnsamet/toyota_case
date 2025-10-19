using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Toyota.Api.Authorization;
using Toyota.Shared.Entities.Common;
using Toyota.Shared.Extensions;

namespace Toyota.Api.Controllers.Area
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationLogController : ControllerBase
    {
        private readonly string _logFilePath;
        public ApplicationLogController()
        {
            _logFilePath = Path.Combine("/app/Data", "applicationLogs.txt");
        }
        #region Methods

        [HttpPost]
        [Authorize]
        [PolicyBasedAuthorize]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ServiceResponse<List<string>>), 200)]
        public async Task<IActionResult> GetAll([FromBody] SearchEntity req)
        {
            if (!System.IO.File.Exists(_logFilePath))
                return Ok(new ServiceResponse<List<string>>());

            var query = await System.IO.File.ReadAllLinesAsync(_logFilePath, Encoding.UTF8);
            var lines = query.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

            if (req.SearchText.IsNotNullOrNotWhiteSpace())
            {
                lines = lines.Where(x => x.Contains(req.SearchText!)).ToList();
            }
            lines = lines.OrderByDescending(x => x).ToList();
            int totalRecords = lines.Count;
            int numberRecords = req?.SortingPaging?.NumberRecords ?? 10;
            int pageNumber = req?.SortingPaging?.PageNumber ?? 1;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)numberRecords);

            var pagedLines = lines.Skip((pageNumber - 1) * numberRecords).Take(numberRecords).ToList();

            return Ok(new ServiceResponse<List<string>>(pagedLines, totalRecords));
        }


        #endregion
    }
}
