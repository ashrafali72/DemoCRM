namespace DemoCRM.Model
{
    public class MyStateContainer
    {
        public User User { get; set; }
        public event Action OnStateChange;
        public void SetUser(User value)
        {
            this.User = value;
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
