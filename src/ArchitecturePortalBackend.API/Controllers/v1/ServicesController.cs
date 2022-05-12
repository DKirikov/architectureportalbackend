using Microsoft.AspNetCore.Mvc;
using ArchitecturePortalBackend.BusinessLogic.Dto.ReadModels;
using ArchitecturePortalBackend.BusinessLogic.Dto.WriteModels;
using ArchitecturePortalBackend.BusinessLogic.Interfaces;

namespace ArchitecturePortalBackend.API.Controllers.v1;

/// <summary>
/// ServicesController class
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class ServicesController : ControllerBase
{
    private readonly ILogger<ServicesController> _logger;
    private readonly IServicesService _servicesService;

    /// <summary>
    /// ServicesController constructor
    /// </summary>
    public ServicesController(IServicesService servicesService, ILogger<ServicesController> logger)
    {
        _servicesService = servicesService;
        _logger = logger;
    }

    /// <summary>
    /// Get a list of services
    /// </summary>
    [ProducesResponseType(typeof(IEnumerable<ServiceRm>), 200)]
    [Produces("application/json")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _servicesService.GetServicesAsync().ConfigureAwait(false));
    }

    /// <summary>
    /// Get a service by id
    /// </summary>
    ///<param name= "id">Guid</param>
    [ProducesResponseType(typeof(ServiceDetailsRm), 200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _servicesService.GetServicesAsync(id).ConfigureAwait(false));
    }

    /// <summary>
    /// Create a new service 
    /// </summary>
    /// <returns>Created Service ID</returns>
    /// <param name="serviceWm">New service</param>
    [ProducesResponseType(typeof(EntityIdRm), 200)]
    [HttpPost]
    public async Task<IActionResult> Create(ServiceWm serviceWm)
    {
        return Ok(await _servicesService.CreateServiceAsync(serviceWm).ConfigureAwait(false));
    }

    /// <summary>
    /// Update an existing service by id
    /// </summary>
    /// <param name="id">Guid</param>
    /// <param name="serviceWm">Updated service</param>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ServiceWm serviceWm)
    {
        await _servicesService.UpdateServiceAsync(id, serviceWm).ConfigureAwait(false);

        return Ok();
    }

    /// <summary>
    /// Delete an existing service by id
    /// </summary>
    ///<param name= "id">Guid</param>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _servicesService.DeleteServiceAsync(id).ConfigureAwait(false);
        return Ok();
    }
}