using Toyota.Shared.Entities.Common;

namespace Toyota.Shared.Entities.Translation
{
    public class TranslationEntity:BaseEntity
    {

        public string Culture { get; set; }

        public string Code { get; set; }

        public string Value { get; set; }

        public string? Description { get; set; }
    }
}
