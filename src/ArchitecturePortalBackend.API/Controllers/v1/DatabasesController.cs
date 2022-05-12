using Microsoft.AspNetCore.Mvc;
using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;
using ArchitecturePortalBackend.BusinessLogic.Interfaces;

namespace ArchitecturePortalBackend.API.Controllers.v1;

/// <summary>
/// DatabaseController class
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class DatabasesController : ControllerBase
{
    private readonly ILogger<DatabasesController> _logger;
    private readonly IDatabaseService _databaseService;

    /// <summary>
    /// DatabaseController constructor
    /// </summary>
    public DatabasesController(IDatabaseService databaseService, ILogger<DatabasesController> logger)
    {
        _databaseService = databaseService;
        _logger = logger;
    }

    /// <summary>
    /// Get a list of databases
    /// </summary>
    [ProducesResponseType(typeof(IEnumerable<DatabaseRm>), 200)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _databaseService.GetDatabasesAsync().ConfigureAwait(false));
    }

    /// <summary>
    /// Get a database by id
    /// </summary>
    ///<param name= "id">Guid</param>
    [ProducesResponseType(typeof(DatabaseDetailsRm), 200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _databaseService.GetDatabasesAsync(id).ConfigureAwait(false));
    }

    /// <summary>
    /// Create a new database
    /// </summary>
    /// <returns>Created database ID</returns>
    /// <param name="databaseWm">New database</param>
    [ProducesResponseType(typeof(EntityIdRm), 200)]
    [Produces("application/json")]
    [HttpPost]
    public async Task<IActionResult> Create(DatabaseWm databaseWm)
    {
        return Ok(await _databaseService.CreateDatabaseAsync(databaseWm).ConfigureAwait(false));
    }

    /// <summary>
    /// Update an existing database by id
    /// </summary>
    /// <param name="id">Guid</param>
    /// <param name="databaseWm">Updated database</param>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, DatabaseWm databaseWm)
    {
        await _databaseService.UpdateDatabaseAsync(id, databaseWm).ConfigureAwait(false);

        return Ok();
    }

    /// <summary>
    /// Delete an existing database by id
    /// </summary>
    ///<param name= "id">Guid</param>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _databaseService.DeleteDatabaseAsync(id).ConfigureAwait(false);
        return Ok();
    }
}