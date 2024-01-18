using Cars_automobile.ViewModels.AccessoryViewModels;

namespace Cars_automobile.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<AccessoryDetailsViewModel> Accessories { get; set; }
    }
}
