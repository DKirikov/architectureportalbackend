using Microsoft.AspNetCore.Mvc;
using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;
using ArchitecturePortalBackend.BusinessLogic.Interfaces;

namespace ArchitecturePortalBackend.API.Controllers.v1;

/// <summary>
/// ContractsController class
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class ContractsController : ControllerBase
{
    private readonly ILogger<ContractsController> _logger;
    private readonly IContractsService _contractsService;

    /// <summary>
    /// ContractsController constructor
    /// </summary>
    public ContractsController(IContractsService contractsService, ILogger<ContractsController> logger)
    {
        _contractsService = contractsService;
        _logger = logger;
    }

    /// <summary>
    /// Get a list of contracts
    /// </summary>
    [ProducesResponseType(typeof(IEnumerable<ContractRm>), 200)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _contractsService.GetContractsAsync().ConfigureAwait(false));
    }

    /// <summary>
    /// Get a contract by id
    /// </summary>
    ///<param name= "id">Guid</param>
    [ProducesResponseType(typeof(ContractDetailsRm), 200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _contractsService.GetContractsAsync(id).ConfigureAwait(false));
    }

    /// <summary>
    /// Create a new contract
    /// </summary>
    /// <returns>Created Contract ID</returns>
    /// <param name="contractWm">New contract</param>
    [ProducesResponseType(typeof(EntityIdRm), 200)]
    [Produces("application/json")]
    [HttpPost]
    public async Task<IActionResult> Create(ContractWm contractWm)
    {
        return Ok(await _contractsService.CreateContractAsync(contractWm).ConfigureAwait(false));
    }

    /// <summary>
    /// Update an existing contract by id
    /// </summary>
    /// <param name="id">Guid</param>
    /// <param name="contractWm">Updated contract</param>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ContractWm contractWm)
    {
        await _contractsService.UpdateContractAsync(id, contractWm).ConfigureAwait(false);

        return Ok();
    }

    /// <summary>
    /// Delete an existing contract by id
    /// </summary>
    ///<param name= "id">Guid</param>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _contractsService.DeleteContractAsync(id).ConfigureAwait(false);
        return Ok();
    }
}