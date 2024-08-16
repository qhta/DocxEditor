namespace DocxControls;

/// <summary>
/// Setting categories groups.
/// </summary>
public class SettingCategoryGroup
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  public static readonly SettingCategory[] LayoutAndUI = [ SettingCategory.Layout, SettingCategory.UI ];
  public static readonly SettingCategory[] LoadSaveAndSecurity = [ SettingCategory.Load, SettingCategory.Save, SettingCategory.Security ];
  public static readonly SettingCategory[] ThemingAndStyles = [SettingCategory.Theming, SettingCategory.Styles];
  public static readonly SettingCategory[] RevisionsAndTracking = [SettingCategory.Revisions, SettingCategory.Tracking];
  public static readonly SettingCategory[] ProofingHyphenationAndAutomation = [SettingCategory.Proofing, SettingCategory.Hyphenation, SettingCategory.Automation];
  public static readonly SettingCategory[] CustomXml = [SettingCategory.CustomXml];
  public static readonly SettingCategory[] MailMergeAndPrinting = [SettingCategory.MailMerge, SettingCategory.Printing];
  public static readonly SettingCategory[] Identification = [SettingCategory.Identification];
  public static readonly SettingCategory[] Compatibility = [SettingCategory.Compatibility];
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
