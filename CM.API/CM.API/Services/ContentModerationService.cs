using System;
using System.Text.RegularExpressions;
using profanity_detector;

namespace CM.API.Services
{
    public class ContentModerationService
    {
        private readonly ProfanityDetection _profanityDetector;

        public ContentModerationService()
        {
            _profanityDetector = new ProfanityDetection();
        }

        public string CensorContent(string content)
        {
            // Detect profanity in the content
            var hasProfanity = _profanityDetector.isToxic(content);

            if (hasProfanity)
            {
                // Use Regex to find and replace profane words with asterisks
                var regex = new Regex(@"\b\w+\b", RegexOptions.IgnoreCase);
                content = regex.Replace(content, match =>
                {
                    return _profanityDetector.isToxic(match.Value) ? new string('*', match.Value.Length) : match.Value;
                });
            }

            return content;
        }
    }
}