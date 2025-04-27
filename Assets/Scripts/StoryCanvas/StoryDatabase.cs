using System.Collections.Generic;

namespace Story
{
    public static class StoryDatabase
    {
        private static List<StoryEntryData> _collectedStories = new List<StoryEntryData>();

        public static void SaveStory(string buttonName, string storyText)
        {
            if (!_collectedStories.Exists(s => s.ButtonName == buttonName))
            {
                _collectedStories.Add(new StoryEntryData
                {
                    ButtonName = buttonName,
                    StoryText = storyText
                });
            }
        }

        public static List<StoryEntryData> GetCollectedStories()
        {
            return _collectedStories;
        }

        public class StoryEntryData
        {
            public string ButtonName;
            public string StoryText;
        }
    }
}
