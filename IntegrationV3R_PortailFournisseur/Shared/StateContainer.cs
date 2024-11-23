namespace IntegrationV3R_PortailFournisseur.Shared
{
    public class StateContainer
    {
        public bool ShouldDisplayFinancePage { get; set; }

        // This event allows components to listen for state changes
        public event Action OnChange;

        public void NotifyStateChanged() => OnChange?.Invoke();
    }

}
