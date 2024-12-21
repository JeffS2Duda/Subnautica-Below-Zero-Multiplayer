namespace Subnautica.Events.EventArgs
{
    using System;

    public class WeatherProfileChangedEventArgs : EventArgs
    {
        public WeatherProfileChangedEventArgs(string profileId, bool isProfile, bool isAllowed = true)
        {
            this.ProfileId = profileId;
            this.IsProfile = isProfile;
            this.IsAllowed = isAllowed;
        }

        public string ProfileId { get; set; }

        public bool IsProfile { get; set; }

        public bool IsAllowed { get; set; }
    }
}
