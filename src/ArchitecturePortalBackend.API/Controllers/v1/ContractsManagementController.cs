using Microsoft.AspNetCore.Mvc;
using ArchitecturePortalBackend.BusinessLogic.Interfaces;

namespace ArchitecturePortalBackend.API.Controllers.v1;

/// <summary>
/// ContractsController class
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class ContractsManagementController : ControllerBase
{
    private readonly ILogger<ContractsController> _logger;
    private readonly IContractsManagementService _contractsManagementService;

    /// <summary>
    /// ContractsManagementController constructor
    /// </summary>
    public ContractsManagementController(IContractsManagementService contractsManagementService, ILogger<ContractsController> logger)
    {
        _contractsManagementService = contractsManagementService;
        _logger = logger;
    }

    /// <summary>
    /// Mark a contract as a used contract in a service
    /// </summary>
    /// <param name="contractId">Guid</param>
    /// <param name="serviceId">Guid</param>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpPut(nameof(MarkContractAsUsed))]
    public async Task<IActionResult> MarkContractAsUsed(Guid contractId, Guid serviceId)
    {
        await _contractsManagementService.MarkContractAsUsed(contractId, serviceId).ConfigureAwait(false);

        return Ok();
    }

    /// <summary>
    /// Unmark a contract as a used contract in a service
    /// </summary>
    /// <param name="contractId">Guid</param>
    /// <param name="serviceId">Guid</param>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpPut(nameof(UnmarkContractAsUsed))]
    public async Task<IActionResult> UnmarkContractAsUsed(Guid contractId, Guid serviceId)
    {
        await _contractsManagementService.UnmarkContractAsUsed(contractId, serviceId).ConfigureAwait(false);

        return Ok();
    }
}
