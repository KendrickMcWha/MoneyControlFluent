namespace MoneyControl.Domain.Records;

public record Result(bool Success, string Message, object Payload = null);
