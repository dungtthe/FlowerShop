namespace FlowerShop.Common.ViewModels
{
    public class PopupViewModel
    {
        public static readonly int NOT_SHOW = -1;
        public static readonly int ERROR = 0;
        public static readonly int SUCCESS = 1;
        public static readonly int YES_NO = 2;

        public int Type {  get; set; }
        public string? Title {  get; set; }

        public string ?Message {  get; set; }


        public PopupViewModel()
        {

        }

        public PopupViewModel(int  type,string title, string message)
        {
            Type = type;
            Title = title;
            Message = message;
        }


    }
}
