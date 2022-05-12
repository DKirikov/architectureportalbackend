using System.ComponentModel.DataAnnotations;

namespace ArchitecturePortalBackend.Migrator.Settings;

public class MigratorSettings
{
    public string TargetMigration { get; set; } = string.Empty;
    [Required]
    public string UserId { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string OwnerRole { get; set; } = string.Empty;
    public bool IntegratedSecurity { get; set; } = false;
}