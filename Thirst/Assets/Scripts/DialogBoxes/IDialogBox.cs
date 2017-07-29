namespace Assets.Scripts.Managers.DialogBoxes
{
    public interface IDialogBox
    {
        bool IsModal { get; }
        bool IsOpen { get; set; }
        void OpenDialog(object context);
        void CloseDialog();
    }
}