using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateSubtitle
{
    public class InitialModel
    {
        public List<SubtitleModel> InitialSegments { get; set; }
    }

    public class SubtitleModel
    {
        public SubtitleDetail TranscriptSegmentRenderer { get; set; }
    }

    public class SubtitleDetail
    {
        public string StartMs { get; set; }
        public string EndMs { get; set; }
        public SnippetModel Snippet { get; set; }
    }

    public class SnippetModel
    {
        public List<TextSubtitleModel> Runs { get; set; }
    }
    public class TextSubtitleModel
    {
        public string Text { get; set; }
    }
}
