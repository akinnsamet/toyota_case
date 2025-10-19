namespace Toyota.Shared.Extensions
{
    public static class MailTemplateExtensions
    {
        public static string Render(IDictionary<string, string> parameters,string template)
        {
            if (parameters.IsAny())
            {
                foreach (var item in parameters)
                {
                    template = template.Replace($"{{{{{item.Key}}}}}", item.Value);
                }
            }
            return template;
        }
    }
}
