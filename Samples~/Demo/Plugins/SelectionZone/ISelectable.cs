namespace UnitSelectionPackage
{
    public interface ISelectable
    {
        void SetSelected(bool flag);
        
        bool IsSelected();
    }
}