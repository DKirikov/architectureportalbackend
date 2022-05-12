using Microsoft.AspNetCore.Mvc;
using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;
using ArchitecturePortalBackend.BusinessLogic.Interfaces;

namespace ArchitecturePortalBackend.API.Controllers.v1;

/// <summary>
/// ModulesController class
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class ModulesController : ControllerBase
{
    private readonly ILogger<ModulesController> _logger;
    private readonly IModulesService _modulesService;

    /// <summary>
    /// ModulesController constructor
    /// </summary>
    public ModulesController(IModulesService modulesService, ILogger<ModulesController> logger)
    {
        _modulesService = modulesService;
        _logger = logger;
    }

    /// <summary>
    /// Get a list of modules
    /// </summary>
    [ProducesResponseType(typeof(IEnumerable<ModuleRm>), 200)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _modulesService.GetModulesAsync().ConfigureAwait(false));
    }

    /// <summary>
    /// Get a module by id
    /// </summary>
    ///<param name= "id">Guid</param>
    [ProducesResponseType(typeof(ModuleDetailsRm), 200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _modulesService.GetModulesAsync(id).ConfigureAwait(false));
    }

    /// <summary>
    /// Create a new module 
    /// </summary>
    /// <returns>Created Module ID</returns>
    /// <param name="moduleWm">New module</param>
    [ProducesResponseType(typeof(EntityIdRm), 200)]
    [Produces("application/json")]
    [HttpPost]
    public async Task<IActionResult> Create(ModuleWm moduleWm)
    {
        return Ok(await _modulesService.CreateModuleAsync(moduleWm).ConfigureAwait(false));
    }

    /// <summary>
    /// Update an existing module by id
    /// </summary>
    /// <param name="id">Guid</param>
    /// <param name="moduleWm">Updated module</param>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ModuleWm moduleWm)
    {
        await _modulesService.UpdateModuleAsync(id, moduleWm).ConfigureAwait(false);

        return Ok();
    }

    /// <summary>
    /// Delete an existing module by id
    /// </summary>
    ///<param name= "id">Guid</param>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _modulesService.DeleteModuleAsync(id).ConfigureAwait(false);
        return Ok();
    }
}