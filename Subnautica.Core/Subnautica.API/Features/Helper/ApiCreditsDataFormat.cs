namespace Subnautica.API.Features.Helper
{
    using System.Collections.Generic;
    
    public class ApiCreditsDataFormat
    {
        public ApiCreditsDataItemFormat ProjectOwner { get; set; } = new ApiCreditsDataItemFormat();
        
        public ApiCreditsDataItemFormat ServerOwners { get; set; } = new ApiCreditsDataItemFormat();
        
        public ApiCreditsDataItemFormat DiscordAdmins { get; set; } = new ApiCreditsDataItemFormat();
        
        public ApiCreditsDataItemFormat DiscordMods { get; set; } = new ApiCreditsDataItemFormat();
        
        public ApiCreditsDataItemFormat PatreonSupporters { get; set; } = new ApiCreditsDataItemFormat();
        
        public ApiCreditsDataItemFormat Translators { get; set; } = new ApiCreditsDataItemFormat();
        
        public ApiCreditsDataItemFormat AlphaTesters { get; set; } = new ApiCreditsDataItemFormat();
    }

    public class ApiCreditsDataItemFormat
    {
        public string Name { get; set; }

        public List<ApiCreditsDataMemberItemFormat> Members { get; set; } = new List<ApiCreditsDataMemberItemFormat>();
    }

    public class ApiCreditsDataMemberItemFormat
    {
        public string Name { get; set; }
    }
}
