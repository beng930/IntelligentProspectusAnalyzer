namespace OpenAIRequestExample
{
    public class PromptTemplate
    {
        public string? PromptType { get; set; }
        public string Template { get; set; } = string.Empty;
        public TunningParameters TunningParameters { get; set; } = new()
        {
            max_tokens = 500,
            top_p = 1,
            temperature = 1,
        };
    }

    public class TunningParameters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public double? temperature { get; set; } = 0.3;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public int? max_tokens { get; set; } = 2000;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public double? top_p { get; set; } = 0;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public float? frequency_penalty { get; set; } = 0;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public int? presence_penalty { get; set; } = 0;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public int? best_of { get; set; } = 1;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public string? stop { get; set; } = null;
    }
}
