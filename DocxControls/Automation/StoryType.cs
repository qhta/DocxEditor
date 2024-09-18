namespace DocxControls.Automation;

/// <summary>
/// Specifies the story type of selection or item.
/// </summary>
public enum StoryType
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  MainTextStory = 1,
  FootnotesStory = 2,
  EndnotesStory = 3,
  CommentsStory = 4,
  TextFrameStory = 5,
  EvenPagesHeaderStory = 6,
  PrimaryHeaderStory = 7,
  EvenPagesFooterStory = 8,
  PrimaryFooterStory = 9,
  FirstPageHeaderStory = 10,
  FirstPageFooterStory = 11,
  FootnoteSeparatorStory = 12,
  FootnoteContinuationSeparatorStory = 13,
  FootnoteContinuationNoticeStory = 14,
  EndnoteSeparatorStory = 15,
  EndnoteContinuationSeparatorStory = 16,
  EndnoteContinuationNoticeStory = 17,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
