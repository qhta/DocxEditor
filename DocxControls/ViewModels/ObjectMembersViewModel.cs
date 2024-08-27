using System.Collections.ObjectModel;

namespace DocxControls;
/// <summary>
/// Collection of object members
/// </summary>
public class ObjectMembersViewModel: CustomObservableCollection<ObjectMemberViewModel>
{

  /// <summary>
  /// Acceptable types of members.
  /// </summary>
  public IEnumerable<Type> MemberTypes { get; set; } = new Collection<Type>();

  /// <summary>
  /// Override of the base method to set item <see cref="ObjectMemberViewModel.Collection"/> property.
  /// </summary>
  /// <param name="index"></param>
  /// <param name="item"></param>
  protected override void InsertItem(int index, ObjectMemberViewModel item)
  {
    item.Collection = this;
    base.InsertItem(index, item);
  }
}
