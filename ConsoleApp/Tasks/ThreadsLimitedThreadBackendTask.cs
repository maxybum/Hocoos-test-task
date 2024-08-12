using Microsoft.Extensions.Logging;

namespace ConsoleApp.Tasks;

public class ThreadsLimitedThreadBackendTask : ThreadBackendTask {
  private readonly ILogger<ThreadsLimitedThreadBackendTask> _logger;

  private readonly SemaphoreSlim _semaphore = new(5, 5);

  public ThreadsLimitedThreadBackendTask(ILogger<ThreadsLimitedThreadBackendTask> logger) {
    _logger = logger ?? throw new ArgumentNullException();
  }

  protected override async Task<ThreadTaskItemResult> ExecuteAsync(ThreadTaskItemConfig config) {
    await _semaphore.WaitAsync();
    var result = await base.ExecuteAsync(config);
    _semaphore.Release();
    return result;
  }

  protected override void WriteResult(ThreadTaskItemResult result)
  {
    _logger.LogInformation(result.Message);
  }
}

