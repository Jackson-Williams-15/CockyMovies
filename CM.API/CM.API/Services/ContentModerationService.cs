using System;
using System.Text.RegularExpressions;
using profanity_detector;

namespace CM.API.Services
{
    /// <summary>
    /// Provides functionality to moderate content by detecting and censoring profanity.
    /// </summary>
    public class ContentModerationService
    {
        private readonly ProfanityDetection _profanityDetector;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentModerationService"/> class.
        /// </summary>
        public ContentModerationService()
        {
            _profanityDetector = new ProfanityDetection();
        }

        /// <summary>
        /// Censors the content by replacing profane words with asterisks.
        /// </summary>
        /// <param name="content">The content to be censored.</param>
        /// <returns>The censored content.</returns>
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