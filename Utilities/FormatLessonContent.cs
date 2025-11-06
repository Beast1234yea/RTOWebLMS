using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;
using System.Text;

namespace RTOWebLMS.Utilities
{
    public static class FormatLessonContent
    {
        public static async Task<bool> FixAllLessonFormatting(LmsDbContext db)
        {
            try
            {
                Console.WriteLine("📝 Fixing lesson content formatting...\n");

                var lessons = await db.Lessons.Where(l => !string.IsNullOrEmpty(l.Content)).ToListAsync();
                
                Console.WriteLine($"Found {lessons.Count} lessons with content to format\n");

                foreach (var lesson in lessons)
                {
                    Console.WriteLine($"Processing: {lesson.Title}");
                    
                    var formattedContent = FormatTextToHtml(lesson.Content);
                    
                    if (formattedContent != lesson.Content)
                    {
                        lesson.Content = formattedContent;
                        Console.WriteLine($"  ✅ Updated formatting");
                    }
                    else
                    {
                        Console.WriteLine($"  ➡️ No changes needed");
                    }
                }

                await db.SaveChangesAsync();
                
                Console.WriteLine($"\n✅ Successfully formatted {lessons.Count} lessons!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                return false;
            }
        }

        private static string FormatTextToHtml(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return content;

            var lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var html = new StringBuilder();

            html.AppendLine("<div class='lesson-content-formatted'>");

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                
                if (string.IsNullOrEmpty(trimmedLine))
                    continue;

                // Check if it's a heading (starts with numbers or capital letters)
                if (IsHeading(trimmedLine))
                {
                    html.AppendLine($"<h4 class='lesson-heading'>{trimmedLine}</h4>");
                }
                // Check if it's a list item
                else if (trimmedLine.StartsWith("•") || trimmedLine.StartsWith("-") || 
                         trimmedLine.StartsWith("*") || System.Text.RegularExpressions.Regex.IsMatch(trimmedLine, @"^\d+\."))
                {
                    html.AppendLine($"<li class='lesson-list-item'>{trimmedLine}</li>");
                }
                // Regular paragraph
                else
                {
                    html.AppendLine($"<p class='lesson-paragraph'>{trimmedLine}</p>");
                }
            }

            html.AppendLine("</div>");

            // Add some CSS styling
            var styledHtml = $@"
<style>
.lesson-content-formatted {{
    line-height: 1.6;
    color: #333;
}}
.lesson-heading {{
    color: #2c5aa0;
    margin: 20px 0 10px 0;
    font-weight: bold;
    border-bottom: 2px solid #e9ecef;
    padding-bottom: 5px;
}}
.lesson-paragraph {{
    margin: 15px 0;
    text-align: justify;
}}
.lesson-list-item {{
    margin: 8px 0;
    list-style-type: none;
    padding-left: 20px;
    position: relative;
}}
.lesson-list-item:before {{
    content: '✓';
    color: #28a745;
    font-weight: bold;
    position: absolute;
    left: 0;
}}
</style>
{html}";

            return styledHtml;
        }

        private static bool IsHeading(string line)
        {
            // Check various heading patterns
            return line.Length < 100 && (
                System.Text.RegularExpressions.Regex.IsMatch(line, @"^\d+\.?\s+[A-Z]") || // "1. Something" or "1 Something"
                System.Text.RegularExpressions.Regex.IsMatch(line, @"^[A-Z][A-Z\s]+$") || // "ALL CAPS"
                System.Text.RegularExpressions.Regex.IsMatch(line, @"^[A-Z][a-z]+\s[A-Z]") || // "Title Case"
                line.Contains("OBJECTIVE") ||
                line.Contains("LEARNING") ||
                line.Contains("SAFETY") ||
                line.Contains("OPERATION") ||
                line.Contains("MAINTENANCE")
            );
        }
    }
}