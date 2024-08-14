namespace DocxControls;

/// <summary>
/// Setting categories groups.
/// </summary>
public class SettingCategoryGroups
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  public static readonly SettingCategories[] LayoutAndUI = [ SettingCategories.Layout, SettingCategories.UI ];
  public static readonly SettingCategories[] SaveLoadAndSecurity = [ SettingCategories.Save, SettingCategories.Load, SettingCategories.Security ];
  public static readonly SettingCategories[] ThemingAndStyles = [SettingCategories.Theming, SettingCategories.Styles];
  public static readonly SettingCategories[] RevisionsAndTracking = [SettingCategories.Revisions, SettingCategories.Tracking];
  public static readonly SettingCategories[] ProofingHyphenationAndAutomation = [SettingCategories.Proofing, SettingCategories.Hyphenation, SettingCategories.Automation];
  public static readonly SettingCategories[] CustomXml = [SettingCategories.CustomXml];
  public static readonly SettingCategories[] MailMergeAndPrinting = [SettingCategories.MailMerge, SettingCategories.Print];
  public static readonly SettingCategories[] Identification = [SettingCategories.Identification];
  public static readonly SettingCategories[] Compatibility = [SettingCategories.Compatibility];
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
