using System.ComponentModel.DataAnnotations;

namespace GalaxyWaves_Media.Areas.admin.Models.Media
{
    public class _URL
    {
        [Required][DataType(DataType.EmailAddress)] public string? Facebook { get; set; }
        [Required][DataType(DataType.EmailAddress)] public string? LinkIn { get; set; }
        [Required][DataType(DataType.EmailAddress)] public string? Twitter { get; set; }
        [Required][DataType(DataType.EmailAddress)] public string? Google { get; set; }
        [Required][DataType(DataType.EmailAddress)] public string? Instagram { get; set; }

    }
}
