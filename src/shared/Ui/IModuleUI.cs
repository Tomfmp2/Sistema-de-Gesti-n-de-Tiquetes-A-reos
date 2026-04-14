namespace MyInventory2026.src.shared.ui;

public interface IModuleUI
{
    string Key { get; }
    string Title { get; }
    Task RunAsync(CancellationToken cancellationToken = default);
}
