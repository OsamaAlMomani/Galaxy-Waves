using System.ComponentModel.DataAnnotations;

namespace GalaxyWaves.Models.SharedModels
{
    public class Links
    {
        [DataType(DataType.EmailAddress)]
        public string? LinkedIn { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Twitter { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Facebook { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Instegram { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? PersonalWebSite { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Gmail { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Outlook { get; set; }
    }
}
